using E3.ActionComments.Model;
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

namespace Eisai.ReactorManager.UI.Models
{
    public class DevicesReportHandler
    {
        private readonly IUnityContainer unityContainer;
        private readonly IReportPrinter reportPrinter;
        private readonly IExperimentInfoProvider experimentInfoProvider;
        private readonly IDatabaseReader databaseReader;
        private readonly ISystemAlarmsManager systemAlarmsManager;
        private readonly ITrendsManager trendsManager;
        private readonly IActionCommentsHandler actionCommentsHandler;
        private readonly MediatorService mediatorService;
        private readonly TaskScheduler taskScheduler;

        public DevicesReportHandler(IUnityContainer unityContainer, IReportPrinter reportPrinter, IExperimentInfoProvider experimentInfoProvider, IDatabaseReader databaseReader, ISystemAlarmsManager systemAlarmsManager, ITrendsManager trendsManager, IActionCommentsHandler actionCommentsHandler, MediatorService mediatorService)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.unityContainer = unityContainer;
            this.reportPrinter = reportPrinter;
            this.experimentInfoProvider = experimentInfoProvider;
            this.databaseReader = databaseReader;
            this.systemAlarmsManager = systemAlarmsManager;
            this.trendsManager = trendsManager;
            this.actionCommentsHandler = actionCommentsHandler;
            this.mediatorService = mediatorService;
        }

