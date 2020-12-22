using E3.Bpu.Services;
using E3.TrendsManager.Models;
using E3.TrendsManager.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;
using Unity.Resolution;

namespace E3.RemoteClient.Shell.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IUnityContainer unityContainer;
        private readonly IRegionManager regionManager;
        private IBusinessProcessingUnit bpu;
        private readonly TaskScheduler taskScheduler;

        public MainWindowViewModel(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.unityContainer = unityContainer;
            this.regionManager = regionManager;
        }

        public void Navigate(string pageName)
        {
            regionManager.RequestNavigate("SelectedViewPane", pageName);
        }

        private void InitializeTrendsModule()
        {
            unityContainer.Resolve<ITrendsManager>(new ParameterOverride[] {
                new ParameterOverride("trendDevices", new List<TrendDevice>() {
                    GetTrendParameters("Reactor_1", "RD/GSA-01 20L"),
                    GetTrendParameters("Reactor_2", "RD/GSA-02 50L"),
                    GetTrendParameters("Reactor_3", "RD/GSA-03 100L"),
                    GetTrendParameters("Reactor_4", "RD/GSA-04 50L"),
                    GetTrendParameters("Reactor_5", "RD/GSA-05 10L"),
                    GetTrendParameters("Reactor_6", "RD/GSA-06 30L"),
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
            };
        }

        public void LoadBpu()
        {
            bpu = unityContainer.Resolve<IBusinessProcessingUnit>(new ResolverOverride[] {
                new ParameterOverride("uiCallBack", new Action<Task>((t) => {
                    Navigate("Login");
                    InitializeTrendsModule();
                })),
                new ParameterOverride("uiTaskScheduler", taskScheduler),
            });
            //navigate to InitializingIndicator
            Navigate("Initializing");
        }

        #region Commands
        private ICommand _loadBpuCommand;
        public ICommand LoadBpuCommand
        {
            get => _loadBpuCommand ?? (_loadBpuCommand = new DelegateCommand(LoadBpu));
            set => SetProperty(ref _loadBpuCommand, value);
        }

        private ICommand _navigateCommand;
        public ICommand NavigateCommand
        {
            get => _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(Navigate));
            set => SetProperty(ref _navigateCommand, value);
        }
        #endregion
    }
}
