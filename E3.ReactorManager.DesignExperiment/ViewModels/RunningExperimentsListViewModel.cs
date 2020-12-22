using E3.ReactorManager.DesignExperiment.Model;
using E3.ReactorManager.DesignExperiment.Model.Data;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace E3.ReactorManager.DesignExperiment.ViewModels
{
    public class RunningExperimentsListViewModel : BindableBase
    {
        private readonly IExperimentInfoProvider experimentInfoProvider;
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;
        private readonly IRegionManager regionManager;

        public RunningExperimentsListViewModel(IExperimentInfoProvider experimentInfoProvider, IFieldDevicesCommunicator fieldDevicesCommunicator, IRegionManager regionManager)
        {
            this.experimentInfoProvider = experimentInfoProvider;
            this.fieldDevicesCommunicator = fieldDevicesCommunicator;
            this.regionManager = regionManager;
            Task.Factory.StartNew(GetRunningExperiments);
        }

        private void GetRunningExperiments()
        {
            Task.Factory.StartNew(new Func<IList<Batch>>(experimentInfoProvider.GetAllRunningBatchesInThePlant))
                .ContinueWith((task) => {
                    RunningExperiments = task.Result;
                    RaisePropertyChanged(nameof(RunningExperiments));
                }).ContinueWith(t => {
                    Thread.Sleep(5000);
                    Task.Factory.StartNew(GetRunningExperiments);
                });
        }

        private void Navigate(string deviceId)
        {
            Device device = fieldDevicesCommunicator.GetBasicDeviceInfo(deviceId);
            regionManager.RequestNavigate("SelectedViewPane", device.Type, new NavigationParameters {
                { "Device", device }
            });
        }

        public ICommand NavigateCommand
        {
            get =>  new DelegateCommand<string>(Navigate);
        }

        public IList<Batch> RunningExperiments { get; set; }
    }
}
