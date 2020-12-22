using E3.ReactorManager.ParametersProvider.Model;
using E3.ReactorManager.TrendsManager.Helpers;
using E3.ReactorManager.TrendsManager.Model.Data;
using E3.ReactorManager.TrendsManager.Model.Interfaces;
using E3.UserManager.Model.Data;
using LiveCharts;
using LiveCharts.Geared;
using LiveCharts.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Unity;

namespace E3.ReactorManager.TrendsManager.ViewModels
{
    public class TrendsViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        ITrendsManager trendsManager;
        TaskScheduler taskScheduler;
        IParametersProvider parametersProvider;
        IRegionManager regionManager;

        public TrendsViewModel(IUnityContainer containerProvider, IParametersProvider parametersProvider, IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            trendsManager = containerProvider.Resolve<ITrendsManager>();
            this.parametersProvider = parametersProvider;

            SelectedStartDate = DateTime.Now;
            SelectedEndDate = DateTime.Now;

            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }

        #region Add & Remove Selected Parameters
        public void AddToSelectedParameters(string parameterName)
        {
            if (SelectedParameters.Contains(parameterName))
            {
                /* parameterName was already added */
            }
            else
            {
                SelectedParameters.Add(parameterName);
            }
        }

        public void RemoveFromSelectedParameters(string parameterName)
        {
            if (SelectedParameters.Contains(parameterName))
            {
                SelectedParameters.Remove(parameterName);
            }
            else
            {
                /* Selected Parameters does not contain parameterName */
            }
        }
        #endregion

        public void PrintTrends(CartesianChart cartesianChart)
        {
            
        }

        #region Generate & Update Trends
        private void GenerateTrends()
        {
            if (!string.IsNullOrWhiteSpace(SelectedFieldDevice)
                && !string.IsNullOrWhiteSpace(SelectedStartDate.ToString())
                && !string.IsNullOrWhiteSpace(SelectedEndDate.ToString())
                && SelectedParameters.Count > 0)
            {
                SelectedStartDate = SelectedStartDate.Date + new TimeSpan(StartHour, StartMinute, 0);
                SelectedEndDate = SelectedEndDate.Date + new TimeSpan(EndHour, EndMinute, 0);
                trendsManager.SetTrendsParameters(SelectedParameters, SelectedStartDate, SelectedEndDate);

                Task.Factory.StartNew(new Func<object, Dictionary<string, dynamic>>(trendsManager.GetTrendsData), SelectedFieldDevice)
                    .ContinueWith(new Action<Task<Dictionary<string, dynamic>>>(UpdateTrends), taskScheduler);
            }
        }

        private void UpdateTrends(Task<Dictionary<string, dynamic>> task)
        {
            if (task.IsCompleted)
            {
                //Set MaxValueAxisY and MinValueAxisY to default values before updating TrendsCollection
                MaxValueAxisY = 32767;
                MinValueAxisY = -32767;

                /* Update the trends data */
                foreach (var keyValuePair in task.Result)
                {
                    if (keyValuePair.Key == "TimeStamp")
                    {
                        var propertyInfo = GetType().GetProperty(keyValuePair.Key + "ValuesCollection");
                        propertyInfo.SetValue(this, keyValuePair.Value, null);
                    }
                    else
                    {
                        TrendsCollection.Add(new GLineSeries {
                            Values = keyValuePair.Value,
                            Title = keyValuePair.Key,
                            ScalesYAt = keyValuePair.Key.Contains("Vol_Avg") ? 0 : keyValuePair.Key.Contains("I_") ? 1 : 2,
                            Foreground = keyValuePair.Key.Contains("Vol_Avg") ? Brushes.Yellow : keyValuePair.Key.Contains("I_") ? Brushes.DodgerBlue : Brushes.DarkOliveGreen,
                        });
                    }
                }

                if (task.Result.Any())
                {
                    //update the max value of X-Axis
                    MaxValueAxisX = task.Result.ToList()[0].Value.Count;
                }
            }
            else
            {
                if (task.IsFaulted)
                {
                    // display error to user 
                }
            }
        }

