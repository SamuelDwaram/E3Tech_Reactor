using E3.Bpu.Services;
using E3.DialogServices.Model;
using E3.RemoteClient.Ui.Views;
using Prism.Mvvm;
using System.Threading;
using System.Threading.Tasks;

namespace E3.RemoteClient.Ui.ViewModels
{
    public class InitializingViewModel : BindableBase
    {
        private readonly IDialogServiceProvider dialogServiceProvider;
        private readonly TaskScheduler taskScheduler;

        public InitializingViewModel(IBusinessProcessingUnit bpu, IDialogServiceProvider dialogServiceProvider)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            bpu.ShutDownClient += Bpu_ShutDownClient;
            this.dialogServiceProvider = dialogServiceProvider;
        }

        private void Bpu_ShutDownClient(object sender, ShutDownClientEventArgs args)
        {
            Task.Factory.StartNew(() => dialogServiceProvider.ShowDynamicDialogWindow(string.Empty, default, new ShutDownWarningView()), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
        }
    }
}
