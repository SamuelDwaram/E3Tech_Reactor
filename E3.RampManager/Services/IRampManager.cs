using System.Collections.Generic;
using E3.RampManager.Models;

namespace E3.RampManager.Services
{
    public interface IRampManager
    {
        IList<Ramp> Ramps { get; }
        Ramp GetRamp(string deviceId, string fieldPointId);
        void SkipRampStep(string deviceId, string fieldPointId, int stepIndex);
        void StartRamp(string deviceId, string fieldPointId, IList<RampStep> rampSteps);
        void EndRamp(string deviceId, string fieldPointId);
        void ClearRamp(string deviceId, string fieldPointId);
        event UpdateRampStep UpdateRampStep;
        event UpdateRamp UpdateRamp;
    }

    public delegate void UpdateRampStep(string deviceId, string fieldPointId, int stepIndex, string propertyName, object value);
    public delegate void UpdateRamp(string deviceId, string fieldPointId, string propertyName, object value);
}
