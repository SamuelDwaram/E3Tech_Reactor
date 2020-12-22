using System;
using E3.ReactorManager.ParametersRecorderIntegrator.Data;

namespace E3.ReactorManager.ParametersRecorderIntegrator.Model
{
    public interface IParametersRecorderIntegrator
    {
        void RecordParameters(string deviceId, string videoName, string videoTime);

        event EventHandler<ParametersRecorderEventArgs> RecordParametersNow;
    }
}
