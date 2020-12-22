using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Magar.Ui.Models
{
    public class Tank : BindableBase
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _label;
        public string Label
        {
            get { return _label; }
            set { SetProperty(ref _label, value); }
        }

        private string _value="0";
        public string Value
        {
            get { return _value; }
            set 
            {
                SetProperty(ref _value, value);
                CheckLimits(Convert.ToSingle(value));
            }
        }

        private string _units;
        public string Units
        {
            get { return _units; }
            set { SetProperty(ref _units, value); }
        }

        public string AlarmLimitsString
        {
            get => string.Empty;
            set
            {
                if (value.Contains('|'))
                {
                    string[] limits = value.Split('|').ToArray();
                    AlarmLimits[0] = Convert.ToSingle(limits[0]);
                    AlarmLimits[1] = Convert.ToSingle(limits[1]);
                }
            }
        }

        public IList<float> AlarmLimits { get; set; } = new List<float> { 0, 0 };  // Lower limit=>Index=0 HigherLimit=>Index=1

        private Brush _foreground = Brushes.White;
        public Brush Foreground
        {
            get { return _foreground; }
            set { SetProperty(ref _foreground, value); }
        }

        private void CheckLimits(float value)
        {
            if (value >= AlarmLimits.ElementAt(0) && value<= AlarmLimits.ElementAt(1))
            {
                Foreground = Brushes.White;
            }
            else if (value < AlarmLimits.ElementAt(0))
            {
                Foreground = Brushes.Orange;
            }
            else
            {
                Foreground = Brushes.Red;
            }
        }
    }
}
