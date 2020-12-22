using E3.EquipmentUsageTracker.Model;
using E3.EquipmentUsageTracker.Model.Data;
using E3.EquipmentUsageTracker.Model.Enums;
using E3.UserManager.Model.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace E3.EquipmentUsageTracker.ViewModels
{
    public class EquipmentOccupancyContainerViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
    {
        IEquipmentUsageTracker equipmentUsageTracker;
        IRegionManager regionManager;
        IUnityContainer unityContainer;
        TaskScheduler taskScheduler;

        public EquipmentOccupancyContainerViewModel(IUnityContainer containerProvider, IRegionManager regionManager)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            unityContainer = containerProvider;
            this.regionManager = regionManager;
            equipmentUsageTracker = containerProvider.Resolve<IEquipmentUsageTracker>();
        }

        public void UpdateEquipmentOccupancy()
        {
            if (!string.IsNullOrWhiteSpace(SelectedMonth) && !string.IsNullOrWhiteSpace(SelectedOccupancyReportTypeEnum.ToString()))
            {
                equipmentUsageTracker.PublishUpdateEquipmentOccupancyEvent(SelectedMonth, SelectedOccupancyReportTypeEnum);
            }
        }

        public void Navigate(string screenIdentifier)
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "UserDetails", UserDetails }
            };
            regionManager.RequestNavigate("SelectedViewPane", screenIdentifier, parameters);
        }

        #region Available Equipments Updaters in UI
        public void GetAvailableEquipments()
        {
            Task.Factory.StartNew(new Func<IList<EquipmentAndConnectedFieldDeviceArgs>>(equipmentUsageTracker.GetAvailableEquipments))
                .ContinueWith(new Func<Task<IList<EquipmentAndConnectedFieldDeviceArgs>>, IList<IList<EquipmentOccupancyViewModel>>>(PrepareAvailableEquipmentsView))
                .ContinueWith(new Action<Task<IList<IList<EquipmentOccupancyViewModel>>>>((task) => {
                    AvailableEquipments = new ObservableCollection<IList<EquipmentOccupancyViewModel>>(task.Result);
                }), taskScheduler);
        }

        private IList<IList<EquipmentOccupancyViewModel>> PrepareAvailableEquipmentsView(Task<IList<EquipmentAndConnectedFieldDeviceArgs>> task)
        {
            if (task.IsCompleted)
            {
                IList<IList<EquipmentOccupancyViewModel>> equipmentOccupanciesList
                    = new List<IList<EquipmentOccupancyViewModel>> { new List<EquipmentOccupancyViewModel>() };
                foreach (EquipmentAndConnectedFieldDeviceArgs args in task.Result)
                {
                    EquipmentOccupancyViewModel equipmentOccupancyViewModel = unityContainer.Resolve<EquipmentOccupancyViewModel>();
                    equipmentOccupancyViewModel.SetParameters(args.EquipmentIdentifier, args.FieldDeviceConnectedTo, args.FieldDeviceLabel);

                    //If count of items in the Last indexed element is less than 4 add EquipmentOccupancyViewModel to it
                    //else add a new Element to list and add EquipmentOccupancyViewModel to the newly added Element in the AvailableEquipments List
                    if (equipmentOccupanciesList[equipmentOccupanciesList.Count - 1].Count < 4)
                    {
                        equipmentOccupanciesList[equipmentOccupanciesList.Count - 1].Add(equipmentOccupancyViewModel);
                    }
                    else
                    {
                        IList<EquipmentOccupancyViewModel> newEquipmentOccupancyViewModelsList = new List<EquipmentOccupancyViewModel> { equipmentOccupancyViewModel };
                        equipmentOccupanciesList.Add(newEquipmentOccupancyViewModelsList);
                    }
                }

                return equipmentOccupanciesList;
            }

            return default;
        }
        #endregion

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

        #region Commands
        private ICommand _getAvailableEquipmentsCommand;
        public ICommand GetAvailableEquipmentsCommand
        {
            get => _getAvailableEquipmentsCommand ?? (_getAvailableEquipmentsCommand = new DelegateCommand(GetAvailableEquipments));
            set => SetProperty(ref _getAvailableEquipmentsCommand, value);
        }

        private ICommand _navigateCommand;
        public ICommand NavigateCommand
        {
            get => _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(Navigate));
            set => SetProperty(ref _navigateCommand, value);
        }

        private ICommand _updateEquipmentOccupancyCommand;
        public ICommand UpdateEquipmentOccupancyCommand
        {
            get => _updateEquipmentOccupancyCommand ?? (_updateEquipmentOccupancyCommand = new DelegateCommand(UpdateEquipmentOccupancy));
            set => SetProperty(ref _updateEquipmentOccupancyCommand, value);
        }
        #endregion

        #region Properties
        public bool KeepAlive
        {
            get => false;
        }

        /// <summary>
        /// Details of the user who is currently logged in
        /// </summary>
        private User _userDetails;
        public User UserDetails
        {
            get => _userDetails ?? (_userDetails = new User());
            set => SetProperty(ref _userDetails, value);
        }

        /// <summary>
        /// Selected Month in GUI
        /// </summary>
        private string _selectedMonth;
        public string SelectedMonth
        {
            get => _selectedMonth ?? (_selectedMonth = string.Empty);
            set => SetProperty(ref _selectedMonth, value);
        }

        public IEnumerable<OccupancyReportTypeEnum> OccupancyReportTypeEnumValues => Enum.GetValues(typeof(OccupancyReportTypeEnum)).Cast<OccupancyReportTypeEnum>();

        private OccupancyReportTypeEnum _selectedOccupancyReportTypeEnum;
        public OccupancyReportTypeEnum SelectedOccupancyReportTypeEnum
        {
            get => _selectedOccupancyReportTypeEnum;
            set => SetProperty(ref _selectedOccupancyReportTypeEnum, value);
        }

        private ObservableCollection<IList<EquipmentOccupancyViewModel>> _availableEquipments;
        public ObservableCollection<IList<EquipmentOccupancyViewModel>> AvailableEquipments
        {
            get => _availableEquipments ?? (_availableEquipments = new ObservableCollection<IList<EquipmentOccupancyViewModel>> { new List<EquipmentOccupancyViewModel>() });
            set => SetProperty(ref _availableEquipments, value);
        }
        #endregion
    }
}
