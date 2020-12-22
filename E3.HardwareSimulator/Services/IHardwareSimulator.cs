using E3.HardwareSimulator.Models;
using System.Collections.Generic;

namespace E3.HardwareSimulator.Services
{
    public interface IHardwareSimulator
    {
        Dictionary<string, T> ReadFieldPointsInDataUnit<T>(string deviceId, string dataUnit);

        T ReadFieldPointValue<T>(string deviceId, string fieldPointLabel);

        void UpdateFieldPointData(string deviceId, string fieldPointLabel, string data);

        IList<DeviceParameter> GetDeviceParameters(string deviceId);
    }
}
