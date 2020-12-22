using System.Collections.Generic;

namespace E3.Bpu.Models
{
    public class RegisteredDevice
    {
        public string IpAddress { get; set; }
        public DeviceType DeviceType { get; set; } = DeviceType.HMI;
        public IEnumerable<string> ModulesAccessible { get; set; } = new List<string>();
    }

    /// <summary>
    /// Device Type
    /// </summary>
    public enum DeviceType
    {
        HMI,
        Tablet,
        RemoteView,
        WebBrowser,
        Unknown
    }
}
