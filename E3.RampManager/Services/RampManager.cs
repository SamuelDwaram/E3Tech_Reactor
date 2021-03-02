using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using E3.RampManager.Models;
using System.Reflection;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Threading;

namespace E3.RampManager.Services
{
    public class RampManager : IRampManager
    {
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;

        public event UpdateRampStep UpdateRampStep;
        public event UpdateRamp UpdateRamp;

        public RampManager(IFieldDevicesCommunicator fieldDevicesCommunicator)
        {
            this.fieldDevicesCommunicator = fieldDevicesCommunicator;
            this.fieldDevicesCommunicator.FieldPointDataReceived += (sender, liveData) => OnLiveDataReceived(liveData);
            Task.Run(() => LoadRampsOfAllDevices(fieldDevicesCommunicator.GetAllFieldDevicesData()));
        }

        private void LoadRampsOfAllDevices(IList<FieldDevice> fieldDevices)
        {
            foreach (FieldDevice fieldDevice in fieldDevices)
            {
                List<FieldPoint> fieldPoints = new List<FieldPoint>();
                fieldDevice.SensorsData.Select(sensorsDataSet => sensorsDataSet.SensorsFieldPoints.Where(fp => fp.TypeOfAddress == "Ramp" && fp.Value == bool.TrueString))
                    .ToList().ForEach(fp => {
                        fieldPoints.AddRange(fp);
                    });
            }
        }

        private void OnLiveDataReceived(FieldPointDataReceivedArgs liveData)
        {
            /*
             Fieldpoint syntax : <field point id>|<ramp step parameter>|<step index>
                example :  Stirrer|StartTime|1
             */
            if (liveData.FieldPointType == "Ramp" && Ramps.Any(r => r.DeviceId == liveData.FieldDeviceIdentifier))
            {
                string[] rampInfo = liveData.FieldPointIdentifier.Split('|');
                string fieldPointId = rampInfo[0];
                string propertyName = rampInfo[1];
                if (rampInfo.Length > 2)
                {
                    //Live Data belongs to Ramp step
                    int stepIndex = Convert.ToInt32(rampInfo[2]);
                    Ramp ramp = Ramps.FirstOrDefault(r => r.DeviceId == liveData.FieldDeviceIdentifier && r.FieldPointId == fieldPointId);
                    if (ramp != null && ramp.Steps.Count > stepIndex)
                    {
                        RampStep rampStep = ramp.Steps.ElementAt(stepIndex);
                        PropertyInfo propertyInfo = typeof(RampStep).GetProperty(propertyName);
                        if (propertyInfo == null)
                        {
                            Console.WriteLine($"Property {propertyName} not found in RampStep class");
                        }
                        else
                        {
                            typeof(RampStep).GetProperty(propertyName).SetValue(rampStep, GetValue(liveData.NewFieldPointData, propertyInfo.PropertyType));
                            UpdateRampStep?.BeginInvoke(liveData.FieldDeviceIdentifier, fieldPointId, stepIndex, propertyInfo.Name, liveData.NewFieldPointData, null, null);
                        }
                    }
                }
                else
                {
                    //Live Data belongs to Ramp
                    PropertyInfo propertyInfo = typeof(Ramp).GetProperty(propertyName);
                    Ramp ramp = Ramps.FirstOrDefault(r => r.DeviceId == liveData.FieldDeviceIdentifier && r.FieldPointId == fieldPointId);
                    if (propertyInfo == null)
                    {
                        Console.WriteLine($"Property {propertyName} not found in Ramp class");
                    }
                    else
                    {
                        typeof(Ramp).GetProperty(propertyName).SetValue(ramp, GetValue(liveData.NewFieldPointData, propertyInfo.PropertyType));
                        UpdateRamp?.BeginInvoke(liveData.FieldDeviceIdentifier, fieldPointId, propertyName, liveData.NewFieldPointData, null, null);
                    }
                }
            }
        }

        public object GetValue(object value, Type targetType)
        {
            return targetType switch
            {
                Type type when type == typeof(int) => Convert.ToInt32(value),
                Type type when type == typeof(bool) => Convert.ToBoolean(value),
                _ => value
            };
        }

        public void StartRamp(string deviceId, string fieldPointId, IList<RampStep> rampSteps)
        {
            //clear any previous ramp
            ClearRamp(deviceId, fieldPointId);

            //start writing new ramp
            foreach (RampStep rampStep in rampSteps)
            {
                fieldDevicesCommunicator.SendCommandToDevice(deviceId, $"{fieldPointId}|{nameof(rampStep.SetPoint)}|{rampStep.StepIndex}", "int", rampStep.SetPoint.ToString());
                fieldDevicesCommunicator.SendCommandToDevice(deviceId, $"{fieldPointId}|{nameof(rampStep.MinsToMaintain)}|{rampStep.StepIndex}", "int", rampStep.MinsToMaintain.ToString());
            }

            fieldDevicesCommunicator.SendCommandToDevice(deviceId, $"{fieldPointId}|NumberOfSteps", "int", rampSteps.Count.ToString());
            fieldDevicesCommunicator.SendCommandToDevice(deviceId, $"{fieldPointId}|Status", "bool", bool.TrueString);

            Ramps.Add(new Ramp { DeviceId = deviceId, FieldPointId = fieldPointId, Steps = rampSteps });
        }

        public void ClearRamp(string deviceId, string fieldPointId)
        {
            fieldDevicesCommunicator.SendCommandToDevice(deviceId, $"{fieldPointId}|ClearRamp", "bool", bool.TrueString);
            Ramps.ToList().RemoveAll(r => r.DeviceId == deviceId && r.FieldPointId == fieldPointId);
        }

        public Ramp GetRamp(string deviceId, string fieldPointId)
        {
            return Ramps.FirstOrDefault(r => r.DeviceId == deviceId && r.FieldPointId == fieldPointId) ?? new Ramp { DeviceId = deviceId, FieldPointId = fieldPointId };
        }

        public void EndRamp(string deviceId, string fieldPointId)
        {
            fieldDevicesCommunicator.SendCommandToDevice(deviceId, $"{fieldPointId}|Status", "bool", bool.FalseString);
        }

        public void SkipRampStep(string deviceId, string fieldPointId, int stepIndex)
        {
            if (Ramps.First(r => r.DeviceId == deviceId && r.FieldPointId == fieldPointId).Steps.Count > stepIndex)
            {
                fieldDevicesCommunicator.SendCommandToDevice(deviceId, $"{fieldPointId}|SkipStep", "bool", bool.TrueString);
            }
        }

        public IList<Ramp> Ramps { get; } = new List<Ramp>();
    }
}
