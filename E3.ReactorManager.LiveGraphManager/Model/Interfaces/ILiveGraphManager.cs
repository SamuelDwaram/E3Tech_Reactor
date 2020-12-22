using E3.ReactorManager.LiveGraphManager.ViewModels;
using System;
using System.Collections.Generic;

namespace E3.ReactorManager.LiveGraphManager.Model.Interfaces
{
    public interface ILiveGraphManager
    {
        event UpdateLiveGraphEventHandler UpdateLiveGraph;

        void SetParameters(string deviceId, List<string> requiredLiveTrendsParameters);

        void UpdateNow();
    }

    public delegate void UpdateLiveGraphEventHandler(object sender, UpdateLiveGraphEventArgs e);

    public class UpdateLiveGraphEventArgs : EventArgs
    {
        public string DeviceId { get; set; }

        public Dictionary<string, dynamic> Data { get; set; }
    }
}
