﻿using E3.ActionComments.Model;
using E3.Mediator.Models;
using E3.Mediator.Services;
using E3.ReactorManager.DesignExperiment.Model;
using E3.ReactorManager.DesignExperiment.Model.Data;
using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.Interfaces.DataAbstractionLayer.Data;
using E3.ReactorManager.ReportsManager.Model.Data;
using E3.ReactorManager.ReportsManager.Model.Interfaces;
using E3.SystemAlarmManager.Models;
using E3.SystemAlarmManager.Services;
using E3.TrendsManager.Services;
using E3.UserManager.Model.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace Basf.Ui.Models
{
    public class DevicesReportHandler
    {
        private readonly IUnityContainer unityContainer;
        private readonly ICsvReportPrinter csvReportPrinter;
        private readonly IReportPrinter reportPrinter;
        private readonly IExperimentInfoProvider experimentInfoProvider;
        private readonly IDatabaseReader databaseReader;
        private readonly ISystemAlarmsManager systemAlarmsManager;
        private readonly ITrendsManager trendsManager;
        private readonly IActionCommentsHandler actionCommentsHandler;
        private readonly MediatorService mediatorService;
        private readonly TaskScheduler taskScheduler;

        public DevicesReportHandler(IUnityContainer unityContainer, ICsvReportPrinter csvReportPrinter, IReportPrinter reportPrinter, IExperimentInfoProvider experimentInfoProvider, IDatabaseReader databaseReader, ISystemAlarmsManager systemAlarmsManager, ITrendsManager trendsManager, IActionCommentsHandler actionCommentsHandler, MediatorService mediatorService)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.unityContainer = unityContainer;
            this.csvReportPrinter = csvReportPrinter;
            this.reportPrinter = reportPrinter;
            this.experimentInfoProvider = experimentInfoProvider;
            this.databaseReader = databaseReader;
            this.systemAlarmsManager = systemAlarmsManager;
            this.trendsManager = trendsManager;
            this.actionCommentsHandler = actionCommentsHandler;
            this.mediatorService = mediatorService;
        }

        public void PrintBatchCSVReport(Batch selectedBatch, string deviceId, string deviceLabel, IList<string> selectedParameters, DateTime startTime, DateTime endTime)
        {
            Task.Factory.StartNew(new Func<IList<ReportSection>>(() => AddDeviceRecordedParametersSection(new List<ReportSection>(), deviceId, deviceLabel, startTime, endTime, selectedParameters).reportSections))
                .ContinueWith(new Action<Task<IList<ReportSection>>>(t =>
                {
                    User loggedInUser = (User)Application.Current.Resources["LoggedInUser"];
                    mediatorService.NotifyColleagues(InMemoryMediatorMessageContainer.RecordAudit, new object[] {
                    unityContainer, $"Generated Report for Batch : {selectedBatch.Name}", loggedInUser.Name, "Batch" });
                    csvReportPrinter.PrintReportSections("ALCHEMI BATCH REPORT", t.Result);
                }));
        }

        public void PrintBatchPDFReport(Batch selectedBatch, string deviceId, string deviceLabel, IList<string> selectedParameters, DateTime startTime, DateTime endTime)
        {
            Task.Factory.StartNew(new Func<IList<ReportSection>>(() => AddBatchInfoSection(selectedBatch)))
                .ContinueWith(new Func<Task<IList<ReportSection>>, (IList<ReportSection>, IList<string>, DataTable)>(t => AddDeviceRecordedParametersSection(t.Result, deviceId, deviceLabel, startTime, endTime, selectedParameters)))
                .ContinueWith(new Func<Task<(IList<ReportSection>, IList<string>, DataTable)>, IList<ReportSection>>(t => AddTrendsSection(t.Result.Item1, deviceId, deviceLabel, t.Result.Item2, t.Result.Item3)), taskScheduler)
                .ContinueWith(new Func<Task<IList<ReportSection>>, IList<ReportSection>>(t => AddUserCommentsSection(t.Result, deviceId, deviceLabel, startTime, endTime)))
                .ContinueWith(new Func<Task<IList<ReportSection>>, IList<ReportSection>>(t => AddAlarmsSection(t.Result, deviceId, deviceLabel, startTime, endTime)))
                .ContinueWith(new Action<Task<IList<ReportSection>>>(t => {
                    User loggedInUser = (User)Application.Current.Resources["LoggedInUser"];
                    mediatorService.NotifyColleagues(InMemoryMediatorMessageContainer.RecordAudit, new object[] {
                    unityContainer, $"Generated Report for Batch : {selectedBatch.Name}", loggedInUser.Name, "Batch" });
                    reportPrinter.PrintReportSections("ALCHEMI BATCH REPORT", t.Result, Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"Images\report_logo.png"));
                }));
        }

        private IList<ReportSection> AddUserCommentsSection(IList<ReportSection> reportSections, string deviceId, string deviceLabel, DateTime startTime, DateTime endTime)
        {
            DataTable actionComments = new DataTable();
            actionComments.Columns.Add("Device", typeof(string));
            actionComments.Columns.Add("Comments", typeof(string));
            actionComments.Columns.Add("User", typeof(string));
            actionComments.Columns.Add("TimeStamp", typeof(DateTime));
            actionCommentsHandler.GetActionComments(deviceId, startTime, endTime).ToList()
                .ForEach(actionComment => {
                    actionComments.Rows.Add(new List<object> {
                        actionComment.FieldDeviceLabel,
                        actionComment.Comments,
                        actionComment.NameOfUser,
                        actionComment.TimeStamp
                    }.ToArray());
                });
            return new List<ReportSection>(reportSections)
            {
                new ReportSection
                {
                    Title = "USER COMMENTS",
                    DataType = SectionalDataType.Tablular,
                    EndPageHere = true,
                    Data = actionComments
                }
            };
        }

        private IList<ReportSection> AddAlarmsSection(IList<ReportSection> reportSections, string deviceId, string deviceLabel, DateTime startTime, DateTime endTime)
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
                    Title = "BATCH ALARMS",
                    DataType = SectionalDataType.Tablular,
                    Data = alarmsDataTable,
                    EndPageHere = true
                });

                return reportSections;
            }
        }

        private DataTable ConvertBytesToDataTable(byte[] data)
        {
            DataTable parametersDataTable = new DataTable();
            IList<object> parameterValues = new List<object>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            using (MemoryStream memStream = new MemoryStream(data))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                parameters = (Dictionary<string, string>)binaryFormatter.Deserialize(memStream);
            }
            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                if (parameter.Key.Contains("MassTemp"))
                {
                    parametersDataTable.Columns.Add($"MassTemp{GetParameterUnits("MassTemp", true)}", typeof(double));
                    parameterValues.Add(parameter.Value);
                }
                if (parameter.Key.Contains("SetPoint"))
                {
                    parametersDataTable.Columns.Add($"SetPoint{GetParameterUnits("SetPoint", true)}", typeof(double));
                    parameterValues.Add(parameter.Value);
                }
                if (parameter.Key.Contains("Jacket"))
                {
                    parametersDataTable.Columns.Add($"Jacket{GetParameterUnits("Jacket", true)}", typeof(double));
                    parameterValues.Add(parameter.Value);
                }
                if (parameter.Key.Contains("Pressure"))
                {
                    parametersDataTable.Columns.Add($"Pressure{GetParameterUnits("Pressure", true)}", typeof(double));
                    parameterValues.Add(parameter.Value);
                }
                if (parameter.Key.Contains("PH"))
                {
                    parametersDataTable.Columns.Add("pH", typeof(double));
                    parameterValues.Add(parameter.Value);
                }
                if (parameter.Key.Contains("RPM"))
                {
                    parametersDataTable.Columns.Add("RPM", typeof(double));
                    parameterValues.Add(parameter.Value);
                }
                if (parameter.Key.Contains("TimeStamp"))
                {
                    parametersDataTable.Columns.Add("TimeStamp", typeof(DateTime));
                    parameterValues.Add(parameter.Value);
                }
            }
            parametersDataTable.Rows.Add(parameterValues.ToArray());
            return parametersDataTable;
        }

        private IList<ReportSection> AddTrendsSection(IList<ReportSection> reportSections, string deviceId, string deviceLabel, IList<string> parameters, DataTable dataTableForTrends)
            => new List<ReportSection>(reportSections) {
                    new ReportSection
                    {
                        Title = "BATCH CHART",
                        DataType = SectionalDataType.Image,
                        Data = new List<object> { trendsManager.PrepareTrendsImageForGivenData(deviceId, dataTableForTrends, parameters) }.ToArray(),
                        EndPageHere = true
                    }
                };

        private (IList<ReportSection> reportSections, IList<string> parameters, DataTable dataTableForTrends) AddDeviceRecordedParametersSection(IList<ReportSection> reportSections, string deviceId, string deviceLabel, DateTime startDateTime, DateTime endDateTime, IList<string> selectedParameters = null)
        {
            DataTable deviceParametersRecordedData = new DataTable();
            deviceParametersRecordedData.Columns.Add("S.No", typeof(int));
            DataTable fieldDeviceData = databaseReader.ExecuteReadCommand($"select * from {deviceId} where TimeStamp between '{startDateTime:yyyy-MM-dd HH:mm:ss.fff}' and '{endDateTime:yyyy-MM-dd HH:mm:ss.fff}' order by TimeStamp", CommandType.Text);
            List<string> fpRecorded = new List<string>();
            databaseReader.ExecuteReadCommand($"select Identifier, DataUnit from dbo.SensorsDataSet where FieldDeviceIdentifier = '{deviceId}'", CommandType.Text).AsEnumerable()
                .ToList().ForEach((sensorDataSet) => {
                    IEnumerable<string> parameters = from DataRow dataRow in databaseReader.ExecuteReadCommand($"select Label from dbo.FieldPoints where SensorDataSetIdentifier = '{Convert.ToString(sensorDataSet["Identifier"])}' and ToBeLogged = 'true'", CommandType.Text).AsEnumerable()
                                                     select Convert.ToString(dataRow["Label"]);
                    fpRecorded.AddRange(parameters.Where(p => selectedParameters.Contains(p)));
                });

            //Add columns to the data table
            fpRecorded.ForEach(parameter => deviceParametersRecordedData.Columns.Add($"{parameter}{GetParameterUnits(parameter)}", typeof(double)));

            //Add TimeStamp column in the end
            deviceParametersRecordedData.Columns.Add("TimeStamp", typeof(DateTime));

            //Prepare the device recorded parameters data
            for (int index = 0; index < fieldDeviceData.AsEnumerable().Count(); index++)
            {
                DataRow dataRecord = fieldDeviceData.Rows[index];
                IList<object> oneDataLogOfParameters = new List<object>
                {
                    //Add S.no to the DataTable
                    index + 1
                };

                //Add the parameters info
                fpRecorded.ForEach(parameter =>
                {
                    oneDataLogOfParameters.Add(dataRecord.Field<double>(parameter));
                });
                oneDataLogOfParameters.Add(dataRecord.Field<DateTime>("TimeStamp"));
                deviceParametersRecordedData.Rows.Add(oneDataLogOfParameters.ToArray());
            }

            return (new List<ReportSection>(reportSections) {
                    new ReportSection
                    {
                        Title = "PARAMETERS INFO",
                        DataType = SectionalDataType.Tablular,
                        Data = deviceParametersRecordedData,
                        EndPageHere = true
                    }
                }, fpRecorded, deviceParametersRecordedData);
        }

        private string GetParameterUnits(string parameterName, bool insertNewLine = false)
        {
            return parameterName switch
            {
                string p when p.Contains("Temp") => insertNewLine ? $"{Environment.NewLine}(°C)" : "(°C)",
                string p when p.Contains("SetPoint") => insertNewLine ? $"{Environment.NewLine}(°C)" : "(°C)",
                string p when p.Contains("Pressure") => insertNewLine ? $"{Environment.NewLine}(bar)" : "(bar)",
                _ => string.Empty,
            };
        }

        private IList<ReportSection> AddBatchInfoSection(Batch selectedBatch)
        {
            return new List<ReportSection>
            {
                new ReportSection
                {
                    Title = "BATCH INFO",
                    DataType = SectionalDataType.LabelValuePairs,
                    EndPageHere = true,
                    Data = new List<LabelValuePair>
                    {
                        new LabelValuePair("Name" , selectedBatch.Name),
                        new LabelValuePair("Number" , selectedBatch.Number),
                        new LabelValuePair("Stage" , selectedBatch.Stage),
                        new LabelValuePair("Device" , selectedBatch.FieldDeviceLabel),
                        new LabelValuePair("HC" , selectedBatch.HCIdentifier),
                        new LabelValuePair("Stirrer" , selectedBatch.StirrerIdentifier),
                        new LabelValuePair("Dosing Pump Usage" , selectedBatch.DosingPumpUsage),
                        new LabelValuePair("Comments" , selectedBatch.Comments),
                        new LabelValuePair("Time Started" , selectedBatch.TimeStarted.ToString()),
                        new LabelValuePair("Time Completed" , selectedBatch.TimeCompleted.ToString()),
                        new LabelValuePair("Duration", (selectedBatch.TimeCompleted - selectedBatch.TimeStarted).ToString()),
                        new LabelValuePair("Scientist Name" , selectedBatch.ScientistName),
                    }
                }
            };
        }

        public IList<Batch> GetCompletedBatches()
        {
            return experimentInfoProvider.GetAllCompletedBatches();
        }
    }
}