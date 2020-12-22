using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.ReportsManager.Model.Data;
using E3.ReactorManager.ReportsManager.Model.Interfaces;
using E3.SystemAlarmManager.Models;
using E3.SystemAlarmManager.Services;
using E3.TrendsManager.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace E3.RemoteMonitoringSystem.UI.Models
{
    public class DevicesReportHandler
    {
        private readonly IReportPrinter reportPrinter;
        private readonly IDatabaseReader databaseReader;
        private readonly ISystemAlarmsManager systemAlarmsManager;
        private readonly ITrendsManager trendsManager;
        private readonly TaskScheduler taskScheduler;

        public DevicesReportHandler(IUnityContainer unityContainer, IDatabaseReader databaseReader, IReportPrinter reportPrinter)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.databaseReader = databaseReader;
            this.reportPrinter = reportPrinter;

            if (unityContainer.IsRegistered<ISystemAlarmsManager>())
            {
                systemAlarmsManager = unityContainer.Resolve<ISystemAlarmsManager>();
            }

            if (unityContainer.IsRegistered<ITrendsManager>())
            {
                trendsManager = unityContainer.Resolve<ITrendsManager>();
            }
        }

        public void PrintDeviceSummaryReport(string deviceId, string deviceLabel, DateTime startTime, DateTime endTime)
        {
            Task.Factory.StartNew(new Func<IList<ReportSection>>(() => AddDeviceInfoReportSection(deviceId, deviceLabel, startTime, endTime)))
                .ContinueWith(new Func<Task<IList<ReportSection>>, IList<ReportSection>>((task) => AddAlarmsReportSection(task.Result, deviceId, deviceLabel, startTime, endTime)))
                .ContinueWith(new Func<Task<IList<ReportSection>>, IList<ReportSection>>((task) => AddTrendsReportSection(task.Result, deviceId, deviceLabel, startTime, endTime)), taskScheduler)
                .ContinueWith(new Action<Task<IList<ReportSection>>>((task) => {
                    reportPrinter.PrintReportSections("RMS MOTOR SUMMARY REPORT", task.Result);
                }));
        }

        public async void PrintConsolidatedReport(DateTime startTime, DateTime endTime)
        {
            Dictionary<string, string> availableDevices = await Task.Factory.StartNew(new Func<Dictionary<string, string>>(() => {
                return (from DataRow row in databaseReader.ExecuteReadCommand("select * from dbo.FieldDevices", CommandType.Text).AsEnumerable()
                        select new KeyValuePair<string, string>(Convert.ToString(row["Identifier"]), Convert.ToString(row["Label"])))
                        .ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value);
            }));
            List<ReportSection> reportSections = new List<ReportSection>();
            foreach (KeyValuePair<string, string> keyValuePair in availableDevices)
            {
                string deviceId = keyValuePair.Key;
                string deviceLabel = keyValuePair.Value;
                IList<ReportSection> sectionsForDevice
                    = await Task.Factory.StartNew(new Func<IList<ReportSection>>(() => AddDeviceInfoReportSection(deviceId, deviceLabel, startTime, endTime)))
                        .ContinueWith(new Func<Task<IList<ReportSection>>, IList<ReportSection>>((task) => AddAlarmsReportSection(task.Result, deviceId, deviceLabel, startTime, endTime)));
                reportSections.AddRange(sectionsForDevice);
            }

            reportPrinter.PrintReportSections("RMS MOTOR CONSOLIDATED REPORT", reportSections);
        }

        public void PrintDataReport(string deviceId, string deviceLabel, IList<string> selectedParameters, DateTime startTime, DateTime endTime)
        {
            Task.Factory.StartNew(new Func<IList<ReportSection>>(() => AddDeviceInfoReportSection(deviceId, deviceLabel, startTime, endTime, selectedParameters)))
                .ContinueWith(new Func<Task<IList<ReportSection>>, IList<ReportSection>>((task) => AddDeviceRecordedParametersSection(task.Result, deviceId, deviceLabel, startTime, endTime, selectedParameters)))
                .ContinueWith(new Func<Task<IList<ReportSection>>, IList<ReportSection>>((task) => AddAlarmsReportSection(task.Result, deviceId, deviceLabel, startTime, endTime)))
                .ContinueWith(new Func<Task<IList<ReportSection>>, IList<ReportSection>>((task) => AddTrendsReportSection(task.Result, deviceId, deviceLabel, startTime, endTime, selectedParameters)), taskScheduler)
                .ContinueWith(new Action<Task<IList<ReportSection>>>((task) => {
                    reportPrinter.PrintReportSections("RMS MOTOR DETAILED REPORT", task.Result);
                }));
        }

        private IList<ReportSection> AddDeviceRecordedParametersSection(IList<ReportSection> reportSections, string deviceId, string deviceLabel, DateTime startDateTime, DateTime endDateTime, IList<string> selectedParameters = null)
        {
            DataTable deviceParametersRecordedData = new DataTable();
            deviceParametersRecordedData.Columns.Add("S.No", typeof(int));
            if (selectedParameters == null)
            {
                //Add all parameters
                deviceParametersRecordedData.Columns.Add("Current(A)", typeof(double));
                deviceParametersRecordedData.Columns.Add("Vibration(mm/s)", typeof(double));
                deviceParametersRecordedData.Columns.Add("Voltage(V)", typeof(double));
                deviceParametersRecordedData.Columns.Add("TimeStamp", typeof(DateTime));
            }
            else
            {
                if (selectedParameters.Contains("Current"))
                {
                    deviceParametersRecordedData.Columns.Add("Current(A)", typeof(double));
                }
                if (selectedParameters.Contains("Vibration"))
                {
                    deviceParametersRecordedData.Columns.Add("Vibration(mm/s)", typeof(double));
                }
                if (selectedParameters.Contains("Voltage"))
                {
                    deviceParametersRecordedData.Columns.Add("Voltage(V)", typeof(double));
                }
                deviceParametersRecordedData.Columns.Add("TimeStamp", typeof(DateTime));
            }

            DataTable fieldDeviceData = databaseReader.ExecuteReadCommand($"select * from {deviceId} where TimeStamp between '{startDateTime:yyyy-MM-dd HH:mm:ss.fff}' and '{endDateTime:yyyy-MM-dd HH:mm:ss.fff}' order by TimeStamp", CommandType.Text);
            Dictionary<string, IList<string>> sensorDataSetDictionary = new Dictionary<string, IList<string>>();
            databaseReader.ExecuteReadCommand($"select Identifier, DataUnit from dbo.SensorsDataSet where FieldDeviceIdentifier = '{deviceId}' and DataUnit is not null", CommandType.Text).AsEnumerable()
                .ToList().ForEach((sensorDataSet) => {
                    string sensorDataSetDataUnit = Convert.ToString(sensorDataSet["DataUnit"]);
                    if (selectedParameters == null)
                    {
                            //Get the Fieldpoints of SensorDataSet
                            IList<string> parametersList = (from DataRow dataRow in databaseReader.ExecuteReadCommand($"select Label from dbo.FieldPoints where SensorDataSetIdentifier = '{sensorDataSet["Identifier"]}' and ToBeLogged = 'true'", CommandType.Text).AsEnumerable()
                                                        select Convert.ToString(dataRow["Label"])).ToList();
                        sensorDataSetDictionary.Add(sensorDataSetDataUnit + GetParameterUnits(Convert.ToString(sensorDataSet["DataUnit"])), parametersList);
                    }
                    else if (selectedParameters.Contains(sensorDataSetDataUnit))
                    {
                            //Get the Fieldpoints of SensorDataSet
                            IList<string> parametersList = (from DataRow dataRow in databaseReader.ExecuteReadCommand($"select Label from dbo.FieldPoints where SensorDataSetIdentifier = '{sensorDataSet["Identifier"]}' and ToBeLogged = 'true'", CommandType.Text).AsEnumerable()
                                                        select Convert.ToString(dataRow["Label"])).ToList();
                        sensorDataSetDictionary.Add(sensorDataSetDataUnit + GetParameterUnits(Convert.ToString(sensorDataSet["DataUnit"])), parametersList);
                    }
                    else
                    {
                        // skip.
                    }
                });

            //Prepare the device recorded parameters data
            for (int index = 0; index < fieldDeviceData.AsEnumerable().Count(); index++)
            {
                DataRow dataRecord = fieldDeviceData.Rows[index];
                IList<object> oneDataLogOfParameters = new List<object>();

                //Add S.no to the DataTable
                oneDataLogOfParameters.Add(index + 1);

                //Add the parameters info
                sensorDataSetDictionary.ToList().ForEach(sensorDataSet => {
                    IList<string> parametersList = sensorDataSet.Value;

                    //Calculate the average of the field points in this sensor Data set
                    double oneDataLogParametersSum = 0;
                    parametersList.ToList().ForEach(p =>
                    {
                        oneDataLogParametersSum += Convert.ToDouble(dataRecord[p]);
                    });
                    oneDataLogOfParameters.Add(Math.Round(oneDataLogParametersSum / parametersList.Count, 1));
                });
                oneDataLogOfParameters.Add(dataRecord.Field<DateTime>("TimeStamp"));
                deviceParametersRecordedData.Rows.Add(oneDataLogOfParameters.ToArray());
            }

            return new List<ReportSection>(reportSections) {
                    new ReportSection
                    {
                        Title = "RMS PARAMETERS",
                        DataType = SectionalDataType.Tablular,
                        Data = deviceParametersRecordedData,
                        EndPageHere = true
                    }
                };
        }

        private IList<ReportSection> AddTrendsReportSection(IList<ReportSection> reportSections, string deviceId, string deviceLabel, DateTime startDateTime, DateTime endDateTime, IList<string> selectedParameters = null)
        {
            if (trendsManager == null)
            {
                return reportSections;
            }
            else
            {
                DataTable dataTableForTrends = new DataTable();
                if (selectedParameters == null)
                {
                    dataTableForTrends.Columns.Add("Current(A)", typeof(double));
                    dataTableForTrends.Columns.Add("Vibration(mm/s)", typeof(double));
                    dataTableForTrends.Columns.Add("Voltage(V)", typeof(double));
                    dataTableForTrends.Columns.Add("TimeStamp", typeof(DateTime));
                }
                else
                {
                    if (selectedParameters.Contains("Current"))
                    {
                        dataTableForTrends.Columns.Add("Current(A)", typeof(double));
                    }
                    if (selectedParameters.Contains("Vibration"))
                    {
                        dataTableForTrends.Columns.Add("Vibration(mm/s)", typeof(double));
                    }
                    if (selectedParameters.Contains("Voltage"))
                    {
                        dataTableForTrends.Columns.Add("Voltage(V)", typeof(double));
                    }
                    dataTableForTrends.Columns.Add("TimeStamp", typeof(DateTime));
                }

                DataTable fieldDeviceData = databaseReader.ExecuteReadCommand($"select * from {deviceId} where TimeStamp between '{startDateTime:yyyy-MM-dd HH:mm:ss.fff}' and '{endDateTime:yyyy-MM-dd HH:mm:ss.fff}' order by TimeStamp", CommandType.Text);
                Dictionary<string, IList<string>> sensorDataSetDictionary = new Dictionary<string, IList<string>>();
                databaseReader.ExecuteReadCommand($"select Identifier, DataUnit from dbo.SensorsDataSet where FieldDeviceIdentifier = '{deviceId}' and DataUnit is not null", CommandType.Text).AsEnumerable()
                    .ToList().ForEach((sensorDataSet) => {
                        string sensorDataSetDataUnit = Convert.ToString(sensorDataSet["DataUnit"]);
                        if (selectedParameters == null)
                        {
                            //Get the Fieldpoints of SensorDataSet
                            IList<string> parametersList = (from DataRow dataRow in databaseReader.ExecuteReadCommand($"select Label from dbo.FieldPoints where SensorDataSetIdentifier = '{sensorDataSet["Identifier"]}' and ToBeLogged = 'true'", CommandType.Text).AsEnumerable()
                                                            select Convert.ToString(dataRow["Label"])).ToList();
                            sensorDataSetDictionary.Add(sensorDataSetDataUnit + GetParameterUnits(Convert.ToString(sensorDataSet["DataUnit"])), parametersList);
                        }
                        else if(selectedParameters.Contains(sensorDataSetDataUnit))
                        {
                            //Get the Fieldpoints of SensorDataSet
                            IList<string> parametersList = (from DataRow dataRow in databaseReader.ExecuteReadCommand($"select Label from dbo.FieldPoints where SensorDataSetIdentifier = '{sensorDataSet["Identifier"]}' and ToBeLogged = 'true'", CommandType.Text).AsEnumerable()
                                                            select Convert.ToString(dataRow["Label"])).ToList();
                            sensorDataSetDictionary.Add(sensorDataSetDataUnit + GetParameterUnits(Convert.ToString(sensorDataSet["DataUnit"])), parametersList);
                        }
                        else
                        {
                            // skip.
                        }
                    });
                
                //Prepare the list of parameters data for the Trends Table
                for (int index = 0; index < fieldDeviceData.AsEnumerable().Count(); index++)
                {
                    DataRow dataRecord = fieldDeviceData.Rows[index];
                    IList<object> oneDataLogOfParameters = new List<object>();
                    sensorDataSetDictionary.ToList().ForEach(sensorDataSet => {
                        IList<string> parametersList = sensorDataSet.Value;

                        //Calculate the average of the field points in this sensor Data set
                        double oneDataLogParametersSum = 0;
                        parametersList.ToList().ForEach(p =>
                        {
                            oneDataLogParametersSum += Convert.ToDouble(dataRecord[p]);
                        });
                        oneDataLogOfParameters.Add(Math.Round(oneDataLogParametersSum / parametersList.Count, 1));
                    });
                    oneDataLogOfParameters.Add(dataRecord.Field<DateTime>("TimeStamp"));
                    dataTableForTrends.Rows.Add(oneDataLogOfParameters.ToArray());
                }

                return new List<ReportSection>(reportSections) {
                    new ReportSection
                    {
                        Title = "RMS MOTOR TRENDS",
                        DataType = SectionalDataType.Image,
                        Data = new List<object> { trendsManager.PrepareTrendsImageForGivenData(dataTableForTrends, new Dictionary<string, string> {
                            { "Current(A)", "0|10" },
                            { "Voltage(V)", "0|300" },
                            { $"Vibration(mm/s)", "0|10" },
                        }) }.ToArray(),
                        EndPageHere = true
                    }
                };
            }
        }

        private IList<ReportSection> AddAlarmsReportSection(IList<ReportSection> reportSections, string deviceId, string deviceLabel, DateTime startTime, DateTime endTime)
        {
            if (systemAlarmsManager == null)
            {
                return reportSections;
            }
            else
            {
                DataTable alarmsDataTable = new DataTable();
                alarmsDataTable.Columns.Add("Title", typeof(string));
                alarmsDataTable.Columns.Add("Raised", typeof(string));
                alarmsDataTable.Columns.Add("Acknowledged", typeof(string));
                alarmsDataTable.Columns.Add("Resolved", typeof(string));
                //Add alarms
                foreach (SystemAlarm systemAlarm in systemAlarmsManager.GetAll(deviceId, startTime, endTime))
                {
                    IList<string> alarmData = new List<string>
                        {
                            systemAlarm.Title,
                            systemAlarm.RaisedTimeStamp.ToString("HH:mm:ss dd-MM-yyyy"),
                            systemAlarm.AcknowledgedTimeStamp.ToString("HH:mm:ss dd-MM-yyyy"),
                            systemAlarm.State == AlarmState.Resolved ? systemAlarm.TimeStamp.ToString("HH:mm:ss dd-MM-yyyy") : "Not Resolved Yet"
                        };
                    alarmsDataTable.Rows.Add(alarmData.ToArray());
                }

                reportSections.Add(new ReportSection
                {
                    Title = "RMS ALARMS",
                    DataType = SectionalDataType.Tablular,
                    Data = alarmsDataTable,
                    EndPageHere = true
                });

                return reportSections;
            }
        }

        private IList<ReportSection> AddDeviceInfoReportSection(string deviceId, string deviceLabel, DateTime startTime, DateTime endTime, IList<string> selectedParameters = null)
        {
            IList<LabelValuePair> labelValuePairs = new List<LabelValuePair>
                {
                    new LabelValuePair("Start Date", startTime.ToString("dd-MM-yyyy")),
                    new LabelValuePair("End Date", endTime.ToString("dd-MM-yyyy")),
                };
            Dictionary<string, IList<string>> parametersList = GetParametersList(deviceId, selectedParameters);
            double runningTime = GetRunningTime(deviceId, startTime, endTime);
            Dictionary<string, string> parametersAvgList = GetGroupedParametersAvgList(deviceId, startTime, endTime, runningTime, selectedParameters);
            parametersAvgList.ToList().ForEach(p => {
                labelValuePairs.Add(new LabelValuePair($"Average of {p.Key}{GetParameterUnits(p.Key)}", p.Value));
            });

            string hours = Convert.ToInt32(runningTime / 60).ToString();
            string minutes = Convert.ToInt32(runningTime % 60).ToString();
            labelValuePairs.Add(new LabelValuePair("Exploitation Duration(HH:MM)", (hours.Length > 1 ? hours : $"0{hours}") + ":" + (minutes.Length > 1 ? minutes : $"0{minutes}")));

            return new List<ReportSection> {
                new ReportSection
                {
                    Title = deviceLabel,
                    DataType = SectionalDataType.LabelValuePairs,
                    Data = labelValuePairs,
                    EndPageHere = true
                }
            };
        }

        private string GetParameterUnits(string parameterName)
        {
            return parameterName switch
            {
                string p when p.Contains("Current") => "(A)",
                string p when p.Contains("Voltage") => "(V)",
                string p when p.Contains("Vibration") => "(mm/s)",
                _ => string.Empty,
            };
        }

        private Dictionary<string, string> GetGroupedParametersAvgList(string deviceId, DateTime startDateTime, DateTime endDateTime, double deviceRunningTime, IList<string> selectedParameters = null)
        {
            DataTable fieldDeviceData = databaseReader.ExecuteReadCommand($"select * from {deviceId} where TimeStamp between '{startDateTime:yyyy-MM-dd HH:mm:ss.fff}' and '{endDateTime:yyyy-MM-dd HH:mm:ss.fff}' and Status > 0", CommandType.Text);
            Dictionary<string, string> groupedParametersAvgDictionary = new Dictionary<string, string>();
            databaseReader.ExecuteReadCommand($"select Identifier, DataUnit from dbo.SensorsDataSet where FieldDeviceIdentifier = '{deviceId}' and DataUnit is not null", CommandType.Text).AsEnumerable()
                .ToList().ForEach((sensorDataSet) => {
                    string sensorsDataSetDataUnit = Convert.ToString(sensorDataSet["DataUnit"]);
                    if (selectedParameters == null)
                    {
                        IList<string> parametersList = (from DataRow dataRow in databaseReader.ExecuteReadCommand($"select top(7) Label from dbo.FieldPoints where SensorDataSetIdentifier = '{sensorDataSet["Identifier"]}' and ToBeLogged = 'true'", CommandType.Text).AsEnumerable()
                                                        select Convert.ToString(dataRow["Label"])).ToList();
                        Dictionary<string, double> parametersIndividualSum = new Dictionary<string, double>();
                        fieldDeviceData.AsEnumerable().ToList().ForEach(dataRecord => {
                            parametersList.ToList().ForEach(p => {
                                if (parametersIndividualSum.ContainsKey(p))
                                {
                                    parametersIndividualSum[p] += Convert.ToDouble(dataRecord[p]);
                                }
                                else
                                {
                                    parametersIndividualSum.Add(p, Convert.ToDouble(dataRecord[p]));
                                }
                            });
                        });

                        string groupParameterAverage = string.Empty;
                        if (parametersIndividualSum.Count > 0)
                        {
                            double allParametersSum = parametersIndividualSum.Sum(keyValuePair => keyValuePair.Value);
                            groupParameterAverage = Math.Round(allParametersSum / (deviceRunningTime * parametersIndividualSum.Count), 2).ToString();
                        }
                        else
                        {
                            groupParameterAverage = "No Data";
                        }

                        groupedParametersAvgDictionary.Add(sensorsDataSetDataUnit, groupParameterAverage);
                    }
                    else if (selectedParameters.Contains(sensorsDataSetDataUnit))
                    {
                        IList<string> parametersList = (from DataRow dataRow in databaseReader.ExecuteReadCommand($"select top(7) Label from dbo.FieldPoints where SensorDataSetIdentifier = '{sensorDataSet["Identifier"]}' and ToBeLogged = 'true'", CommandType.Text).AsEnumerable()
                                                        select Convert.ToString(dataRow["Label"])).ToList();
                        Dictionary<string, double> parametersIndividualSum = new Dictionary<string, double>();
                        fieldDeviceData.AsEnumerable().ToList().ForEach(dataRecord => {
                            parametersList.ToList().ForEach(p => {
                                if (parametersIndividualSum.ContainsKey(p))
                                {
                                    parametersIndividualSum[p] += Convert.ToDouble(dataRecord[p]);
                                }
                                else
                                {
                                    parametersIndividualSum.Add(p, Convert.ToDouble(dataRecord[p]));
                                }
                            });
                        });

                        string groupParameterAverage = string.Empty;
                        if (parametersIndividualSum.Count > 0)
                        {
                            double allParametersSum = parametersIndividualSum.Sum(keyValuePair => keyValuePair.Value);
                            groupParameterAverage = Math.Round(allParametersSum / (deviceRunningTime * parametersIndividualSum.Count), 2).ToString();
                        }
                        else
                        {
                            groupParameterAverage = "No Data";
                        }

                        groupedParametersAvgDictionary.Add(sensorsDataSetDataUnit, groupParameterAverage);
                    }
                    else
                    {
                        // skip.
                    }
                });

            return groupedParametersAvgDictionary;
        }

        private Dictionary<string, IList<string>> GetParametersList(string deviceId, IList<string> selectedParameters = null)
        {
            Dictionary<string, IList<string>> parametersDictionary = new Dictionary<string, IList<string>>();
            foreach (DataRow row in databaseReader.ExecuteReadCommand($"select Identifier, DataUnit from dbo.SensorsDataSet where FieldDeviceIdentifier = '{deviceId}' and DataUnit is not null", CommandType.Text).AsEnumerable())
            {
                string dataUnit = Convert.ToString(row["DataUnit"]);
                if (selectedParameters == null)
                {
                    IList<string> parametersList = new List<string>();
                    parametersList = (from DataRow dataRow in databaseReader.ExecuteReadCommand($"select Label from dbo.FieldPoints where SensorDataSetIdentifier = '{row["Identifier"]}'", CommandType.Text).AsEnumerable()
                                      select Convert.ToString(dataRow["Label"])).ToList();
                    parametersDictionary.Add(dataUnit, parametersList);
                }
                else if (selectedParameters.Contains(dataUnit))
                {
                    IList<string> parametersList = new List<string>();
                    parametersList = (from DataRow dataRow in databaseReader.ExecuteReadCommand($"select Label from dbo.FieldPoints where SensorDataSetIdentifier = '{row["Identifier"]}'", CommandType.Text).AsEnumerable()
                                      select Convert.ToString(dataRow["Label"])).ToList();
                    parametersDictionary.Add(dataUnit, parametersList);
                }
                else
                {
                    continue;
                }
            }

            return parametersDictionary;
        }

        public double GetRunningTime(string deviceId, DateTime startDate, DateTime endDate)
        {
            return databaseReader.ExecuteReadCommand($"select count(*) from dbo.{deviceId} where TimeStamp between '{startDate:yyyy-MM-dd HH:mm:ss}' and '{endDate:yyyy-MM-dd HH:mm:ss}' and Status = 1",
                CommandType.Text).AsEnumerable().First().Field<int>(0);
        }
    }
}
