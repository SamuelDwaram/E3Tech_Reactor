using E3.Bpu.Models;
using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace E3.Bpu.Services
{
    public class RegisteredDevicesHandler : IDevicesHandler
    {
        private readonly IDatabaseReader databaseReader;

        public RegisteredDevicesHandler(IDatabaseReader databaseReader)
        {
            this.databaseReader = databaseReader;
            Task.Factory.StartNew(new Func<RegisteredDevice>(GetRegisteredDevice))
                .ContinueWith(t => CurrentDevice = t.Result);
        }

        public RegisteredDevice GetRegisteredDevice()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST(Device which is running the application)  
            foreach (IPAddress iPAddress in Dns.GetHostAddresses(hostName).Where(ip => ip.AddressFamily == AddressFamily.InterNetwork))
            {
                DataRow result = databaseReader.ExecuteReadCommand($"select top(1) * from dbo.RegisteredDevices where IpAddress='{iPAddress}'", CommandType.Text).AsEnumerable().FirstOrDefault();
                if (result == null)
                {
                    continue;
                }
                else
                {
                    return new RegisteredDevice
                    {
                        DeviceType = (DeviceType)Enum.Parse(typeof(DeviceType), result.Field<string>("DeviceType")),
                        IpAddress = result.Field<string>("IpAddress"),
                        ModulesAccessible = GetModulesAccessible(result.Field<string>("DeviceType"))
                    };
                }
            }
            return new RegisteredDevice();
        }

        public IEnumerable<RegisteredDevice> GetAllRegisteredDevices()
        {
            return from DataRow dataRow in databaseReader.ExecuteReadCommand("select * from dbo.RegisteredDevices", CommandType.Text).AsEnumerable()
                    select new RegisteredDevice
                    {
                        IpAddress = dataRow.Field<string>("IpAddress"),
                        DeviceType = (DeviceType)Enum.Parse(typeof(DeviceType), dataRow.Field<string>("DeviceType")),
                        ModulesAccessible = GetModulesAccessible(dataRow.Field<string>("DeviceType"))
                    };
        }

        private IEnumerable<string> GetModulesAccessible(string deviceType)
        {
            return from DataRow dataRow in databaseReader.ExecuteReadCommand($"select ModuleAccessible from dbo.DevicesAndModulesConfiguration where DeviceType='{deviceType}'", CommandType.Text).AsEnumerable()
                   select dataRow.Field<string>("ModuleAccessible");
        }

        public RegisteredDevice CurrentDevice { get; set; }
    }
}
