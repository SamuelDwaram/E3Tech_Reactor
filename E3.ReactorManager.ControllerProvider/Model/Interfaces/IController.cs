using System.Collections.Generic;

namespace E3.ReactorManager.ControllerProvider.Model.Interfaces
{
    public interface IController
    {
        string Label { get; set; }

        string Identifier { get; set; }

        string Address { get; set; }

        string PortNumber { get; set; }

        string ProviderName { get; set; }

        int ResponseTime { get; set; }

        IList<string> UsedMemoryAddresses { get; set; }

        void Connect();

        bool IsConnected();

        Dictionary<string, ushort> Read(string memoryHandle);
        
        bool Write(string writeMemoryInfo, object writeData);

        void Disconnect();
    }
}
