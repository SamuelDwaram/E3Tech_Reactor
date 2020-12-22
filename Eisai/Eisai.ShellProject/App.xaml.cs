using System;
using System.Threading;
using System.Windows;
using E3.UserManager.Model.Data;
using E3.UserManager.Model.Interfaces;
using E3Tech.VideoRecorder.StreamSource;
using Eisai.ShellProject.Views;
using NLogger;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity.Ioc;
using Unity;

namespace Eisai.ShellProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private IVideoStreamSource videoStreamSource;

        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogOutExistingUserIfExists();
            OnExit(null);
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //Log the error in text file
            ((UnityContainerExtension)Current.Resources["IoC"]).Resolve<E3.ReactorManager.Interfaces.Framework.Logging.ILogger>().Log(E3.ReactorManager.Interfaces.Framework.Logging.LogType.Exception, string.Empty, e.Exception);
            LogOutExistingUserIfExists();
            e.Handled = true;
        }

        private void LogOutExistingUserIfExists()
        {
            User user = (User)Current.Resources["LoggedInUser"];
            if (user != null)
            {
                ((UnityContainerExtension)Current.Resources["IoC"]).Resolve<IUserManager>().UpdateUserLoginStatus(user.UserID, false);
            }
            else
            {
                //Continue
            }
        }

        protected override Window CreateShell() => Container.Resolve<MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            UnityContainerExtension container = containerRegistry as UnityContainerExtension;
            container.RegisterInstance(LoggerFactory.CreateEventLogLogger("Application", LoggingLevel.None));
            Current.Resources.Add("IoC", container);
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
            IUnityContainer containerProvider = Container.Resolve<IUnityContainer>();
            if (containerProvider.IsRegistered<IVideoStreamSource>())
            {
                videoStreamSource = containerProvider.Resolve<IVideoStreamSource>();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            if (videoStreamSource != null)
            {
                videoStreamSource.Terminate();
                Thread.Sleep(2000);
            }
        }
    }
}
