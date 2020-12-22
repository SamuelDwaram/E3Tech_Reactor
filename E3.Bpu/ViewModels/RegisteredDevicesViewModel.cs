using E3.Bpu.Models;
using E3.Bpu.Services;
using System.Collections.Generic;

namespace E3.Bpu.ViewModels
{
    public class RegisteredDevicesViewModel
    {
        private readonly IDevicesHandler devicesHandler;

        public RegisteredDevicesViewModel(IDevicesHandler devicesHandler)
        {
            this.devicesHandler = devicesHandler;
        }

        public IEnumerable<RegisteredDevice> RegisteredDevices => devicesHandler.GetAllRegisteredDevices();
    }
}
