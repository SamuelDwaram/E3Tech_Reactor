using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace EcoMotorPanel.ShellProject.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer unityContainer;
        private readonly TaskScheduler taskScheduler;
        private IFieldDevicesCommunicator fieldDevicesCommunicator;

        public ShellViewModel(IRegionManager regionManager, IUnityContainer containerProvider)
        {
            this.regionManager = regionManager;
            unityContainer = containerProvider;
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }

        public void ShellLoaded()
        {
            fieldDevicesCommunicator = unityContainer.Resolve<IFieldDevicesCommunicator>();
            fieldDevicesCommunicator.StartCyclicPollingOfFieldDevices(NavigateToDashboard, taskScheduler);
        }

        public void NavigateToDashboard(Task task)
        {
            if (task.IsCompleted)
            {
                regionManager.RequestNavigate("SelectedViewPane", "EcoMotor");
            }
            else
            {
                if (task.IsFaulted)
                {
                    throw task.Exception;
                }
            }
        }

        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand(ShellLoaded));
            set => _loadedCommand = value;
        }

    }
}
