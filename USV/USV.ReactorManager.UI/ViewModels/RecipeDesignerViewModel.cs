using E3.ReactorManager.DesignExperiment.Model;
using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.UserManager.Model.Data;
using E3Tech.RecipeBuilding.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace USV.ReactorManager.UI.ViewModels
{
    public class RecipeDesignerViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        IRegionManager regionManager;
        IDatabaseReader databaseReader;
        IRecipesManager recipesManager;
        TaskScheduler taskScheduler;
        IExperimentInfoProvider experimentInfoProvider;

        public RecipeDesignerViewModel(IUnityContainer containerProvider, IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            databaseReader = containerProvider.Resolve<IDatabaseReader>();
            recipesManager = containerProvider.Resolve<IRecipesManager>();
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            experimentInfoProvider = containerProvider.Resolve<IExperimentInfoProvider>();
        }

        #region Navigation Aware Handlers

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            NavigationParameters = navigationContext.Parameters;
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

        #region Page Loaded Functions
        public void PageLoaded()
        {
            RunningBatchesList = new ObservableCollection<string>(experimentInfoProvider.GetAllRunningBatchesInThePlant().Select(b => b.FieldDeviceIdentifier));
            AddNewBatchesToRecipesManager(RunningBatchesList.ToList());
            RemoveExtraBatchesInRecipeManager(RunningBatchesList.ToList());
        }

        private void RemoveExtraBatchesInRecipeManager(IList<string> runningBatches)
        {
            foreach (var deviceId in recipesManager.DevicesRunningRecipe.ToList())
            {
                if (runningBatches.Contains(deviceId))
                {
                    /* Keep it */
                }
                else
                {
                    /* Remove it because the batch in the deviceId is not running now */
                    recipesManager.RemoveRecipe(deviceId);
                }
            }
        }

        private void AddNewBatchesToRecipesManager(IList<string> runningBatches)
        {
            foreach (var deviceId in runningBatches)
            {
                recipesManager.AddRecipe(deviceId);
            }
        }

        #endregion

        #region Navigation Functions
        public void NavigateToOtherScreen(string screenIdentifier)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("UserDetails", UserDetails);
            regionManager.RequestNavigate("SelectedViewPane", screenIdentifier, parameters);
        }
        #endregion

        #region Commands
        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand(PageLoaded));
            set { _loadedCommand = value; }
        }
        private ICommand _navigateToOtherScreenCommand;
        public ICommand NavigateToOtherScreenCommand
        {
            get => _navigateToOtherScreenCommand ?? (_navigateToOtherScreenCommand = new DelegateCommand<string>(NavigateToOtherScreen));
            set { _navigateToOtherScreenCommand = value; }
        }
        #endregion

        #region Properties
        private NavigationParameters _navigationParameters;
        public NavigationParameters NavigationParameters
        {
            get => _navigationParameters ?? (_navigationParameters = new NavigationParameters());
            set
            {
                _navigationParameters = value;
                RaisePropertyChanged();
            }
        }

        public bool KeepAlive
        {
            get => false;
        }
        private User _userDetails;
        public User UserDetails
        {
            get { return _userDetails; }
            set
            {
                _userDetails = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// List of the Running Batches in the Plant
        /// </summary>
        private ObservableCollection<string> _runningBatchesList;
        public ObservableCollection<string> RunningBatchesList
        {
            get => _runningBatchesList ?? (_runningBatchesList = new ObservableCollection<string>());
            set
            {
                _runningBatchesList = value;
                RaisePropertyChanged();
            }
        }

        #endregion

    }
}
