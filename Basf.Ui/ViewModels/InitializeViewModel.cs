using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.TrendsManager.Models;
using E3.TrendsManager.Services;
using Prism.Regions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;
using Unity.Resolution;

namespace Basf.Ui.ViewModels
{
    public class InitializeViewModel
    {
        private readonly IUnityContainer unityContainer;
        private readonly IRegionManager regionManager;
        private readonly TaskScheduler taskScheduler;

        public InitializeViewModel(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.unityContainer = unityContainer;
            this.regionManager = regionManager;
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            unityContainer.Resolve<IFieldDevicesCommunicator>().StartCyclicPollingOfFieldDevices(CallBack, taskScheduler);
        }

        private void CallBack(Task task)
        {
            regionManager.RequestNavigate("SelectedViewPane", "Login");
            InitializeTrendsModule();
        }

        private void InitializeTrendsModule()
        {
            unityContainer.Resolve<ITrendsManager>(new ParameterOverride[] {
                new ParameterOverride("trendDevices", new List<TrendDevice>() {
                    GetTrendParameters("Reactor_1", "ATR-30L")
                })
            });
        }

        private TrendDevice GetTrendParameters(string deviceId, string deviceLabel)
        {
            return new TrendDevice
            {
                DeviceId = deviceId,
                DeviceLabel = deviceLabel,
                Parameters = new List<TrendParameter>
                {
                    new TrendParameter
                    {
                        Label = "Pressure",
                        Limits = "-1|1.5",
                        FieldPointId = "Pressure",
                        SensorDataSetId = "sensorDataSet_1",
                        Units = "bar",
                    },
                    new TrendParameter
                    {
                        Label = "HcSetPoint",
                        Limits = "-90|200",
                        FieldPointId = "HeatCoolSetPoint",
                        SensorDataSetId = "sensorDataSet_1",
                        Units = "°C",
                        IsLiveTrendParameter = true
                    },
                    new TrendParameter
                    {
                        Label = "Reactor pH",
                        Limits = "0|14",
                        FieldPointId = "ReactorpH",
                        SensorDataSetId = "sensorDataSet_1",
                    },
                    new TrendParameter
                    {
                        Label = "Scrubber pH",
                        Limits = "0|15",
                        FieldPointId = "ScrubberpH",
                        SensorDataSetId = "sensorDataSet_1",
                    },
                    new TrendParameter
                    {
                        Label = "Vent Temperature",
                        Limits = "0|200",
                        FieldPointId = "VentTemperature",
                        SensorDataSetId = "sensorDataSet_1",
                        Units = "°C",
                        IsLiveTrendParameter = true
                    },
                    new TrendParameter
                    {
                        Label = "Vapour Temperature",
                        Limits = "0|200",
                        FieldPointId = "VapourTemperature",
                        SensorDataSetId = "sensorDataSet_1",
                        Units = "°C",
                        IsLiveTrendParameter = true
                    }
                }
            };
        }
    }
}
