using Basf.Shell.Views;
using E3.ReactorManager.Interfaces.Framework.Logging;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity.Ioc;
using System;
using System.Windows;
using Unity;

namespace Basf.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ((UnityContainerExtension)Current.Resources["IoC"]).Resolve<ILogger>().Log(LogType.Exception, e.ExceptionObject.ToString());
            MessageBox.Show($"Exception : {e.ExceptionObject}. {Environment.NewLine} App is {(e.IsTerminating ? "Terminating" : "Not Terminating")}");
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            ((UnityContainerExtension)Current.Resources["IoC"]).Resolve<ILogger>().Log(LogType.Exception, e.Exception.Message, e.Exception);
            MessageBox.Show($"Exception : {e.Exception.Message}{Environment.NewLine} StackTrace : {e.Exception.StackTrace}");
            e.Handled = true;
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            UnityContainerExtension container = containerRegistry as UnityContainerExtension;
            Current.Resources.Add("IoC", container);
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }
    }
}
