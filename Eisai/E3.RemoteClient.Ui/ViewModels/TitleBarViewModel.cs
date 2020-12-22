using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace E3.RemoteClient.Ui.ViewModels
{
    public class TitleBarViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;

        public TitleBarViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Loaded()
        {
            Task.Factory.StartNew(new Action(UpdateCurrentTime));
        }

        private void UpdateCurrentTime()
        {
            IRegionNavigationJournalEntry currentEntry = regionManager.Regions["SelectedViewPane"].NavigationService.Journal.CurrentEntry;
            if (currentEntry == null)
            {
                ActiveView = string.Empty;
            }
            else
            {
                ActiveView = currentEntry.Uri.ToString();
            }

            Thread.Sleep(300);
            Task.Factory.StartNew(UpdateCurrentTime);
        }

        public void ShutDown()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        public void Navigate(string pageName)
        {
            regionManager.RequestNavigate("SelectedViewPane", pageName);
        }

        #region Commands
        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand(Loaded));
            set => SetProperty(ref _loadedCommand, value);
        }

        private ICommand _navigateCommand;
        public ICommand NavigateCommand
        {
            get => _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(Navigate));
            set => SetProperty(ref _navigateCommand, value);
        }

        private ICommand _shutDownCommand;
        public ICommand ShutDownCommand
        {
            get => _shutDownCommand ?? (_shutDownCommand = new DelegateCommand(ShutDown));
            set => SetProperty(ref _shutDownCommand, value);
        }
        #endregion

        #region Properties
        private string _activeView;
        public string ActiveView
        {
            get => _activeView ?? string.Empty;
            set => SetProperty(ref _activeView, value);
        }
        #endregion
    }
}
