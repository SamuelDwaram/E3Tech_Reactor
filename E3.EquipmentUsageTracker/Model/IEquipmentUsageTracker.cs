using E3.EquipmentUsageTracker.Model.Data;
using E3.EquipmentUsageTracker.Model.Enums;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using System.Collections.Generic;

namespace E3.EquipmentUsageTracker.Model
{
    public interface IEquipmentUsageTracker
    {
        event UpdateEquipmentOccupancyEventHandler UpdateEquipmentOccupancyEvent;

        void PublishUpdateEquipmentOccupancyEvent(string month, OccupancyReportTypeEnum occupancyReportTypeEnum);

        IList<EquipmentAndConnectedFieldDeviceArgs> GetAvailableEquipments();

        void LogEquipmentUsage(FieldPointDataReceivedArgs fieldPointDataReceivedArgs);

        IList<EquipmentUsageLogArgs> GetEquipmentUsageLog(string fieldDeviceIdentifier);

        EquipmentOccupancyArgs GetEquipmentOccupancy(string fieldDeviceIdentifier, string equipmentIdentifier, string month, OccupancyReportTypeEnum occupancyReportTypeEnum);
    }

    public delegate void UpdateEquipmentOccupancyEventHandler(string selectedMonth, OccupancyReportTypeEnum occupancyReportTypeEnum);
}
