using E3.ReactorManager.EquipmentUsageTracker.Model.Data;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using System.Collections.Generic;

namespace E3.ReactorManager.EquipmentUsageTracker.Model.Interfaces
{
    public interface IEquipmentUsageTracker
    {
        void LogEquipmentUsage(FieldPointDataReceivedArgs fieldPointDataChangedArgs);

        IList<OccupancyReportArgs> GetOccupancyData(string fieldDeviceIdentifier, IList<string> equipmentIdentifierList, string month, OccupancyReportTypeEnum occupancyReportTypeEnum);

        IList<EquipmentUsageLogArgs> GetEquipmentUsageLogData(string fieldDeviceIdentifier);
    }
}
