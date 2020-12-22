using E3.Bpu.Models;
using System.Collections.Generic;

namespace E3.Bpu.Services
{
    public interface IDevicesHandler
    {
        RegisteredDevice CurrentDevice { get; set; }

        IEnumerable<RegisteredDevice> GetAllRegisteredDevices();
    }
}
