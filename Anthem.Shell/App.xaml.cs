using Anathem.Shell.Views;
using E3.ReactorManager.Interfaces.Framework.Logging;
using Prism.Ioc;
using Prism.Modularity;
using SingleInstanceUtilities;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Anathem.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private ILogger logger;

        private static readonly SingleInstance Singleton = new SingleInstance(typeof(App).FullName);
        public App()
        {
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            logger.Log(LogType.Exception, e.Exception.Message, e.Exception);
            MessageBox.Show($"Exception : {e.Exception.Message}{Environment.NewLine} StackTrace : {e.Exception.StackTrace}");
            e.Handled = true;
        }
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            logger.Log(LogType.Exception, e.ExceptionObject.ToString());
            MessageBox.Show($"Exception : {e.ExceptionObject}. {Environment.NewLine} App is {(e.IsTerminating ? "Terminating" : "Not Terminating")}");
        }

        [STAThread]
        public static void Main(string[] args)
        {
            Singleton.RunFirstInstance(() => {
                App app = new App();
                app.InitializeComponent();
                app.Run();
            });
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            logger = Container.Resolve<ILogger>();
            logger.Log(LogType.Information, $"{DateTime.Now} Remote Monitoring System Started");
            Current.Resources.Add("IoC", Container);
        }
    }
}
