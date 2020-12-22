using Prism.Mvvm;
using System;

namespace E3Tech.RecipeBuilding.Model.Blocks
{
    public class DosingBlockParameters : BindableBase, ICloneable
    {
        public string Name
        {
            get => "Dosing";
        }

        private string _uiLabel;
        public string UiLabel
        {
            get => _uiLabel ?? "Dosing";
            set
            {
                _uiLabel = value;
                RaisePropertyChanged();
            }
        }

        private string _started;
        public string Started
        {
            get => _started ?? bool.FalseString;
            set
            {
                _started = value;
                RaisePropertyChanged();
            }
        }

        private string _ended;
        public string Ended
        {
            get => _ended ?? bool.FalseString;
            set
            {
                _ended = value;
                RaisePropertyChanged();
            }
        }

        private string _startedTime;
        public string StartedTime
        {
            get => !string.IsNullOrWhiteSpace(_startedTime) ? _startedTime : "00:00";
            set
            {
                _startedTime = value;
                RaisePropertyChanged();
            }
        }

        private string _endedTime;
        public string EndedTime
        {
            get => !string.IsNullOrWhiteSpace(_endedTime) ? _endedTime : "00:00";
            set
            {
                _endedTime = value;
                RaisePropertyChanged();
            }
        }

        private string _enabled;
        public string Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                RaisePropertyChanged();
            }
        }

        private string _maxAmount;
        public string MaxAmount
        {
            get { return _maxAmount; }
            set
            {
                _maxAmount = value;
                RaisePropertyChanged();
            }
        }

        private string _remainingDosableAmount;
        public string RemainingDosableAmount
        {
            get => _remainingDosableAmount ?? (_remainingDosableAmount = "0");
            set => SetProperty(ref _remainingDosableAmount, value);
        }

        private string _stopTemperature;
        public string StopTemperature
        {
            get { return _stopTemperature; }
            set
            {
                _stopTemperature = value;
                RaisePropertyChanged();
            }
        }
        private string _resumeTemperature;
        public string ResumeTemperature
        {
            get { return _resumeTemperature; }
            set
            {
                _resumeTemperature = value;
                RaisePropertyChanged();
            }
        }
        private string _setPoint;
        public string SetPoint
        {
            get { return _setPoint; }
            set
            {
                _setPoint = value;
                RaisePropertyChanged();
            }
        }
        private string _minRate;
        public string MinRate
        {
            get { return _minRate; }
            set
            {
                _minRate = value;
                RaisePropertyChanged();
            }
        }
        private string _maxRate;
        public string MaxRate
        {
            get { return _maxRate; }
            set
            {
                _maxRate = value;
                RaisePropertyChanged();
            }
        }
        private string _settlingTime;
        public string SettlingTime
        {
            get { return _settlingTime; }
            set
            {
                _settlingTime = value;
                RaisePropertyChanged();
            }
        }
        private string _minPh;
        public string MinPh
        {
            get { return _minPh; }
            set
            {
                _minPh = value;
                RaisePropertyChanged();
            }
        }
        private string _maxPh;
        public string MaxPh
        {
            get { return _maxPh; }
            set
            {
                _maxPh = value;
                RaisePropertyChanged();
            }
        }
        private string _operatingMode;
        public string OperatingMode
        {
            get { return _operatingMode; }
            set
            {
                _operatingMode = value;
                RaisePropertyChanged();
            }
        }
        
        public object Clone()
        {
            return new DosingBlockParameters()
            {
                UiLabel = this.UiLabel?.Clone().ToString(),
                Started = this.Started?.Clone().ToString(),
                StartedTime = this.StartedTime?.Clone().ToString(),
                Ended = this.Ended?.Clone().ToString(),
                EndedTime = this.EndedTime?.Clone().ToString(),
                Enabled = this.Enabled?.Clone().ToString(),

                MaxAmount = this.MaxAmount?.Clone().ToString(),
                RemainingDosableAmount = this.RemainingDosableAmount?.Clone().ToString(),
                StopTemperature = this.StopTemperature?.Clone().ToString(),
                ResumeTemperature = this.ResumeTemperature?.Clone().ToString(),
                SetPoint = this.SetPoint?.Clone().ToString(),
                MinRate = this.MinRate?.Clone().ToString(),
                MaxRate = this.MaxRate?.Clone().ToString(),
                SettlingTime = this.SettlingTime?.Clone().ToString(),
                MinPh = this.MinPh?.Clone().ToString(),
                MaxPh = this.MaxPh?.Clone().ToString(),
                OperatingMode = this.OperatingMode?.Clone().ToString(),
            };
        }
    }
}
