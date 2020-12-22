using E3.TrendsManager.Models;
using E3.TrendsManager.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Windows.Input;
using Unity;
using Unity.Resolution;

namespace E3.Test.Shell.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IUnityContainer unityContainer;
        private readonly IRegionManager regionManager;

        public MainWindowViewModel(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.unityContainer = unityContainer;
            this.regionManager = regionManager;
        }

        public ICommand LoadedCommand
        {
            get => new DelegateCommand(() => {
                unityContainer.Resolve<ITrendsManager>(new ParameterOverride[] {
                    new ParameterOverride("trendDevices", new List<TrendDevice>() {
                        new TrendDevice
                        {
                            DeviceId = "Reactor_1", DeviceLabel = "RD/GSA-01 20L",
                            Parameters = new List<TrendParameter>
                            {
                                new TrendParameter
                                {
                                    Label = "MassTemp",
                                    Limits = "-90|200",
                                    FieldPointId = "ReactorMassTemperature",
                                    SensorDataSetId = "sensorDataSet_1",
                                    Units = "°C",
                                    IsLiveTrendParameter = true
                                },
                                new TrendParameter
                                {
                                    Label = "SetPoint",
                                    Limits = "-90|200",
                                    FieldPointId = "HeatCoolSetPoint",
                                    SensorDataSetId = "sensorDataSet_1",
                                    Units = "°C",
                                    IsLiveTrendParameter = true
                                },
                                new TrendParameter
                                {
                                    Label = "Jacket",
                                    Limits = "-90|200",
                                    FieldPointId = "JacketOutletTemperature",
                                    SensorDataSetId = "sensorDataSet_1",
                                    Units = "°C",
                                    IsLiveTrendParameter = true
                                },
                                new TrendParameter
                                {
                                    Label = "RPM",
                                    Limits = "200",
                                    FieldPointId = "RPM",
                                    SensorDataSetId = "sensorDataSet_1",
                                    Units = ""
                                }
                            }
                        },
                        /*
                        new TrendDevice
                        {
                            DeviceId = "Motor_10", DeviceLabel = "REF PLANT",
                            Parameters = new List<TrendParameter>
                            {
                                new TrendParameter
                                {
                                    Label = "Current",
                                    Limits = "0|10",
                                    FieldPointId = "Current_R",
                                    SensorDataSetId = "currentSensors_10",
                                    Units = "A",
                                    ParameterType = TrendParameterType.Group
                                },
                                new TrendParameter
                                {
                                    Label = "Current",
                                    Limits = "0|10",
                                    FieldPointId = "Current_Y",
                                    SensorDataSetId = "currentSensors_10",
                                    Units = "A",
                                    ParameterType = TrendParameterType.Group
                                },
                                new TrendParameter
                                {
                                    Label = "Current",
                                    Limits = "0|10",
                                    FieldPointId = "Current_B",
                                    SensorDataSetId = "currentSensors_10",
                                    Units = "A",
                                    ParameterType = TrendParameterType.Group
                                },
                                new TrendParameter
                                {
                                    Label = "Vibration",
                                    Limits = "0|2",
                                    FieldPointId = "Vibration",
                                    SensorDataSetId = "vibrationSensors_10",
                                    Units = "mm/s"
                                },
                                new TrendParameter
                                {
                                    Label = "Voltage",
                                    Limits = "0|250",
                                    FieldPointId = "Voltage_1",
                                    SensorDataSetId = "voltageSensors_10",
                                    Units = "V",
                                    ParameterType = TrendParameterType.Group
                                },
                                new TrendParameter
                                {
                                    Label = "Voltage",
                                    Limits = "0|250",
                                    FieldPointId = "Voltage_2",
                                    SensorDataSetId = "voltageSensors_10",
                                    Units = "V",
                                    ParameterType = TrendParameterType.Group
                                },
                                new TrendParameter
                                {
                                    Label = "Voltage",
                                    Limits = "0|250",
                                    FieldPointId = "Voltage_3",
                                    SensorDataSetId = "voltageSensors_10",
                                    Units = "V",
                                    ParameterType = TrendParameterType.Group
                                },
                            }
                        },
                        new TrendDevice
                        {
                            DeviceId = "Motor_11", DeviceLabel = "AC CHILLED WATER PUMP 2",
                            Parameters = new List<TrendParameter>
                            {
                                new TrendParameter
                                {
                                    Label = "Current",
                                    Limits = "0|10",
                                    FieldPointId = "Current_R",
                                    SensorDataSetId = "currentSensors_11",
                                    Units = "A",
                                    ParameterType = TrendParameterType.Group
                                },
                                new TrendParameter
                                {
                                    Label = "Current",
                                    Limits = "0|10",
                                    FieldPointId = "Current_Y",
                                    SensorDataSetId = "currentSensors_11",
                                    Units = "A",
                                    ParameterType = TrendParameterType.Group
                                },
                                new TrendParameter
                                {
                                    Label = "Current",
                                    Limits = "0|10",
                                    FieldPointId = "Current_B",
                                    SensorDataSetId = "currentSensors_11",
                                    Units = "A",
                                    ParameterType = TrendParameterType.Group
                                },
                                new TrendParameter
                                {
                                    Label = "Vibration",
                                    Limits = "0|5",
                                    FieldPointId = "Vibration",
                                    SensorDataSetId = "vibrationSensors_11",
                                    Units = "mm/s"
                                },
                                new TrendParameter
                                {
                                    Label = "Voltage",
                                    Limits = "0|250",
                                    FieldPointId = "Voltage_1",
                                    SensorDataSetId = "voltageSensors_11",
                                    Units = "V",
                                    ParameterType = TrendParameterType.Group
                                },
                                new TrendParameter
                                {
                                    Label = "Voltage",
                                    Limits = "0|250",
                                    FieldPointId = "Voltage_2",
                                    SensorDataSetId = "voltageSensors_11",
                                    Units = "V",
                                    ParameterType = TrendParameterType.Group
                                },
                                new TrendParameter
                                {
                                    Label = "Voltage",
                                    Limits = "0|250",
                                    FieldPointId = "Voltage_3",
                                    SensorDataSetId = "voltageSensors_11",
                                    Units = "V",
                                    ParameterType = TrendParameterType.Group
                                },
                            }
                        }
                        */
                    })
                });

                regionManager.RequestNavigate("SelectedViewPane", "Trends");
            });
        }
    }
}
