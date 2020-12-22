using System;

namespace E3.ReactorManager.ParametersRecorderIntegrator.Data
{
    public class ParametersRecorderEventArgs : EventArgs
    {
        public string DeviceId { get; set; }

        public string VideoName { get; set; }

        public string VideoTime { get; set; }
    }
}
