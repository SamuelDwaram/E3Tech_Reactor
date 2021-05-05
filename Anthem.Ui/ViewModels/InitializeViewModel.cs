using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using Prism.Regions;
using System.Threading.Tasks;
using Unity;

namespace Anathem.Ui.ViewModels
{
    public class InitializeViewModel
    {
        private readonly IRegionManager regionManager;
        private readonly TaskScheduler taskScheduler;

        public InitializeViewModel(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(() => unityContainer.Resolve<IFieldDevicesCommunicator>().StartCyclicPollingOfFieldDevices(CallBack, taskScheduler));
        }

        private void CallBack(Task task)
        {
            regionManager.RequestNavigate("SelectedViewPane", "Dashboard");
        }
    }
}
