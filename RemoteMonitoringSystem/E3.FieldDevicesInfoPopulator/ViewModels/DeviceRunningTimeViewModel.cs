using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.Interfaces.DataAbstractionLayer.Data;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Timer = System.Timers.Timer;

namespace E3.FieldDevicesInfoPopulator.ViewModels
{
    public class DeviceRunningTimeViewModel : BindableBase
    {
        private readonly IDatabaseReader databaseReader;
        private readonly TaskScheduler taskScheduler;
        private readonly Timer runningTimeUpdater = new Timer(TimeSpan.FromSeconds(30).TotalMilliseconds);

        public DeviceRunningTimeViewModel(IDatabaseReader databaseReader)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.databaseReader = databaseReader;
            runningTimeUpdater.Elapsed += (sender, args) => UpdateRunningTime();
            Task.Factory.StartNew(new Action(() => runningTimeUpdater.Start()));
        }

        private void UpdateRunningTime()
        {
            Task.Factory.StartNew(new Func<int>(() =>
            {
                return string.IsNullOrWhiteSpace(DeviceId) ? 0 : databaseReader.ExecuteReadCommand("GetRunningTime", CommandType.StoredProcedure, new List<DbParameterInfo> {
                        new DbParameterInfo("@DeviceId", DeviceId, DbType.String)
                    }).AsEnumerable().First().Field<int>(0);
            })).ContinueWith(new Func<Task<int>, string>(t => {
                int hours = t.Result / 60;
                int minutes = t.Result % 60;
                return $"{(Convert.ToString(hours).Length < 2 ? $"0{hours}" : $"{hours}")} H : {(Convert.ToString(minutes).Length < 2 ? $"0{minutes}" : $"{minutes}")} M";
            })).ContinueWith(new Action<Task<string>>(t => RunningTime = t.Result), taskScheduler);
        }

        public ICommand SetDeviceIdCommand
        {
            get => new DelegateCommand<string>(deviceId => {
                DeviceId = deviceId;
                UpdateRunningTime();
            });
        }

        #region Properties
        private string _deviceId;
        public string DeviceId
        {
            get { return _deviceId; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    // skip.
                }
                else
                {
                    SetProperty(ref _deviceId, value);
                    RaisePropertyChanged(nameof(DeviceId));
                }
            }
        }

        private string _runningTime = "00:00";
        public string RunningTime
        {
            get { return _runningTime; }
            set { SetProperty(ref _runningTime, value); }
        }
        #endregion
    }
}
