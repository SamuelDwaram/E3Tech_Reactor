using E3.ReactorManager.DesignExperiment.Model;
using E3.ReactorManager.DesignExperiment.Model.Data;
using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.UserManager.Model.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Unity;

namespace USV.ReactorManager.UI.ViewModels
{
    public class DesignExperimentViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        IRegionManager regionManager;
        IUnityContainer unityContainer;
        TaskScheduler taskScheduler;
        IDesignExperiment designExperiment;
        IDatabaseReader databaseReader;
        IFieldDevicesCommunicator fieldDevicesCommunicator;

        public DesignExperimentViewModel(IUnityContainer containerProvider, IRegionManager regionManager)
        {
            unityContainer = containerProvider;
            this.regionManager = regionManager;
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
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

        #region Page Loaded Functions
        public void PageLoaded()
        {
            designExperiment = unityContainer.Resolve<IDesignExperiment>();
            databaseReader = unityContainer.Resolve<IDatabaseReader>();
            fieldDevicesCommunicator = unityContainer.Resolve<IFieldDevicesCommunicator>();

            Task<Dictionary<string, string>>.Factory.StartNew(new Func<Dictionary<string, string>>(designExperiment.GetAvailableFieldDevicesForRunningBatch))
                .ContinueWith(new Action<Task<Dictionary<string, string>>>(UpdateAvailableFieldDevicesList), taskScheduler);
        }

        private void UpdateAvailableFieldDevicesList(Task<Dictionary<string, string>> task)
        {
            /* Update Available field devices list from task result */
            AvailableFieldDevices = task.Result;
        }
        #endregion

        #region Navigation Functions
        public void NavigateToOtherScreen(string screenIdentifier)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("UserDetails", UserDetails);
            regionManager.RequestNavigate("SelectedViewPane", screenIdentifier);
        }
        #endregion

        #region Upload Image Functions
        /// <summary>
        /// Allows to execute upload image command
        /// </summary>
        /// <returns></returns>
        private bool CanUploadImage()
        {
            return true;
        }
        /// <summary>
        /// Function to execute when upload image button 
        /// is clicked in GUI
        /// </summary>
        private void UploadImage()
        {

        }
        #endregion

