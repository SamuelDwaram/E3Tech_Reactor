using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.TrendsManager.Models;
using E3.TrendsManager.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;
using Unity.Resolution;

namespace E3.ShellProject.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer unityContainer;
        private readonly TaskScheduler taskScheduler;

        public MainWindowViewModel(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.regionManager = regionManager;
            this.unityContainer = unityContainer;
        }

        private TrendDevice GetTrendDevice(string deviceId, string deviceLabel, int deviceIndex)
        {
            return new TrendDevice
            {
                DeviceId = deviceId,
                DeviceLabel = deviceLabel,
                Parameters = new List<TrendParameter>
                {
                    new TrendParameter
                    {
                        Label = "Current",
                        Limits = "0|10",
                        FieldPointId = "Current_R",
                        SensorDataSetId = $"currentSensors_{deviceIndex}",
                        Units = "A",
                        ParameterType = TrendParameterType.Group
                    },
                    new TrendParameter
                    {
                        Label = "Current",
                        Limits = "0|10",
                        FieldPointId = "Current_Y",
                        SensorDataSetId = $"currentSensors_{deviceIndex}",
                        Units = "A",
                        ParameterType = TrendParameterType.Group
                    },
                    new TrendParameter
                    {
                        Label = "Current",
                        Limits = "0|10",
                        FieldPointId = "Current_B",
                        SensorDataSetId = $"currentSensors_{deviceIndex}",
                        Units = "A",
                        ParameterType = TrendParameterType.Group
                    },
                    new TrendParameter
                    {
                        Label = "Voltage",
                        Limits = "0|300",
                        FieldPointId = "Voltage_1",
                        SensorDataSetId = $"voltageSensors_{deviceIndex}",
                        Units = "V",
                        ParameterType = TrendParameterType.Group
                    },
                    new TrendParameter
                    {
                        Label = "Voltage",
                        Limits = "0|300",
                        FieldPointId = "Voltage_2",
                        SensorDataSetId = $"voltageSensors_{deviceIndex}",
                        Units = "V",
                        ParameterType = TrendParameterType.Group
                    },
                    new TrendParameter
                    {
                        Label = "Voltage",
                        Limits = "0|300",
                        FieldPointId = "Voltage_3",
                        SensorDataSetId = $"voltageSensors_{deviceIndex}",
                        Units = "V",
                        ParameterType = TrendParameterType.Group
                    },
                    new TrendParameter
                    {
                        Label = "Vibration",
                        Limits = "0|10",
                        FieldPointId = "Vibration",
                        SensorDataSetId = $"vibrationSensors_{deviceIndex}",
                        Units = "mm/s",
                    },
                }
            };
        }

        public ICommand InitializeCommand
        {
            get => new DelegateCommand(() => {
                regionManager.RequestNavigate("SelectedViewPane", "Login");

                Task.Factory.StartNew(new Func<Dictionary<string, string>>(() => unityContainer.Resolve<IDatabaseReader>()
                .ExecuteReadCommand("select * from dbo.FieldDevices", CommandType.Text).AsEnumerable()
                .Select(row => new KeyValuePair<string, string>(row.Field<string>("Identifier"), row.Field<string>("Label")))
                .ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value)))
                .ContinueWith(new Func<Task<Dictionary<string, string>>, IList<TrendDevice>>(t => {
                    IList<TrendDevice> trendDevices = new List<TrendDevice>();
                    foreach (KeyValuePair<string, string> keyValuePair in t.Result)
                    {
                        trendDevices.Add(GetTrendDevice(keyValuePair.Key, keyValuePair.Value, trendDevices.Count + 1));
                    }
                    return trendDevices;
                })).ContinueWith(t => unityContainer.Resolve<ITrendsManager>(new ParameterOverride[] { 
                    new ParameterOverride("trendDevices", t.Result) 
                }), taskScheduler);
            });
        }
    }
}
