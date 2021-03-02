using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace E3.RampManager.Models
{
    public class Ramp : BindableBase
    {
        public string DeviceId { get; set; } = string.Empty;
        public string FieldPointId { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
        public int NumberOfSteps { get; set; } = 0;
        public int CurrentStep { get; set; } = -1;
        public string RemainingTime { get; set; } = string.Empty;
        public IList<RampStep> Steps { get; set; } = new List<RampStep>();

        public void SetProperty(string deviceId, string fieldPointId, string propName, object value)
        {
            if (DeviceId == deviceId && FieldPointId == fieldPointId)
            {
                PropertyInfo propertyInfo = typeof(Ramp).GetProperty(propName);
                if (propertyInfo == null)
                {
                    // skip.
                }
                else
                {
                    propertyInfo.SetValue(this, TypeConverter.GetValue(value, propertyInfo.PropertyType));
                    RaisePropertyChanged(propName);
                }
            }
        }
    }

    public class RampStep : BindableBase
    {
        public int StepIndex { get; set; } = -1;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public int SetPoint { get; set; } = 0;
        public int MinsToMaintain { get; set; } = 0;

        public void UpdateRampStep(int stepIndex, string propName, object value)
        {
            if (stepIndex == StepIndex)
            {
                SetProperty(propName, value);
            }
            else
            {
                // skip.
            }
        }

        public void SetProperty(string propName, object value)
        {
            PropertyInfo propertyInfo = typeof(RampStep).GetProperty(propName);
            if (propertyInfo == null)
            {
                // skip.
            }
            else
            {
                propertyInfo.SetValue(this, TypeConverter.GetValue(value, propertyInfo.PropertyType));
                RaisePropertyChanged(propName);
            }
        }
    }

    public class TypeConverter
    {
        public static object GetValue(object value, Type targetType)
        {
            return targetType switch
            {
                Type type when type == typeof(int) => Convert.ToInt32(value),
                Type type when type == typeof(float) => Convert.ToSingle(value),
                Type type when type == typeof(double) => Convert.ToDouble(value),
                Type type when type == typeof(bool) => Convert.ToBoolean(value),
                _ => value
            };
        }
    }
}
