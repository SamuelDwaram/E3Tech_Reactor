using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace E3.RemoteMonitoringSystem.UI.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly TaskScheduler taskScheduler;
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;

        public LoginViewModel(IRegionManager regionManager, IFieldDevicesCommunicator fieldDevicesCommunicator)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.regionManager = regionManager;
            this.fieldDevicesCommunicator = fieldDevicesCommunicator;
        }

        private void PageLoaded()
        {
            fieldDevicesCommunicator.StartCyclicPollingOfFieldDevices(NavigateToDashboard, taskScheduler);
        }

        private void NavigateToDashboard(Task task)
        {
            if (task.IsCompleted)
            {
                regionManager.RequestNavigate("SelectedViewPane", "Dashboard");
            }
            else
            {
                if (task.IsFaulted)
                {
                    throw task.Exception;
                }
            }
        }

        private ICommand loadedCommand;
        public ICommand LoadedCommand
        {
            get => loadedCommand ?? (loadedCommand = new DelegateCommand(PageLoaded));
            set => loadedCommand = value;
        }
    }
}
