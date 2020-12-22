using System;
using E3.ReactorManager.ParametersRecorderIntegrator.Data;

namespace E3.ReactorManager.ParametersRecorderIntegrator.Model
{
    public class ParametersRecorderIntegrator : IParametersRecorderIntegrator
    {
        public event EventHandler<ParametersRecorderEventArgs> RecordParametersNow;

        public void RecordParameters(string deviceId, string videoName, string videoTime)
        {
            RecordParametersNow?.Invoke(this, new ParametersRecorderEventArgs { DeviceId = deviceId, VideoName = videoName, VideoTime = videoTime });
        }
    }
}
