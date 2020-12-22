using E3.UserManager.Model.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;

namespace USV.ReactorManager.UI.ViewModels
{
    public class ReportsViewModel : BindableBase, INavigationAware
    {
        IRegionManager regionManager;

        public ReportsViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Navigate(string screenName)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("UserDetails", UserDetails);
            regionManager.RequestNavigate("SelectedViewPane", screenName, parameters);
        }

        #region Navigation Aware Handlers
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            UserDetails = navigationContext.Parameters["UserDetails"] as User;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion

        private ICommand _navigateCommand;
        public ICommand NavigateCommand
        {
            get => _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(Navigate));
            set => _navigateCommand = value;
        }

        private User _userDetails;
        public User UserDetails
        {
            get => _userDetails ?? (_userDetails = new User());
            set
            {
                _userDetails = value;
                RaisePropertyChanged();
            }
        }
    }
}
