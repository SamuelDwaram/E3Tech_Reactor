using E3.ReactorManager.LiveGraphManager.ViewModels;
using System.Collections.Generic;

namespace E3.ReactorManager.LiveGraphManager.Model.Interfaces
{
    public interface ILiveGraphViewsContainerManager
    {
        bool AddLiveGraphView(string deviceId);

        bool IsDeviceIdValid(string deviceId);

        LiveGraphViewModel ResoveLiveGraphView(string deviceId);

        Dictionary<string, LiveGraphViewModel> LiveGraphViewsContainer { get; }
    }
}
