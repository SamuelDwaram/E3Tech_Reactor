using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.HardwareSimulator.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using E3.HardwareSimulator.Models;

namespace E3.HardwareSimulator.Services
{
    public class InMemoryHardwareSimulator : IHardwareSimulator
    {
        private readonly IDatabaseReader databaseReader;
        IList<FieldDevice> fieldDevices = new List<FieldDevice>();

        public InMemoryHardwareSimulator(IDatabaseReader databaseReader)
        {
            this.databaseReader = databaseReader;
            LoadFieldDevices();
        }

        private void LoadFieldDevices()
        {
            fieldDevices = PrepareFieldDevices();
        }

        private IList<FieldDevice> PrepareFieldDevices()
        {
            IList<FieldDevice> fieldDevicesList = new List<FieldDevice>();

            foreach (DataRow row in databaseReader.GetFieldDevicesList().AsEnumerable())
            {
                var fieldDevice = new FieldDevice
                {
                    Identifier = row["Identifier"].ToString(),
                    Label = row["Label"].ToString(),
                    Type = row["Type"].ToString(),
                    SensorsData = PrepareSensorsDataSetForFieldDevice(row["Identifier"].ToString()),
                };

                fieldDevicesList.Add(fieldDevice);
            }

            return fieldDevicesList;
        }
        private IList<SensorsDataSet> PrepareSensorsDataSetForFieldDevice(string fieldDeviceIdentifier)
        {
            IList<SensorsDataSet> sensorsDataSet = new List<SensorsDataSet>();

            foreach (DataRow row in databaseReader.GetSensorsDataSetInFieldDevice(fieldDeviceIdentifier).AsEnumerable())
            {
                var eachSensorsdataSet = new SensorsDataSet
                {
                    Identifier = row["Identifier"].ToString(),
                    Label = row["Label"].ToString(),
                    DataUnit = row["DataUnit"].ToString(),
                    SensorsFieldPoints = PrepareFieldPointsForSensorsDataSet(row["Identifier"].ToString())
                };

                sensorsDataSet.Add(eachSensorsdataSet);
            }

            return sensorsDataSet;
        }

        private IList<FieldPoint> PrepareFieldPointsForSensorsDataSet(string sensorsDataSetIdentifier)
        {
            IList<FieldPoint> fieldPoints = new List<FieldPoint>();

            foreach (DataRow row in databaseReader.GetFieldPointsInSensorsDataSet(sensorsDataSetIdentifier).AsEnumerable())
            {
                var fieldPoint = new FieldPoint
                {
                    Label = row["Label"].ToString(),
                    Description = row["Description"].ToString(),
                    TypeOfAddress = row["TypeOfAddress"].ToString(),
                    MemoryAddress = row["MemoryAddress"].ToString(),
                    FieldPointDataType = row["FieldPointDataType"].ToString(),
                    SensorDataSetIdentifier = row["SensorDataSetIdentifier"].ToString(),
                    RequireNotificationService = (row["RequireNotificationService"].ToString().Length > 0) ? bool.Parse(row["RequireNotificationService"].ToString()) : false,
                    ToBeLogged = (row["ToBeLogged"].ToString().Length > 0) ? bool.Parse(row["ToBeLogged"].ToString()) : false,
                };

                fieldPoints.Add(fieldPoint);
            }

            return fieldPoints;
        }

        public T ReadFieldPointValue<T>(string deviceId, string fieldPointLabel)
        {
            return TConverter.ChangeType<T>(
                fieldDevices.First(device => device.Identifier == deviceId).SensorsData.ToList()
                        .Where(sensorsDataSet =>
                            sensorsDataSet.SensorsFieldPoints
                                .Any(fieldPoint => fieldPoint.Label == fieldPointLabel)).First()
                                .SensorsFieldPoints.Where(fieldPoint => fieldPoint.Label == fieldPointLabel).First().Value);
        }

        public Dictionary<string, T> ReadFieldPointsInDataUnit<T>(string deviceId, string dataUnit)
        {
            return fieldDevices.First(device => device.Identifier == deviceId).SensorsData
                        .First(sensorsDataSet => sensorsDataSet.DataUnit == dataUnit)
                        .SensorsFieldPoints.ToDictionary(fieldPoint => fieldPoint.Label, 
                            fieldPoint => TConverter.ChangeType<T>(fieldPoint.Value ?? GetDefaultValue(fieldPoint.FieldPointDataType)));
        }

        private object GetDefaultValue(string dataType)
        {
            switch (dataType)
            {
                case "int":
                case "float":
                case "double":
                    return "0";
                case "string":
                    return string.Empty;
                default:
                    return default;
            }
        }

        public void UpdateFieldPointData(string deviceId, string fieldPointLabel, string data)
        {
            fieldDevices.First(device => device.Identifier == deviceId).SensorsData.ToList()
                .ForEach(sensorsDataSet => {
                    sensorsDataSet.SensorsFieldPoints.Where(fieldPoint => fieldPoint.Label == fieldPointLabel)
                        .ToList().ForEach(fieldPoint => fieldPoint.Value = data);
                });
        }

        public IList<DeviceParameter> GetDeviceParameters(string deviceId)
        {
            IList<DeviceParameter> deviceParameters = new List<DeviceParameter>();
            foreach (SensorsDataSet sensorsDataSet in fieldDevices.First(device => device.Identifier == deviceId).SensorsData)
            {
                ((List<DeviceParameter>)deviceParameters).AddRange(sensorsDataSet.SensorsFieldPoints
                        .Select(fieldPoint => new DeviceParameter {
                        Name = fieldPoint.Label,
                        Value = fieldPoint.Value
                        }).ToList());
            }
            return deviceParameters;
        }
    }
}
