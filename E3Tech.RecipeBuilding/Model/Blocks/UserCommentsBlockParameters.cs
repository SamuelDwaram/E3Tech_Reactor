using Prism.Mvvm;
using System;

namespace E3Tech.RecipeBuilding.Model.Blocks
{
    public class UserCommentsBlockParameters : BindableBase, ICloneable
    {
        public string Name
        {
            get => "UserComments";
        }

        private string _uiLabel;
        public string UiLabel
        {
            get => _uiLabel ?? "UserComments";
            set => SetProperty(ref _uiLabel, value);
        }

        private string _started;
        public string Started
        {
            get => _started ?? bool.FalseString;
            set => SetProperty(ref _started, value);
        }

        private string _ended;
        public string Ended
        {
            get => _ended ?? bool.FalseString;
            set => SetProperty(ref _ended, value);
        }

        private string _startedTime;
        public string StartedTime
        {
            get => !string.IsNullOrWhiteSpace(_startedTime) ? _startedTime : "00:00";
            set => SetProperty(ref _startedTime, value);
        }

        private string _endedTime;
        public string EndedTime
        {
            get => !string.IsNullOrWhiteSpace(_endedTime) ? _endedTime : "00:00";
            set => SetProperty(ref _endedTime, value);
        }

        private string _enabled;
        public string Enabled
        {
            get => _enabled ?? (_enabled = bool.FalseString);
            set => SetProperty(ref _enabled, value);
        }

        private string _comments;
        public string Comments
        {
            get => _comments ?? (_comments = string.Empty);
            set => SetProperty(ref _comments, value);
        }

        public object Clone()
        {
            return new UserCommentsBlockParameters
            {
                UiLabel = this.UiLabel?.Clone().ToString(),
                Started = this.Started?.Clone().ToString(),
                StartedTime = this.StartedTime?.Clone().ToString(),
                Ended = this.Ended?.Clone().ToString(),
                EndedTime = this.EndedTime?.Clone().ToString(),
                Enabled = this.Enabled?.Clone().ToString(),

                Comments = this.Comments?.Clone().ToString(),
            };
        }
    }
}
