using E3.FieldDevicesInfoPopulator.Model;
using E3.FieldDevicesInfoPopulator.Model.Interfaces;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace E3.FieldDevicesInfoPopulator.ViewModels
{
    public class MultiFieldDeviceTypesViewModel : BindableBase
    {
        IFieldDeviceTypesContainerManager fieldDeviceTypesContainerManager;
        DeviceStatusToParametersViewNavigator parametersViewNavigator;

        public MultiFieldDeviceTypesViewModel(IUnityContainer containerProvider)
        {
            FieldDevicesContainer = new Dictionary<string, EachFieldDeviceTypeViewModel>();
            fieldDeviceTypesContainerManager = containerProvider.Resolve<IFieldDeviceTypesContainerManager>();

            parametersViewNavigator = containerProvider.Resolve<DeviceStatusToParametersViewNavigator>();
            parametersViewNavigator.NavigateToParametersView += ParametersViewNavigator_NavigateToParametersView;
            LoadFieldDevicesContainer();
        }

        private void ParametersViewNavigator_NavigateToParametersView(object sender, string deviceType)
        {
            SelectedTabItemIndex = FieldDevicesContainer.ContainsKey(deviceType) ? FieldDevicesContainer.Keys.ToList().IndexOf(deviceType) : 0;
        }

        private void LoadFieldDevicesContainer()
        {
            foreach (var keyValuePair in fieldDeviceTypesContainerManager.FieldDeviceTypesContainer)
            {
                FieldDevicesContainer.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        #region Properties
        public Dictionary<string, EachFieldDeviceTypeViewModel> FieldDevicesContainer { get; }

        private int _selectedTabItemIndex;
        public int SelectedTabItemIndex
        {
            get => _selectedTabItemIndex;
            set => SetProperty(ref _selectedTabItemIndex, value);
        }
        #endregion
    }
}
