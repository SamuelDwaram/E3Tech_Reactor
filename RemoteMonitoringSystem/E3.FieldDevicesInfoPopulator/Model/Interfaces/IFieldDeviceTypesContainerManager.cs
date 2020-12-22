using E3.FieldDevicesInfoPopulator.ViewModels;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using System.Collections.Generic;

namespace E3.FieldDevicesInfoPopulator.Model.Interfaces
{
    public interface IFieldDeviceTypesContainerManager
    {
        IList<FieldDevice> GetFieldDevicesData(string fieldDeviceType);

        Dictionary<string, IList<FieldDevice>> FieldDevicesData { get; }

        Dictionary<string, EachFieldDeviceTypeViewModel> FieldDeviceTypesContainer { get; }
    }
}