        private void ClearTrendsAndTooltipContent()
        {
            try
            {
                TooltipContent = new TooltipContent();
                if (TrendsCollection.Count > 0)
                {
                    TrendsCollection.Clear();
                }
                TimeStampValuesCollection.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " In ClearTrendsCollection");
            }
        }

        private double RelocateValue(double value, double relocationDistance)
        {
            if (value >= 0)
            {
                value += relocationDistance;
            }
            else
            {
                value -= relocationDistance;
            }

            return value;
        }
        #endregion

        private void PageLoaded()
        {
            Task.Factory.StartNew(new Func<Dictionary<string, string>>(trendsManager.GetAvailableFieldDevices))
                .ContinueWith(new Action<Task<Dictionary<string, string>>>(UpdateAvailableFieldDevices));
        }

        private void UpdateAvailableFieldDevices(Task<Dictionary<string, string>> task)
        {
            if (task.IsCompleted)
            {
                AvailableFieldDevices = task.Result;
            }
            else
            {
                if (task.IsFaulted)
                {
                    // display error to user
                }
            }
        }

        #region Field Device Parameters Getters & Updaters
        private void GetAvailableFieldDeviceParameters(string deviceId)
        {
            Task.Factory.StartNew(new Func<object, IList<string>>(FetchParameters), deviceId)
                .ContinueWith(new Action<Task<IList<string>>>(UpdateParametersList));
        }

        private void UpdateParametersList(Task<IList<string>> task)
        {
            if (task.IsCompleted)
            {
                AvailableFieldDeviceParameters = task.Result;
            }
            else
            {
                if (task.IsFaulted)
                {
                    // display error to user
                }
            }
        }

        private IList<string> FetchParameters(object deviceId)
        {
            return parametersProvider.GetFieldDeviceRecordedParametersList((string)deviceId);
        }
        #endregion

        public void Navigate(string screenName)
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "UserDetails", UserDetails }
            };
            regionManager.RequestNavigate("SelectedViewPane", screenName, parameters);
        }

        #region Touch Device Identifiers
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);

        public static bool IsTouchEnabled()
        {
            int MAXTOUCHES_INDEX = 0x95;
            return GetSystemMetrics(MAXTOUCHES_INDEX) > 0;
        }
        #endregion

        #region Tooltip Handlers
        private TooltipContent GetTooltipContent(Point point)
        {
            int closestPointIndex = point.X > 0 ? int.Parse(Math.Truncate(point.X).ToString()) : 0;
            var newTooltipContent = new TooltipContent();
            SeriesCollection trendsCollection = (SeriesCollection)GetType().GetProperty("TrendsCollection").GetValue(this, null);

            foreach (string parameter in SelectedParameters)
            {
                string parameterNameInCollection = GetParameterNameInCollection(parameter);
                if (newTooltipContent.Content.Any(p => p.ParameterName == parameterNameInCollection))
                {
                    continue;
                }

                if (trendsCollection.Any(series => series.Title == parameterNameInCollection))
                {
                    GLineSeries parameterCollection = (GLineSeries)trendsCollection.Where(series => series.Title == parameterNameInCollection).FirstOrDefault();
                    {
                        if (parameterCollection != null)
                        {
                            var tooltipParameterAndValue = ReturnTooltipParameterAndValueObject<double>(parameterNameInCollection, (GearedValues<double>)parameterCollection.Values, closestPointIndex);
                            if (tooltipParameterAndValue != null)
                            {
                                newTooltipContent.Content.Add(tooltipParameterAndValue);
                            }
                        }
                    }
                }
            }

            //Add the TimeStamp data to tooltip content
            if (SelectedParameters.Count > 0)
            {
                var propertyInfo = GetType().GetProperty("TimeStampValuesCollection");
                var propertyValue = propertyInfo.GetValue(this, null) as GearedValues<string>;

                var tooltipParameterAndValue = ReturnTooltipParameterAndValueObject<string>("TimeStamp", propertyValue, closestPointIndex);
                if (tooltipParameterAndValue != null)
                {
                    newTooltipContent.Content.Add(tooltipParameterAndValue);
                }
            }

            return newTooltipContent;
        }

        private string GetParameterNameInCollection(string parameter)
        {
            switch (parameter)
            {
                case "Current_R":
                    return "I_R(A)";
                case "Current_Y":
                    return "I_Y(A)";
                case "Current_B":
                    return "I_B(A)";
                case "Voltage_1":
                case "Voltage_2":
                case "Voltage_3":
                    return "Vol_Avg(V)";
                case "Vibration":
                    return "Vib(mm/s)";
                default:
                    return parameter;
            }
        }

        TooltipParameterAndValue ReturnTooltipParameterAndValueObject<T>(string parameterName, GearedValues<T> valuesCollection, int closestValueIndex)
        {
            if (valuesCollection != null && valuesCollection.Count > 0 && closestValueIndex < valuesCollection.Count)
            {
                return new TooltipParameterAndValue { ParameterName = parameterName, ParameterValue = valuesCollection[closestValueIndex].ToString() };
            }

            return null;
        }

        private void UpdateUI(TooltipContent result)
        {
            TooltipContent = result;
        }

        public void UpdateMouseMoveTooltipContent(MouseMoveCommandParameters commandParameters)
        {
            var chart = commandParameters.Sender;
            try
            {
                //lets get where the mouse is at our chart
                var mouseCoordinate = commandParameters.args.GetPosition(chart);
                var point = chart.ConvertToChartValues(mouseCoordinate);

                XPointer = point.X;

                UpdateUI(GetTooltipContent(point));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " At UpdateMouseMoveTooltipContent");
            }
        }

        public void UpdateTouchMoveToolTipContent(TouchMoveCommandParameters commandParameters)
        {
            var chart = commandParameters.Sender;
            try
            {
                //lets get where the mouse is at our chart
                var mouseCoordinate = commandParameters.args.GetTouchPoint(chart);
                var point = chart.ConvertToChartValues(mouseCoordinate.Position);

                XPointer = point.X;

                UpdateUI(GetTooltipContent(point));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " At UpdateTouchMoveToolTipContent");
            }
        }
        #endregion

        #region Navigation Aware Handlers
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            UserDetails = (User)navigationContext.Parameters["UserDetails"];
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
        private ICommand _clearTrendsCollectionCommand;
        public ICommand ClearTrendsCollectionCommand
        {
            get { return _clearTrendsCollectionCommand ?? (_clearTrendsCollectionCommand = new DelegateCommand(ClearTrendsAndTooltipContent)); }
            set { SetProperty(ref _clearTrendsCollectionCommand, value); }
        }

        private ICommand _navigateCommand;
        public ICommand NavigateCommand
        {
            get => _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(Navigate));
            set => _navigateCommand = value;
        }

        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand(new Action(PageLoaded)));
            set => _loadedCommand = value;
        }

        private ICommand _getAvailableFieldDeviceParametersCommand;
        public ICommand GetAvailableFieldDeviceParametersCommand
        {
            get => _getAvailableFieldDeviceParametersCommand ?? (_getAvailableFieldDeviceParametersCommand = new DelegateCommand<string>(GetAvailableFieldDeviceParameters));
            set => _getAvailableFieldDeviceParametersCommand = value;
        }

        private ICommand _addToSelectedParametersCommand;
        public ICommand AddToSelectedParametersCommand
        {
            get => _addToSelectedParametersCommand ?? (_addToSelectedParametersCommand = new DelegateCommand<string>(new Action<string>(AddToSelectedParameters)));
            set => _addToSelectedParametersCommand = value;
        }

        private ICommand _removeFromSelectedParametersCommand;
        public ICommand RemoveFromSelectedParametersCommand
        {
            get => _removeFromSelectedParametersCommand ?? (_removeFromSelectedParametersCommand = new DelegateCommand<string>(new Action<string>(RemoveFromSelectedParameters)));
            set => _removeFromSelectedParametersCommand = value;
        }

        private ICommand _generateTrendsCommand;
        public ICommand GenerateTrendsCommand
        {
            get => _generateTrendsCommand ?? (_generateTrendsCommand = new DelegateCommand(new Action(GenerateTrends)));
            set => _generateTrendsCommand = value;
        }

        private ICommand _printTrendsCommand;
        public ICommand PrintTrendsCommand
        {
            get => _printTrendsCommand ?? (_printTrendsCommand = new RelayCommand<CartesianChart>(new Action<CartesianChart>(PrintTrends)));
            set => _printTrendsCommand = value;
        }

        private ICommand _updateMouseMoveToolTipCommand;
        public ICommand UpdateMouseMoveToolTipCommand
        {
            get => _updateMouseMoveToolTipCommand ?? (_updateMouseMoveToolTipCommand = new DelegateCommand<MouseMoveCommandParameters>(new Action<MouseMoveCommandParameters>(UpdateMouseMoveTooltipContent)));
            set => _updateMouseMoveToolTipCommand = value;
        }

        private ICommand _updateTouchMoveToolTipCommand;
        public ICommand UpdateTouchMoveToolTipCommand
        {
            get => _updateTouchMoveToolTipCommand ?? (_updateTouchMoveToolTipCommand = new DelegateCommand<TouchMoveCommandParameters>(new Action<TouchMoveCommandParameters>(UpdateTouchMoveToolTipContent)));
            set => SetProperty(ref _updateTouchMoveToolTipCommand, value);
        }
        #endregion

        #region Properties
        public bool KeepAlive
        {
            get => false;
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

        public bool IsTouchDevice
        {
            get => IsTouchEnabled();
        }

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

        private List<string> _selectedParameters;
        public List<string> SelectedParameters
        {
            get => _selectedParameters ?? (_selectedParameters = new List<string>());
            set
            {
                _selectedParameters = value;
                RaisePropertyChanged();
            }
        }

        private IList<string> _availableFieldDeviceParameters;
        public IList<string> AvailableFieldDeviceParameters
        {
            get => _availableFieldDeviceParameters ?? (_availableFieldDeviceParameters = new List<string>());
            set
            {
                _availableFieldDeviceParameters = value;
                RaisePropertyChanged();
            }
        }

        private SeriesCollection _trendsCollection;
        public SeriesCollection TrendsCollection
        {
            get => _trendsCollection ?? (_trendsCollection = new SeriesCollection());
            set
            {
                _trendsCollection = value;
                RaisePropertyChanged();
            }
        }

        private GearedValues<string> _timeStampValuesCollection;
        public GearedValues<string> TimeStampValuesCollection
        {
            get => _timeStampValuesCollection ?? (_timeStampValuesCollection = new GearedValues<string>());
            set
            {
                _timeStampValuesCollection = value;
                RaisePropertyChanged();
            }
        }

        private TooltipContent _tooltipContent;
        public TooltipContent TooltipContent
        {
            get => _tooltipContent ?? (_tooltipContent = new TooltipContent());
            set
            {
                _tooltipContent = value;
                RaisePropertyChanged();
            }
        }

        private double _minValueAxisY = -32767;
        public double MinValueAxisY
        {
            get => _minValueAxisY;
            set
            {
                _minValueAxisY = value;
                RaisePropertyChanged();
            }
        }

        private double _maxValueAxisY = 32767;
        public double MaxValueAxisY
        {
            get => _maxValueAxisY;
            set
            {
                _maxValueAxisY = value;
                RaisePropertyChanged();
            }
        }

        private double _maxValueAxisX = 60;
        public double MaxValueAxisX
        {
            get => _maxValueAxisX;
            set
            {
                _maxValueAxisX = value;
                RaisePropertyChanged();
            }
        }

        private double _xPointer;
        public double XPointer
        {
            get => _xPointer;
            set
            {
                _xPointer = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _selectedStartDate;
        public DateTime SelectedStartDate
        {
            get => _selectedStartDate;
            set
            {
                _selectedStartDate = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _selectedEndDate;
        public DateTime SelectedEndDate
        {
            get => _selectedEndDate;
            set
            {
                _selectedEndDate = value;
                RaisePropertyChanged();
            }
        }

        private int _startHour;
        public int StartHour
        {
            get => _startHour;
            set => SetProperty(ref _startHour, value);
        }

        private int _startMinute;
        public int StartMinute
        {
            get => _startMinute;
            set => SetProperty(ref _startMinute, value);
        }

        private int _endHour;
        public int EndHour
        {
            get => _endHour;
            set => SetProperty(ref _endHour, value);
        }

        private int _endMinute;
        public int EndMinute
        {
            get => _endMinute;
            set => SetProperty(ref _endMinute, value);
        }

        private string _selectedFieldDevice;
        public string SelectedFieldDevice
        {
            get => _selectedFieldDevice;
            set
            {
                _selectedFieldDevice = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }

}