        #region Start Batch Functions
        /// <summary>
        /// Allows to execute Start Reaction Command
        /// </summary>
        /// <returns></returns>
        private bool CanStartBatch()
        {
            return SelectedFieldDevice != null && SelectedFieldDevice.Length > 0
                && SelectedStirrer != null && SelectedStirrer.Length > 0
                && SelectedDosingPump != null && SelectedDosingPump.Length > 0
                && BatchName != null && BatchName.Length > 0
                && BatchNumber != null && BatchNumber.Length > 0
                && Comments != null && Comments.Length > 0;
        }
        /// <summary>
        /// Function to execute when start reaction
        /// is clicked in GUI
        /// </summary>
        public async void StartBatch()
        {
            var batchData = new Batch
            {
                Name = BatchName,
                Number = BatchNumber,
                ScientistName = UserDetails.Name,
                FieldDeviceIdentifier = SelectedFieldDevice,
                HCIdentifier = string.Empty,
                StirrerIdentifier = SelectedStirrer,
                DosingPumpUsage = SelectedDosingPump,
                Comments = Comments,
                TimeStarted = DateTime.Now
            };

            try
            {
                if (designExperiment.StartBatch(batchData))
                {
                    await Task.Factory.StartNew(new Action(UpdateAlarmLimitsToPlc));

                    /* If Starting batch was successful then Navigate to Dashboard */
                    NavigateToOtherScreen("Dashboard");
                }
                else
                {
                    /* 
                     * If Starting batch returns false in the Design Experiment module 
                     * then Show the failure information to the user
                     */
                    MessageBox.Show("Batch " + BatchName + " already exists in the Database", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Starting batch");
                Console.WriteLine("Error Message : " + ex.Message);
                Console.WriteLine("Error StackTrace : " + ex.StackTrace);
            }

            await Task.Yield();
        }

        private void UpdateAlarmLimitsToPlc()
        {
            if (EnabledAlarms)
            {
                fieldDevicesCommunicator.SendCommandToDevice(SelectedFieldDevice, "HeatCoolTemperatureMaxLimit",
                                                                    "int", MaxLimit.ToString());
                fieldDevicesCommunicator.SendCommandToDevice(SelectedFieldDevice, "HeatCoolTemperatureMinLimit",
                                                                    "int", MinLimit.ToString());
            }
        }

        #endregion

        #region Update HC List Functions
        /// <summary>
        /// as soon as field device is selected 
        /// the connected HC & stirrer are updated to list
        /// </summary>
        /// <returns></returns>
        public void UpdateHClistForSelectedFieldDevice()
        {
            //clear the previous list(if any)
            HCList.Clear();
            switch (SelectedFieldDevice)
            {
                case "Reactor_1":
                    HCList.Add("RD/HBR-11");
                    break;
                case "Reactor_2":
                    HCList.Add("RD/HBR-09");
                    break;
                case "Reactor_3":
                    HCList.Add("RD/HBR-01");
                    break;
                case "Reactor_4":
                    HCList.Add("RD/HBR-02");
                    break;
                case "Reactor_5":
                    HCList.Add("RD/HBR-07");
                    break;
                case "Reactor_6":
                    HCList.Add("RD/HBR-14");
                    break;
            }
        }
        #endregion

        #region Commands
        private ICommand _navigateToOtherScreenCommand;
        public ICommand NavigateToOtherScreenCommand
        {
            get => _navigateToOtherScreenCommand ?? (_navigateToOtherScreenCommand = new DelegateCommand<string>(NavigateToOtherScreen));
            set { _navigateToOtherScreenCommand = value; }
        }
        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand(PageLoaded));
            set { _loadedCommand = value; }
        }
        /// <summary>
        /// Binded to upload image button in GUI
        /// </summary>
        private ICommand _uploadImageCommand;
        public ICommand UploadImageCommand
        {
            get => _uploadImageCommand ?? (_uploadImageCommand = new DelegateCommand(UploadImage, CanUploadImage));
            set { _uploadImageCommand = value; }
        }
        /// <summary>
        /// Binded to Start Reaction button in GUI
        /// </summary>
        private ICommand _startBatchCommand;
        public ICommand StartBatchCommand
        {
            get => _startBatchCommand ?? 
                (_startBatchCommand = new DelegateCommand(StartBatch, CanStartBatch)
                .ObservesProperty(() => SelectedFieldDevice)
                .ObservesProperty(() => SelectedStirrer)
                .ObservesProperty(() => SelectedDosingPump)
                .ObservesProperty(() => BatchName)
                .ObservesProperty(() => BatchNumber)
                .ObservesProperty(() => Comments));
            set { _startBatchCommand = value; }
        }

        private ICommand _updateHCForSelectedFieldDeviceCommand;
        public ICommand UpdateHCForSelectedFieldDeviceCommand
        {
            get => _updateHCForSelectedFieldDeviceCommand ?? (_updateHCForSelectedFieldDeviceCommand = new DelegateCommand(UpdateHClistForSelectedFieldDevice));
            set => _updateHCForSelectedFieldDeviceCommand = value;
        }
        #endregion

