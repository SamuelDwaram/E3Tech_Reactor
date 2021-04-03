using E3.Mediator.Services;
using E3.Mediator.Models;
using Prism.Commands;
using Prism.Mvvm;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using System.Windows.Input;
using E3.RampManager.Services;
using E3.RampManager.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace E3.RampManager.ViewModels
{
    public class RampViewModel : BindableBase
    {
        private readonly MediatorService mediatorService;
        private readonly IRampManager rampManager;

        public RampViewModel(MediatorService mediatorService, IRampManager rampManager)
        {
            this.mediatorService = mediatorService;
            this.rampManager = rampManager;
            this.rampManager.UpdateRampStep += RampManager_UpdateRampStep;
            this.rampManager.UpdateRamp += RampManager_UpdateRamp;
            this.mediatorService.Register(InMemoryMediatorMessageContainer.UpdateSelectedDeviceId, d => {
                Device = d as Device;
            });
        }

        public void SetFieldPointId(string fpId)
        {
            FieldPointId = fpId;
            RaisePropertyChanged(nameof(FieldPointId));
        }

        public void LoadRampIfExists()
        {
            LoadRampIfExists(Device.Id, FieldPointId);
        }

        private void RampManager_UpdateRamp(string deviceId, string fieldPointId, string propertyName, object value)
        {
            Ramp.SetProperty(deviceId, fieldPointId, propertyName, value);
        }

        private void LoadRampIfExists(string deviceId, string fieldPointId)
        {
            Ramp = rampManager.GetRamp(deviceId, fieldPointId);
            RaisePropertyChanged(nameof(Ramp));
            Steps.Clear();
            Steps = new ObservableCollection<RampStep>(Ramp.Steps);
        }

        private void RampManager_UpdateRampStep(string deviceId, string fieldPointId, int stepIndex, string propertyName, object value)
        {
            if (Device.Id == deviceId && fieldPointId == FieldPointId)
            {
                Steps[stepIndex].UpdateRampStep(stepIndex, propertyName, value);
            }
        }

        #region Commands
        public ICommand RemoveRampStepCommand
        {
            get => new DelegateCommand<object>(obj => {
                int index = Convert.ToInt32(obj);
                //Update the step indexes for the remaining steps
                for (int counter = index+1; counter < Steps.Count; counter++)
                {
                    Steps[counter].SetProperty(nameof(RampStep.StepIndex), counter-1);
                }
                Steps.RemoveAt(index);
            });
        }
        public ICommand SkipRampStepCommand
        {
            get => new DelegateCommand<object>(obj => {
                Task.Factory.StartNew(() => rampManager.SkipRampStep(Device.Id, FieldPointId, Convert.ToInt32(obj)));
            });
        }
        public ICommand AddRampStepCommand
        {
            get => new DelegateCommand<object>(stepsCount => {
                if (Convert.ToInt32(stepsCount) < 7)
                {
                    Steps.Add(new RampStep { StepIndex = Convert.ToInt32(stepsCount) });
                    RaisePropertyChanged(nameof(Steps));
                }
            });
        }
        public ICommand StartRampCommand
        {
            get => new DelegateCommand(() => rampManager.StartRamp(Device.Id, FieldPointId, Steps.ToList()));
        }
        public ICommand EndRampCommand
        {
            get => new DelegateCommand(() => rampManager.EndRamp(Device.Id, FieldPointId));
        }
        public ICommand ClearRampCommand
        {
            get => new DelegateCommand(() => Steps.Clear());
        }
        #endregion

        #region Properties
        public string FieldPointId { get; private set; }
        public ObservableCollection<RampStep> Steps { get; set; } = new ObservableCollection<RampStep>();
        public Ramp Ramp { get; set; } = new Ramp();
        public Device Device { get; set; } = new Device();
        #endregion
    }
}
