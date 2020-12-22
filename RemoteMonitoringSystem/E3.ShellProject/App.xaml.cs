using E3.ReactorManager.Interfaces.Framework.Logging;
using E3.ShellProject.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity.Ioc;
using System;
using System.Windows;
using System.Windows.Threading;

namespace E3.ShellProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private IRegionManager regionManager;
        private ILogger logger;

        public App()
        {
            Current.Exit += Current_Exit;
            Current.Startup += Current_Startup;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void Current_Startup(object sender, StartupEventArgs e)
        {
            //logger.Log(LogType.Information, $"{DateTime.Now} Remote Monitoring System Started");
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            logger.Log(LogType.Information, $"{DateTime.Now} Remote Monitoring System Terminated");
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            OnExit(null);
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            OnExit(null);
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
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
            logger = Container.Resolve<ILogger>();
            logger.Log(LogType.Information, $"{DateTime.Now} Remote Monitoring System Started");
            Current.Resources.Add("IoC", Container);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            logger.Log(LogType.Error, $"{DateTime.Now} Remote Monitoring System Error Code : {(e == null ? "None" : e.ApplicationExitCode.ToString())}");
        }
    }
}
