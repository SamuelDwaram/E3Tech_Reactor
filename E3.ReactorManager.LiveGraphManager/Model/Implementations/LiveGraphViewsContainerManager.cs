using E3.ReactorManager.LiveGraphManager.Model.Interfaces;
using E3.ReactorManager.LiveGraphManager.ViewModels;
using System.Collections.Generic;
using Unity;

namespace E3.ReactorManager.LiveGraphManager.Model.Implementations
{
    public class LiveGraphViewsContainerManager : ILiveGraphViewsContainerManager
    {
        private readonly IUnityContainer containerProvider;
        private readonly Dictionary<string, LiveGraphViewModel> liveGraphViewsContainer;

        public LiveGraphViewsContainerManager(IUnityContainer containerProvider)
        {
            this.containerProvider = containerProvider;
            liveGraphViewsContainer = new Dictionary<string, LiveGraphViewModel>();
        }

        public bool AddLiveGraphView(string deviceId)
        {
            if (IsDeviceIdValid(deviceId))
            {
                var vm = containerProvider.Resolve<LiveGraphViewModel>();
                vm.DeviceId = deviceId;
                vm.SetParametersToLiveGraphManager();
                liveGraphViewsContainer.Add(deviceId, vm);

                return true;
            }

            return false;
        }

        public LiveGraphViewModel ResoveLiveGraphView(string deviceId)
        {
            LiveGraphViewsContainer.TryGetValue(deviceId, out LiveGraphViewModel liveGraphViewModel);
            return liveGraphViewModel;
        }

        public bool IsDeviceIdValid(string deviceId)
        {
            if (LiveGraphViewsContainer.ContainsKey(deviceId))
            {
                return false;
            }

            return true;
        }

        public Dictionary<string, LiveGraphViewModel> LiveGraphViewsContainer => liveGraphViewsContainer;
    }
}
