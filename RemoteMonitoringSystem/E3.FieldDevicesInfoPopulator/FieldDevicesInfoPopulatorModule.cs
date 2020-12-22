using E3.FieldDevicesInfoPopulator.Model.Interfaces;
using E3.FieldDevicesInfoPopulator.Model.Implementations;
using E3.FieldDevicesInfoPopulator.ViewModels;
using E3.FieldDevicesInfoPopulator.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using E3.FieldDevicesInfoPopulator.Model;

namespace E3.FieldDevicesInfoPopulator
{
    public class FieldDevicesInfoPopulatorModule : IModule
    {
        IRegionManager regionManager;

        public FieldDevicesInfoPopulatorModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IFieldDeviceTypesContainerManager, FieldDeviceTypesContainerManager>();
            containerRegistry.RegisterSingleton<DeviceStatusToParametersViewNavigator>();
            containerRegistry.Register<EachFieldDeviceTypeViewModel>();
            containerRegistry.Register<FieldDeviceParameterInfoViewModel>();

            regionManager.RegisterViewWithRegion("FieldDevicesInfoPopulatorView", typeof(MultiFieldDeviceTypesView));
            regionManager.RegisterViewWithRegion("FieldDevicesStatusView", typeof(FieldDevicesStatusView));
        }
    }
}
