using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Anathem.Ui.ViewModels
{
    public class ReactorControlViewModel : BindableBase
    {
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;
        private readonly TaskScheduler taskScheduler;

        public ReactorControlViewModel(IFieldDevicesCommunicator fieldDevicesCommunicator)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.fieldDevicesCommunicator = fieldDevicesCommunicator;
            this.fieldDevicesCommunicator.FieldPointDataReceived += FieldDevicesCommunicator_FieldPointDataReceived;
            Task.Factory.StartNew(new Action(LoadDeviceParametersFromHardwareLayer))
                .ContinueWith(t => UpdateUi());
        }

        private void LoadDeviceParametersFromHardwareLayer()
        {
            foreach (SensorsDataSet sensorsDataSet in fieldDevicesCommunicator.GetFieldDeviceData(DeviceId).SensorsData)
            {
                foreach (FieldPoint fieldPoint in sensorsDataSet.SensorsFieldPoints)
                {
                    if (fieldPoint.TypeOfAddress == "FieldPoint")
                    {
                        if (ParameterDictionary.ContainsKey(fieldPoint.Label))
                        {
                            ParameterDictionary[fieldPoint.Label] = fieldPoint.Value;
                        }
                        else
                        {
                            ParameterDictionary.Add(fieldPoint.Label, fieldPoint.Value);
                        }
                    }
                }
            }
        }

        private void FieldDevicesCommunicator_FieldPointDataReceived(object sender, FieldPointDataReceivedArgs liveData)
        {
            if (ParameterDictionary.ContainsKey(liveData.FieldPointIdentifier))
            {
                ParameterDictionary[liveData.FieldPointIdentifier] = liveData.NewFieldPointData;
                UpdateUi();
            }
        }

        private void UpdateUi()
        {
            Task.Factory.StartNew(new Action(() => RaisePropertyChanged(nameof(ParameterDictionary))), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
        }

        public ICommand SendCommandToDevice
        {
            get => new DelegateCommand<string>(str => {
                string[] strArray = str.Split('|');
                fieldDevicesCommunicator.SendCommandToDevice(DeviceId, strArray[0], strArray[1], strArray[2]);
            });
        }

        public string DeviceId => "Reactor_1";
        public Dictionary<string, string> ParameterDictionary { get; set; } = new Dictionary<string, string>();
    }
}
