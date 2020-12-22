using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.Interfaces.DataAbstractionLayer.Data;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using Magar.Ui.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Magar.Ui.ViewModels
{
    public class TanksViewModel : BindableBase
    {
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;
        private readonly IDatabaseWriter databaseWriter;
        private readonly IDatabaseReader databaseReader;
        private readonly TaskScheduler taskScheduler;
        private IList<LookupTableItem> lookupTableItems = new List<LookupTableItem>();

        public TanksViewModel(IFieldDevicesCommunicator fieldDevicesCommunicator, IDatabaseWriter databaseWriter, IDatabaseReader databaseReader)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.fieldDevicesCommunicator = fieldDevicesCommunicator;
            this.databaseWriter = databaseWriter;
            this.databaseReader = databaseReader;
            this.fieldDevicesCommunicator.FieldPointDataReceived += FieldDevicesCommunicator_FieldPointDataReceived;
            UpdateLookupTableItems();
            Task.Factory.StartNew(new Func<DataTable>(() => databaseReader.ExecuteReadCommand("select Label, Description, Limits, Units from dbo.FieldPoints join dbo.AlarmLimits on FieldPoints.Label = AlarmLimits.FieldPointId join dbo.TankUnits on FieldPoints.Label = TankUnits.FieldPointId", CommandType.Text)))
                .ContinueWith(new Func<Task<DataTable>, IList<IList<Tank>>>(t => PrepareTanksList(t.Result)))
                .ContinueWith(t => {
                    TanksListUpdatedToUi = true;
                    Tanks = t.Result;
                    RaisePropertyChanged(nameof(Tanks));
                }, taskScheduler);
        }

        private IList<IList<Tank>> PrepareTanksList(DataTable result)
        {
            IList<IList<Tank>> tanks = new List<IList<Tank>> { new List<Tank>() };
            foreach (DataRow row in result.AsEnumerable())
            {
                if (tanks[tanks.Count - 1].Count == 8)
                {
                    tanks.Add(new List<Tank>());
                }

                tanks[tanks.Count - 1].Add(new Tank
                {
                    Id = row.Field<string>("Label"),
                    Label = row.Field<string>("Description"),
                    AlarmLimitsString = row.Field<string>("Limits"),
                    Units = row.Field<string>("Units")
                });
            }
            return tanks;
        }

        private void UpdateLookupTableItems()
        {
            try
            {
                lookupTableItems
                    = (from DataRow row in databaseReader.ExecuteReadCommand("select * from dbo.LookupTable order by Height", CommandType.Text).AsEnumerable()
                       select new LookupTableItem
                       {
                           FieldPointId = Convert.ToString(row["FieldPointId"]),
                           Height = Convert.ToSingle(row["Height"]),
                           Volume = Convert.ToSingle(row["Volume"])
                       }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to Read Data from Lookup Table{Environment.NewLine}{ex.Message}", "Data Not found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FieldDevicesCommunicator_FieldPointDataReceived(object sender, FieldPointDataReceivedArgs liveData)
        {
            if (liveData.NewFieldPointData.Contains("NC"))
            {
                return;
            }

            Task.Factory.StartNew(new Func<float>(() => UpdateDatabase(liveData)))
                .ContinueWith(task => {
                    if (TanksListUpdatedToUi)
                    {
                        Tank tank = new Tank
                        {
                            Label = liveData.FieldPointDescription,
                            Value = liveData.NewFieldPointData
                        };

                        if (Tanks.Any(miniTankList => miniTankList.Any(t => t.Label == tank.Label)))
                        {
                            Tanks.ToList().ForEach(miniTankList => {
                                Tank currentTank = miniTankList.FirstOrDefault(t => t.Label == tank.Label);
                                if (currentTank == null)
                                {
                                    //Skip
                                }
                                else
                                {
                                    currentTank.Value = task.Result.ToString();
                                }
                            });
                        }
                    }
                }).ContinueWith(t => RaisePropertyChanged(nameof(Tanks)), taskScheduler);
        }

        private float UpdateDatabase(FieldPointDataReceivedArgs fieldPointDataReceivedArgs)
        {
            IEnumerable<LookupTableItem> itemsForGivenFieldPoint = lookupTableItems.Where(i => i.FieldPointId == fieldPointDataReceivedArgs.FieldPointIdentifier);
            float valueUpdatedForUi = 0;
            float rawValue = Convert.ToSingle(fieldPointDataReceivedArgs.NewFieldPointData);
            for (int i = 0; i < itemsForGivenFieldPoint.Count(); i++)
            {
                LookupTableItem currentItem = itemsForGivenFieldPoint.ElementAt(i);
                if (currentItem.Height == rawValue)
                {
                    Debug.WriteLine($"Equal Height {currentItem.Height}");
                    valueUpdatedForUi = currentItem.Volume;
                    break;
                }
                else if (currentItem.Height > rawValue)
                {
                    if (i == 0)
                    {
                        Debug.WriteLine($"Height less than minimum{currentItem.Height}");
                        valueUpdatedForUi = currentItem.Volume;
                    }
                    else
                    {
                        LookupTableItem previousItem = itemsForGivenFieldPoint.ElementAt(i - 1);
                        float numerator = rawValue - previousItem.Height;
                        float denominator = currentItem.Height - previousItem.Height;
                        float multiplier = currentItem.Volume - previousItem.Volume;
                        valueUpdatedForUi = Convert.ToSingle(Math.Round(numerator / denominator * multiplier, 1) + previousItem.Volume);
                    }
                    break;
                }
                else if (i == itemsForGivenFieldPoint.Count() - 1)
                {
                    Debug.WriteLine($"Height at Max value {currentItem.Height}");
                    valueUpdatedForUi = currentItem.Volume;
                }
                else
                {
                    Debug.WriteLine($"No matchable height found {fieldPointDataReceivedArgs.FieldPointIdentifier} height {rawValue}");
                }
            }

            Debug.WriteLine($"Raw Value:{rawValue} Updated Value:{valueUpdatedForUi}");

            IList<DbParameterInfo> dbParameterInfos = new List<DbParameterInfo> 
            { 
                new DbParameterInfo("DeviceId", fieldPointDataReceivedArgs.FieldDeviceIdentifier, DbType.String),
                new DbParameterInfo("ParameterLabel", fieldPointDataReceivedArgs.FieldPointIdentifier, DbType.String),
                new DbParameterInfo("SensorDataSetId", fieldPointDataReceivedArgs.SensorDataSetIdentifier, DbType.String),
                new DbParameterInfo("Value", rawValue, DbType.String),
                new DbParameterInfo("ValueUpdatedForUi", Math.Round(itemsForGivenFieldPoint.Count() > 0 ? valueUpdatedForUi : rawValue, 2), DbType.String),
            };
            databaseWriter.ExecuteWriteCommand("LogLiveData", CommandType.StoredProcedure, dbParameterInfos);

            return itemsForGivenFieldPoint.Count() > 0 ? valueUpdatedForUi : rawValue;
        }

        public void DoneLoadingFieldDevices(Task task)
        {
            Debug.WriteLine("Field Devices are loaded");
        }

        #region Commands
        public ICommand LoadedCommand 
        {
            get => new DelegateCommand(() => fieldDevicesCommunicator.StartCyclicPollingOfFieldDevices(DoneLoadingFieldDevices, taskScheduler));
        }
        public ICommand RestartCommand
        {
            get => new DelegateCommand(() => {
                Process.Start(Application.ResourceAssembly.Location);
                ShutDownCommand.Execute(default);
            });
        }
        public ICommand ShutDownCommand
        {
            get => new DelegateCommand(() => Process.GetCurrentProcess().Kill());
        }
        #endregion

        public bool TanksListUpdatedToUi { get; set; }
        public IList<IList<Tank>> Tanks { get; set; } = new List<IList<Tank>>();
    }

    public class LookupTableItem
    {
        public string FieldPointId { get; set; }
        public float Height { get; set; }
        public float Volume { get; set; }
    }
}
