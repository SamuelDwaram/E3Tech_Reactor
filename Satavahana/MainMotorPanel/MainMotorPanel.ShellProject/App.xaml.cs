using MainMotorPanel.ShellProject.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity.Ioc;
using System;
using System.Windows;

namespace MainMotorPanel.ShellProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private IRegionManager regionManager;

        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            OnExit(null);
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            OnExit(null);
        }

        protected override Window CreateShell()
        {
            ShellView window = Container.Resolve<ShellView>();

            return window;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            UnityContainerExtension container = containerRegistry as UnityContainerExtension;
            regionManager = container.Resolve<IRegionManager>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.Initialize();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
