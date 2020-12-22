using E3.FieldDevicesInfoPopulator.Model.Interfaces;
using E3.FieldDevicesInfoPopulator.ViewModels;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using System.Collections.Generic;
using Unity;

namespace E3.FieldDevicesInfoPopulator.Model.Implementations
{
    public class FieldDeviceTypesContainerManager : IFieldDeviceTypesContainerManager
    {
        Dictionary<string, EachFieldDeviceTypeViewModel> fieldDeviceTypesContainer;
        Dictionary<string, IList<FieldDevice>> fieldDevicesData;
        IFieldDevicesCommunicator fieldDevicesCommunicator;
        IUnityContainer containerProvider;

        public FieldDeviceTypesContainerManager(IUnityContainer containerProvider, IFieldDevicesCommunicator fieldDevicesCommunicator)
        {
            this.containerProvider = containerProvider;
            this.fieldDevicesCommunicator = fieldDevicesCommunicator;
            fieldDeviceTypesContainer = new Dictionary<string, EachFieldDeviceTypeViewModel>();
            fieldDevicesData = new Dictionary<string, IList<FieldDevice>>();
            PrepareFieldDeviceData(fieldDevicesCommunicator.GetAllFieldDevicesData());
            PrepareFieldDeviceTypesContainer();
        }

        private void PrepareFieldDeviceTypesContainer()
        {
            foreach (var keyValuePair in fieldDevicesData)
            {
                var vm = containerProvider.Resolve<EachFieldDeviceTypeViewModel>();
                vm.FieldDeviceType = keyValuePair.Key;
                vm.Update(keyValuePair.Key);
                fieldDeviceTypesContainer.Add(keyValuePair.Key, vm);
            }
        }

        private void PrepareFieldDeviceData(IList<FieldDevice> fieldDevices)
        {
            foreach (var fieldDevice in fieldDevices)
            {
                if (fieldDevicesData.ContainsKey(fieldDevice.Type))
                {
                    fieldDevicesData[fieldDevice.Type].Add(fieldDevice);
                }
                else
                {
                    fieldDevicesData[fieldDevice.Type] = new List<FieldDevice> { fieldDevice };
                }
            }
        }

        public IList<FieldDevice> GetFieldDevicesData(string fieldDeviceType)
        {
            if (fieldDevicesData.ContainsKey(fieldDeviceType))
            {
                return FieldDevicesData[fieldDeviceType];
            }

            return new List<FieldDevice>();
        }

        public Dictionary<string, IList<FieldDevice>> FieldDevicesData => fieldDevicesData;

        public Dictionary<string, EachFieldDeviceTypeViewModel> FieldDeviceTypesContainer => fieldDeviceTypesContainer;
    }
}