        #region Properties
        public bool KeepAlive
        {
            get => false;
        }
        /// <summary>
        /// Details of user
        /// </summary>
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
        /// KeyValue Pairs of the Field Device Label and its identifier
        /// </summary>
        private Dictionary<string, string> _availableFieldDevices;
        public Dictionary<string, string> AvailableFieldDevices
        {
            get => _availableFieldDevices ?? (_availableFieldDevices = new Dictionary<string, string>());
            set
            {
                _availableFieldDevices = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// Gets Selected FieldDevice from FieldDevices List
        /// </summary>
        private string _selectedFieldDevice;
        public string SelectedFieldDevice
        {
            get { return _selectedFieldDevice; }
            set
            {
                _selectedFieldDevice = value;
                RaisePropertyChanged();
                //update the connected hc as soon as the field device is selected
                UpdateHClistForSelectedFieldDevice();
            }
        }

        /// <summary>
        /// Provides options for selecting HC System
        ///     (Yes-Required Or No-Not Required)
        /// </summary>
        private ObservableCollection<string> _HCList;
        public ObservableCollection<string> HCList
        {
            get => _HCList ?? (_HCList = new ObservableCollection<string>());
            set
            {
                _HCList = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Provides options for selecting Stirrer
        ///     (Yes-Required Or No-Not Required)
        /// </summary>
        private List<string> _stirrerList;
        public List<string> StirrerList
        {
            get => _stirrerList ?? (_stirrerList = new List<string>() { "Anchor", "PBT", "CERT" });
            set
            {
                _stirrerList = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Tells the requirement of Stirrer in the Experiment
        /// </summary>
        private string _selectedStirrer;
        public string SelectedStirrer
        {
            get { return _selectedStirrer; }
            set
            {
                _selectedStirrer = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Provides options for selecting Stirrer
        ///     (Yes-Required Or No-Not Required)
        /// </summary>
        private List<string> _dosingPumpList;
        public List<string> DosingPumpList
        {
            get => _dosingPumpList ?? (_dosingPumpList = new List<string>() { "Yes", "No" });
            set
            {
                _dosingPumpList = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Tells the selected Dosing Pump from 
        ///     DosingPumpList
        /// </summary>
        private string _selectedDosingPump;
        public string SelectedDosingPump
        {
            get { return _selectedDosingPump; }
            set
            {
                _selectedDosingPump = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// List of Chemical DataBases in Reaction Block
        /// </summary>
        private List<string> _chemicalDataBaseList;
        public List<string> ChemicalDataBaseList
        {
            get => _chemicalDataBaseList ?? (_chemicalDataBaseList = new List<string>());
            set
            {
                _chemicalDataBaseList = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets the selected chemical Data base from 
        ///     list of chemical databases
        /// </summary>
        private string _selectedChemicalDataBase;
        public string SelectedChemicalDataBase
        {
            get { return _selectedChemicalDataBase; }
            set
            {
                _selectedChemicalDataBase = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets the Batch Name from the experiment block in GUI    
        /// when user entered it
        /// </summary>
        private string _batchName;
        public string BatchName
        {
            get { return _batchName; }
            set
            {
                _batchName = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets the Batch No from the experiment block in GUI
        /// when user entered it
        /// </summary>
        private string _batchNumber;
        public string BatchNumber
        {
            get { return _batchNumber; }
            set
            {
                _batchNumber = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets the scientistName in GUI
        /// when user entered it
        /// </summary>
        private string _scientistName;
        public string ScientistName
        {
            get { return _scientistName; }
            set
            {
                _scientistName = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets the comments from GUI 
        /// which are entered by user
        /// </summary>
        private string _comments;
        public string Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                RaisePropertyChanged();
            }
        }

        private int _maxLimit;
        public int MaxLimit
        {
            get => _maxLimit;
            set
            {
                _maxLimit = value;
                RaisePropertyChanged();
            }
        }

        private int _minLimit;
        public int MinLimit
        {
            get => _minLimit;
            set
            {
                _minLimit = value;
                RaisePropertyChanged();
            }
        }

        private bool _enabledAlarms;
        public bool EnabledAlarms
        {
            get => _enabledAlarms;
            set
            {
                _enabledAlarms = value;
                RaisePropertyChanged();
            }
        }
        #endregion

    }
}
