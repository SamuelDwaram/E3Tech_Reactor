using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.LiveGraphManager.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;
using Timer = System.Timers.Timer;

namespace E3.ReactorManager.LiveGraphManager.Model.Implementations
{
    public class LiveGraphManager : ILiveGraphManager
    {
        private readonly IUnityContainer containerProvider;
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;
        private readonly Timer timer;
        private string deviceId;
        private List<string> liveTrendParameters;
        private int lastUpdatedSecond = int.MaxValue;

        public LiveGraphManager(IUnityContainer containerProvider)
        {
            this.containerProvider = containerProvider;
            fieldDevicesCommunicator = containerProvider.Resolve<IFieldDevicesCommunicator>();
            timer = new Timer(TimeSpan.FromSeconds(1).TotalMilliseconds);
            timer.Elapsed += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int liveSecond = DateTime.Now.Second;
            if (liveSecond == lastUpdatedSecond)
            {
                //Skip.
            }
            else
            {
                lastUpdatedSecond = DateTime.Now.Second;
                UpdateNow();
            }
        }

        public object ReadFromPlc()
        {
            return GetLiveData(this.deviceId, this.liveTrendParameters.ToArray());
        }

        private void RaiseUpdateLiveGraphEvent(Task<object> obj)
        {
            UpdateLiveGraph?.BeginInvoke(this, new UpdateLiveGraphEventArgs() { DeviceId = deviceId, Data = (dynamic)obj.Result }, null, null);
        }

        public event UpdateLiveGraphEventHandler UpdateLiveGraph;

        public Dictionary<string, dynamic> GetLiveData(string deviceId, string[] parameterNames)
        {
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

            foreach (var parameterName in parameterNames)
            {
                data.Add(parameterName, fieldDevicesCommunicator.ReadFieldPointValue(deviceId, parameterName));
            }
            return data;
        }

        public void SetParameters(string deviceId, List<string> requiredLiveTrendsParameters)
        {
            this.deviceId = deviceId;
            this.liveTrendParameters = requiredLiveTrendsParameters;
        }

        public void UpdateNow()
        {
            Task.Factory.StartNew<object>(new Func<dynamic>(ReadFromPlc))
                .ContinueWith(new Action<Task<dynamic>>(RaiseUpdateLiveGraphEvent));
        }
    }
}
