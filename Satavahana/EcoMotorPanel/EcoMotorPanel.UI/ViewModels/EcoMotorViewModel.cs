using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EcoMotorPanel.UI.ViewModels
{
    public class EcoMotorViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;
        private readonly IList<PropertyInfo> existingProperties;
        private readonly TaskScheduler taskScheduler;

        public EcoMotorViewModel(IRegionManager regionManager, IFieldDevicesCommunicator fieldDevicesCommunicator)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            existingProperties = new List<PropertyInfo>(GetType().GetProperties());
            this.regionManager = regionManager;
            this.fieldDevicesCommunicator = fieldDevicesCommunicator;
            this.fieldDevicesCommunicator.FieldPointDataReceived += FieldDevicesCommunicator_FieldPointDataReceived;

        }

        private void FieldDevicesCommunicator_FieldPointDataReceived(object sender, FieldPointDataReceivedArgs fieldPointDataChangedArgs)
        {
            if (fieldPointDataChangedArgs.FieldDeviceIdentifier == DeviceId)
            {
                var liveDataEventArgs = new LiveDataEventArgs
                {
                    PropertyInfoIdentifier = fieldPointDataChangedArgs.FieldPointDescription,
                    LiveData = fieldPointDataChangedArgs.NewFieldPointData,
                };

                Task.Factory.StartNew(new Func<object, LiveDataEventArgs>(ValidateLiveDataReceived), liveDataEventArgs)
                    .ContinueWith(new Action<Task<LiveDataEventArgs>>(UpdatePropertyValue), taskScheduler);
            }
        }

        #region Live Data Handlers
        private void UpdatePropertyValue(Task<LiveDataEventArgs> task)
        {
            var liveDataEventArgs = task.Result;

            if (liveDataEventArgs != null && liveDataEventArgs.PropertyInfo != null && liveDataEventArgs.LiveData != null)
            {
                liveDataEventArgs.PropertyInfo
                                    .SetValue(this, liveDataEventArgs.LiveData == "NC" ? null : liveDataEventArgs.LiveData, null);
                Console.WriteLine($"{liveDataEventArgs.PropertyInfoIdentifier} : {liveDataEventArgs.LiveData}");
            }
        }

        private void OnLiveDataReceived(object sender, FieldPointDataReceivedArgs fieldPointDataChangedArgs)
        {
            if (fieldPointDataChangedArgs.FieldDeviceIdentifier == DeviceId)
            {
                var liveDataEventArgs = new LiveDataEventArgs
                {
                    PropertyInfoIdentifier = fieldPointDataChangedArgs.FieldPointDescription,
                    LiveData = fieldPointDataChangedArgs.NewFieldPointData,
                };

                Task.Factory.StartNew(new Func<object, LiveDataEventArgs>(ValidateLiveDataReceived), liveDataEventArgs)
                    .ContinueWith(new Action<Task<LiveDataEventArgs>>(UpdatePropertyValue), taskScheduler);
            }
        }

        private LiveDataEventArgs ValidateLiveDataReceived(object liveData)
        {
            if (liveData != null && existingProperties != null)
            {
                var liveDataEventArgs = (LiveDataEventArgs)liveData;

                liveDataEventArgs.PropertyInfo = existingProperties.FirstOrDefault(property => property.Name == liveDataEventArgs.PropertyInfoIdentifier);

                return liveDataEventArgs;
            }

            return null;
        }
        #endregion
        #region Update Field device data initially
        private FieldDevice GetFieldDeviceData(object deviceId)
        {
            return fieldDevicesCommunicator.GetFieldDeviceData((string)deviceId);
        }

        private void UpdateFieldDeviceData(Task<FieldDevice> obj)
        {
            var fieldDeviceData = obj.Result;

            if (fieldDeviceData != null)
            {
                foreach (var sensorDataSet in fieldDeviceData.SensorsData)
                {
                    foreach (var fieldPoint in sensorDataSet.SensorsFieldPoints)
                    {
                        var liveDataEventArgs = new LiveDataEventArgs
                        {
                            PropertyInfoIdentifier = fieldPoint.Label,
                            LiveData = fieldPoint.Value,
                        };

                        Task.Factory.StartNew(new Func<object, LiveDataEventArgs>(ValidateLiveDataReceived), liveDataEventArgs)
                            .ContinueWith(new Action<Task<LiveDataEventArgs>>(UpdatePropertyValue), taskScheduler);
                        return;
                    }
                }
            }
        }
        #endregion
        public void Navigate(string pageName)
        {
            regionManager.RequestNavigate("DynamicView", pageName);
        }

        private void ShutDownApp()
        {
            Application.Current.Shutdown();
        }


        #region Commands
        private ICommand _navigateCommand;
        public ICommand NavigateCommand
        {
            get => _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(Navigate));
            set => SetProperty(ref _navigateCommand, value);
        }

        private ICommand _shutDownAppCommand;
        public ICommand ShutDownAppCommand
        {
            get => _shutDownAppCommand ?? (_shutDownAppCommand = new DelegateCommand(ShutDownApp));
            set => _shutDownAppCommand = value;
        }
        #endregion


        #region Properties
        private string _deviceId;
        public string DeviceId
        {
            get => _deviceId ?? "Motor_2";
            set => SetProperty(ref _deviceId, value);
        }
        #endregion

    }
}