        public void PrintBatchReport(Batch selectedBatch, string deviceId, string deviceLabel, IList<string> selectedParameters, DateTime startTime, DateTime endTime)
        {
            Task.Factory.StartNew(new Func<IList<ReportSection>>(() => AddBatchInfoSection(selectedBatch)))
                .ContinueWith(new Func<Task<IList<ReportSection>>, IList<ReportSection>>(t => AddTrendsSection(t.Result, deviceId, deviceLabel, startTime, endTime, selectedParameters)), taskScheduler)
                .ContinueWith(new Func<Task<IList<ReportSection>>, IList<ReportSection>>(t => AddBatchPhotoSection(t.Result, deviceId, deviceLabel, startTime, endTime)))
                .ContinueWith(new Func<Task<IList<ReportSection>>, IList<ReportSection>>(t => AddUserCommentsSection(t.Result, deviceId, deviceLabel, startTime, endTime)))
                .ContinueWith(new Func<Task<IList<ReportSection>>, IList<ReportSection>>(t => AddAlarmsSection(t.Result, deviceId, deviceLabel, startTime, endTime)))
                .ContinueWith(new Func<Task<IList<ReportSection>>, IList<ReportSection>>(t => AddDeviceRecordedParametersSection(t.Result, deviceId, deviceLabel, startTime, endTime, selectedParameters)))
                .ContinueWith(new Func<Task<IList<ReportSection>>, IList<ReportSection>>(t => AddRecipeStepsSection(t.Result, selectedBatch, deviceId, deviceLabel, startTime, endTime)))
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

        private IList<ReportSection> AddBatchPhotoSection(IList<ReportSection> reportSections, string deviceId, string deviceLabel, DateTime startTime, DateTime endTime)
        {
            IList<DbParameterInfo> dbParameters = new List<DbParameterInfo>
            {
                new DbParameterInfo("@FieldDeviceIdentifier", deviceId, DbType.String),
                new DbParameterInfo("@StartTime", startTime.ToString("yyyy-MM-d HH:mm:ss"), DbType.String),
                new DbParameterInfo("@EndTime", endTime.ToString("yyyy-MM-d HH:mm:ss"), DbType.String),
            };

            databaseReader.ExecuteReadCommand("GetFieldDeviceImages", CommandType.StoredProcedure, dbParameters).AsEnumerable().ToList()
                .ForEach(dataRecord => reportSections.Add(new ReportSection {
                        Title = "BATCH PHOTO",
                        Data = new ImageInfo
                        {
                            ImageData = (byte[])dataRecord["ImageData"],
                            ParametersData = ConvertBytesToDataTable((byte[])dataRecord["ParametersData"])
                        },
                        DataType = SectionalDataType.Image,
                        EndPageHere = true
                    }
                ));

            return new List<ReportSection>(reportSections);
        }

        private DataTable ConvertBytesToDataTable(byte[] data)
        {
            DataTable parametersDataTable = new DataTable();
            IList<object> parameterValues = new List<object>();
            using MemoryStream memStream = new MemoryStream(data);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Dictionary<string, string> parameters = (Dictionary<string, string>)binaryFormatter.Deserialize(memStream);
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
        
        private IList<ReportSection> AddTrendsSection(IList<ReportSection> reportSections, string deviceId, string deviceLabel, DateTime startDateTime, DateTime endDateTime, IList<string> selectedParameters)
        {
            DataTable dataTableForTrends = new DataTable();
            if (selectedParameters == null || selectedParameters.Count == 0)
            {
                //Add all parameters
                dataTableForTrends.Columns.Add($"MassTemp{GetParameterUnits("MassTemp")}", typeof(double));
                dataTableForTrends.Columns.Add($"SetPoint{GetParameterUnits("SetPoint")}", typeof(double));
                dataTableForTrends.Columns.Add($"Jacket{GetParameterUnits("Jacket")}", typeof(double));
                dataTableForTrends.Columns.Add($"Pressure{GetParameterUnits("Pressure")}", typeof(double));
                dataTableForTrends.Columns.Add("pH", typeof(double));
                dataTableForTrends.Columns.Add("RPM", typeof(double));
                dataTableForTrends.Columns.Add("TimeStamp", typeof(DateTime));
                
                databaseReader.ExecuteReadCommand($"select * from {deviceId} where TimeStamp between '{startDateTime:yyyy-MM-dd HH:mm:ss.fff}' and '{endDateTime:yyyy-MM-dd HH:mm:ss.fff}' order by TimeStamp",
                    CommandType.Text).AsEnumerable().ToList()
                    .ForEach(dataRecord => {
                        dataTableForTrends.Rows.Add(dataRecord.ItemArray);
                    });
            }
            else
            {
                if (selectedParameters.Any(p => p.Contains("MassTemp")))
                {
                    dataTableForTrends.Columns.Add($"MassTemp{GetParameterUnits("MassTemp")}", typeof(double));
                }
                if (selectedParameters.Any(p => p.Contains("SetPoint")))
                {
                    dataTableForTrends.Columns.Add($"SetPoint{GetParameterUnits("SetPoint")}", typeof(double));
                }
                if (selectedParameters.Any(p => p.Contains("Jacket")))
                {
                    dataTableForTrends.Columns.Add($"Jacket{GetParameterUnits("Jacket")}", typeof(double));
                }
                if (selectedParameters.Any(p => p.Contains("Pressure")))
                {
                    dataTableForTrends.Columns.Add($"Pressure{GetParameterUnits("Pressure")}", typeof(double));
                }
                if (selectedParameters.Any(p => p.Contains("PH")))
                {
                    dataTableForTrends.Columns.Add("pH", typeof(double));
                }
                if (selectedParameters.Any(p => p.Contains("RPM")))
                {
                    dataTableForTrends.Columns.Add("RPM", typeof(double));
                }
                dataTableForTrends.Columns.Add("TimeStamp", typeof(DateTime));

                databaseReader.ExecuteReadCommand($"select * from {deviceId} where TimeStamp between '{startDateTime:yyyy-MM-dd HH:mm:ss.fff}' and '{endDateTime:yyyy-MM-dd HH:mm:ss.fff}' order by TimeStamp",
                    CommandType.Text).AsEnumerable().ToList()
                    .ForEach(dataRecord => {
                        IList<object> oneDataLogOfParameters = new List<object>();
                        selectedParameters.ToList().ForEach(p => {
                            oneDataLogOfParameters.Add(Math.Round(Convert.ToDouble(dataRecord[p]), 1));
                        });
                        oneDataLogOfParameters.Add(dataRecord.Field<DateTime>("TimeStamp"));
                        dataTableForTrends.Rows.Add(oneDataLogOfParameters.ToArray());
                    });
            }

            return new List<ReportSection>(reportSections) {
                    new ReportSection
                    {
                        Title = "BATCH CHART",
                        DataType = SectionalDataType.Image,
                        Data = new List<object> { trendsManager.PrepareTrendsImageForGivenData(dataTableForTrends, new Dictionary<string, string> {
                            { $"MassTemp{GetParameterUnits("MassTemp")}", "-90|210" },
                            { $"SetPoint{GetParameterUnits("SetPoint")}", "-90|210" },
                            { $"Jacket{GetParameterUnits("Jacket")}", "-90|210" },
                            { $"Pressure{GetParameterUnits("Pressure")}", "-1.5|1" },
                            { "pH", "0|14" },
                            { "RPM", "0|250" },
                        }) }.ToArray(),
                        EndPageHere = true
                    }
                };
        }

        private IList<ReportSection> AddRecipeStepsSection(IList<ReportSection> reportSections, Batch selectedBatch, string deviceId, string deviceLabel, DateTime startTime, DateTime endTime)
        {
            DataTable recipeSteps = new DataTable();
            recipeSteps.Columns.Add("Start", typeof(string));
            recipeSteps.Columns.Add("End", typeof(string));
            recipeSteps.Columns.Add("Duration", typeof(string));
            recipeSteps.Columns.Add("Execution Message", typeof(string));
            databaseReader.ExecuteReadCommand($"select * from dbo.RecipeBlockExecutionInfo where BatchIdentifier='{selectedBatch.Identifier}' and FieldDeviceIdentifier = '{deviceId}' and TimeStamp between '{startTime:yyyy-MM-dd HH:mm:ss}' and '{endTime:yyyy-MM-dd HH:mm:ss}'", CommandType.Text).AsEnumerable().ToList()
                .ForEach(dataRecord =>
                {
                    IList<object> recipeStep = new List<object>
                    {
                        dataRecord["StartTime"],
                        dataRecord["EndTime"],
                        dataRecord["Duration"],
                        dataRecord["ExecutionMessage"],
                    };
                    recipeSteps.Rows.Add(recipeStep.ToArray());
                });
            return new List<ReportSection>(reportSections)
            {
                new ReportSection
                {
                    Title = "RECIPE STEPS",
                    DataType = SectionalDataType.Tablular,
                    Data = recipeSteps,
                    EndPageHere = true
                }
            };
        }

        private IList<ReportSection> AddDeviceRecordedParametersSection(IList<ReportSection> reportSections, string deviceId, string deviceLabel, DateTime startDateTime, DateTime endDateTime, IList<string> selectedParameters = null)
        {
            DataTable deviceParametersData = new DataTable();
            if (selectedParameters == null || selectedParameters.Count == 0)
            {
                //Add all parameters
                deviceParametersData.Columns.Add($"MassTemp{GetParameterUnits("MassTemp", true)}", typeof(double));
                deviceParametersData.Columns.Add($"SetPoint{GetParameterUnits("SetPoint", true)}", typeof(double));
                deviceParametersData.Columns.Add($"Jacket{GetParameterUnits("Jacket", true)}", typeof(double));
                deviceParametersData.Columns.Add($"Pressure{GetParameterUnits("Pressure", true)}", typeof(double));
                deviceParametersData.Columns.Add("pH", typeof(double));
                deviceParametersData.Columns.Add("RPM", typeof(double));
                deviceParametersData.Columns.Add("TimeStamp", typeof(DateTime));

                databaseReader.ExecuteReadCommand($"select * from {deviceId} where TimeStamp between '{startDateTime:yyyy-MM-dd HH:mm:ss.fff}' and '{endDateTime:yyyy-MM-dd HH:mm:ss.fff}' order by TimeStamp",
                    CommandType.Text).AsEnumerable().ToList()
                    .ForEach(dataRecord => {
                        deviceParametersData.Rows.Add(dataRecord.ItemArray);
                    });
            }
            else
            {
                if (selectedParameters.Any(p => p.Contains("MassTemp")))
                {
                    deviceParametersData.Columns.Add($"MassTemp{GetParameterUnits("MassTemp", true)}", typeof(double));
                }
                if (selectedParameters.Any(p => p.Contains("SetPoint")))
                {
                    deviceParametersData.Columns.Add($"SetPoint{GetParameterUnits("SetPoint", true)}", typeof(double));
                }
                if (selectedParameters.Any(p => p.Contains("Jacket")))
                {
                    deviceParametersData.Columns.Add($"Jacket{GetParameterUnits("Jacket", true)}", typeof(double));
                }
                if (selectedParameters.Any(p => p.Contains("Pressure")))
                {
                    deviceParametersData.Columns.Add($"Pressure{GetParameterUnits("Pressure", true)}", typeof(double));
                }
                if (selectedParameters.Any(p => p.Contains("PH")))
                {
                    deviceParametersData.Columns.Add("pH", typeof(double));
                }
                if (selectedParameters.Any(p => p.Contains("RPM")))
                {
                    deviceParametersData.Columns.Add("RPM", typeof(double));
                }
                deviceParametersData.Columns.Add("TimeStamp", typeof(DateTime));

                databaseReader.ExecuteReadCommand($"select * from {deviceId} where TimeStamp between '{startDateTime:yyyy-MM-dd HH:mm:ss.fff}' and '{endDateTime:yyyy-MM-dd HH:mm:ss.fff}' order by TimeStamp",
                    CommandType.Text).AsEnumerable().ToList()
                    .ForEach(dataRecord => {
                        IList<object> oneDataLogOfParameters = new List<object>();
                        selectedParameters.ToList().ForEach(p => {
                            oneDataLogOfParameters.Add(Math.Round(Convert.ToDouble(dataRecord[p]), 1));
                        });
                        oneDataLogOfParameters.Add(dataRecord.Field<DateTime>("TimeStamp"));
                        deviceParametersData.Rows.Add(oneDataLogOfParameters.ToArray());
                    });
            }

            return new List<ReportSection>(reportSections) {
                    new ReportSection
                    {
                        Title = "PARAMETERS INFO",
                        DataType = SectionalDataType.Tablular,
                        Data = deviceParametersData,
                        EndPageHere = true
                    }
                };
        }

        private string GetParameterUnits(string parameterName, bool insertNewLine = false)
        {
            return parameterName switch
            {
                string p when p.Contains("MassTemp") => insertNewLine ? $"{Environment.NewLine}(°C)" : "(°C)",
                string p when p.Contains("Jacket") => insertNewLine ? $"{Environment.NewLine}(°C)" : "(°C)",
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
