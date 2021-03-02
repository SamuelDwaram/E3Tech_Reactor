using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;

namespace Basf.Ui.ViewModels
{
    public class DashboardViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;

        public DashboardViewModel(IRegionManager regionManager, IFieldDevicesCommunicator fieldDevicesCommunicator)
        {
            this.regionManager = regionManager;
            this.fieldDevicesCommunicator = fieldDevicesCommunicator;
            this.fieldDevicesCommunicator.FieldPointDataReceived += OnFieldPointDataReceived;
        }

        private void OnFieldPointDataReceived(object sender, FieldPointDataReceivedArgs fieldPointDataReceivedArgs)
        {
            LiveData = fieldPointDataReceivedArgs;
        }

        public ICommand NavigateCommand
        {
            get => new DelegateCommand<object>(page => regionManager.RequestNavigate("SelectedViewPane", (string)page));
        }

        private FieldPointDataReceivedArgs _liveData;
        public FieldPointDataReceivedArgs LiveData
        {
            get { return _liveData; }
            set { SetProperty(ref _liveData, value); }
        }
    }
}
