using Prism.Mvvm;
using System;

namespace E3Tech.RecipeBuilding.Model.Blocks
{
    public class TransferBlockParameters : BindableBase, ICloneable
    {
        public string Name
        {
            get => "Transfer";
        }

        private string _uiLabel;
        public string UiLabel
        {
            get => _uiLabel ?? "Transfer";
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

        private string _volume;
        public string Volume
        {
            get => _volume;
            set
            {
                _volume = value;
                RaisePropertyChanged();
            }
        }

        private string _sourceItemIndex;
        public string SourceItemIndex
        {
            get => _sourceItemIndex;
            set
            {
                _sourceItemIndex = value;
                RaisePropertyChanged();
            }
        }

        private string _targetItemIndex;
        public string TargetItemIndex
        {
            get => _targetItemIndex;
            set
            {
                _targetItemIndex = value;
                RaisePropertyChanged();
            }
        }

        public object Clone()
        {
            return new TransferBlockParameters()
            {
                UiLabel = this.UiLabel?.Clone().ToString(),
                Started = this.Started?.Clone().ToString(),
                StartedTime = this.StartedTime?.Clone().ToString(),
                Ended = this.Ended?.Clone().ToString(),
                EndedTime = this.EndedTime?.Clone().ToString(),
                Enabled = this.Enabled?.Clone().ToString(),

                SourceItemIndex = this.SourceItemIndex?.Clone().ToString(),
                TargetItemIndex = this.TargetItemIndex?.Clone().ToString(),
                Volume = this.Volume?.Clone().ToString(),
            };
        }
    }
}
