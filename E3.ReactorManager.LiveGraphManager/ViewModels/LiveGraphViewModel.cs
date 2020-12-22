using E3.ReactorManager.LiveGraphManager.Model.Interfaces;
using LiveCharts.Defaults;
using LiveCharts.Geared;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace E3.ReactorManager.LiveGraphManager.ViewModels
{
    public class LiveGraphViewModel : BindableBase
    {
        private readonly ILiveGraphManager liveGraphManager;

        public LiveGraphViewModel(ILiveGraphManager liveGraphManager)
        {
            this.liveGraphManager = liveGraphManager;
            this.liveGraphManager.UpdateLiveGraph += OnUpdateLiveGraph;
        }

        private void OnUpdateLiveGraph(object sender, UpdateLiveGraphEventArgs updateLiveGraphEventArgs)
        {
            if (!string.IsNullOrWhiteSpace(DeviceId))
            {
                foreach (var requiredParameter in updateLiveGraphEventArgs.Data)
                {
                    if (requiredParameter.Value != null)
                    {
                        double.TryParse(requiredParameter.Value.ToString(), out double parsedLiveData);

                        var propertyInfo = GetType().GetProperty(requiredParameter.Key + "ValuesCollection");
                        var propertyValue = propertyInfo.GetValue(this, null) as GearedValues<DateTimePoint>;

                        /* Its enough if we show the live graph for last 6 hours
                         * Update the Property value */
                        if (propertyValue.Count < 21600)
                        {
                            propertyValue.Add(new DateTimePoint(DateTime.Now, parsedLiveData));
                            /* Add all the parameter values to Combined Collection for Min and Max graph values calculation */
                            CombinedValuesCollection.Add(new DateTimePoint(DateTime.Now, parsedLiveData));
                        }
                        else
                        {
                            propertyValue.RemoveAt(0);
                            propertyValue.Add(new DateTimePoint(DateTime.Now, parsedLiveData));
                            /* Add all the parameter values to Combined Collection for Min and Max graph values calculation */
                            CombinedValuesCollection.RemoveAt(0 + updateLiveGraphEventArgs.Data.Count - 1);
                            CombinedValuesCollection.Add(new DateTimePoint(DateTime.Now, parsedLiveData));
                        }

                        /* Set the modified property value using propertyInfo */
                        propertyInfo.SetValue(this, propertyValue, null);
                    }
                }

                UpdateMinMaxValuesOfLiveGraphView();
            }
        }

        internal void SetParametersToLiveGraphManager()
        {
            liveGraphManager.SetParameters(DeviceId, RequiredLiveTrendsParameters);
        }

        private void UpdateMinMaxValuesOfLiveGraphView()
        {
            //double latestMinValue = CombinedValuesCollection.Any() ? CombinedValuesCollection.Min(minimumValue => minimumValue) : -90;
            //double latestMaxValue = CombinedValuesCollection.Any() ? CombinedValuesCollection.Max(maximumValue => maximumValue) : 200;

            //if (latestMaxValue == latestMinValue)
            //{
            //    /* 
            //     * Make some difference in the latest Min and latest Max values 
            //     * otherwise invalid axis range exception will be thrown
            //     */
            //    latestMaxValue += 5;
            //    latestMinValue -= 5;
            //}

            //MinValueAxisY = latestMinValue;
            //MaxValueAxisY = latestMaxValue;
        }

        public void PageLoaded()
        {
            liveGraphManager.SetParameters(DeviceId, RequiredLiveTrendsParameters);
            liveGraphManager.UpdateNow();
        }

        #region Commands
        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand(new System.Action(PageLoaded)));
            set
            {
                _loadedCommand = value;
            }
        }
        #endregion

        #region Trends Properties
        private string deviceId;
        public string DeviceId
        {
            get { return deviceId; }
            set
            {
                deviceId = value;
                RaisePropertyChanged();
            }
        }

        private List<string> _requiredLiveTrendsParameters;
        public List<string> RequiredLiveTrendsParameters
        {
            get => _requiredLiveTrendsParameters ?? (_requiredLiveTrendsParameters = new List<string> { "ReactorMassTemperature", "JacketOutletTemperature", "HeatCoolSetPoint" });
            set
            {
                _requiredLiveTrendsParameters = value;
                RaisePropertyChanged();
            }
        }

        private GearedValues<DateTimePoint> _reactorMassTemperatureValuesCollection;
        public GearedValues<DateTimePoint> ReactorMassTemperatureValuesCollection
        {
            get => _reactorMassTemperatureValuesCollection ?? (_reactorMassTemperatureValuesCollection = new GearedValues<DateTimePoint>());
            set
            {
                _reactorMassTemperatureValuesCollection = value;
                RaisePropertyChanged();
            }
        }

        private GearedValues<DateTimePoint> _jacketOutletTemperatureValuesCollection;
        public GearedValues<DateTimePoint> JacketOutletTemperatureValuesCollection
        {
            get => _jacketOutletTemperatureValuesCollection ?? (_jacketOutletTemperatureValuesCollection = new GearedValues<DateTimePoint>());
            set
            {
                _jacketOutletTemperatureValuesCollection = value;
                RaisePropertyChanged();
            }
        }

        private GearedValues<DateTimePoint> _heatCoolSetPointValuesCollection;
        public GearedValues<DateTimePoint> HeatCoolSetPointValuesCollection
        {
            get => _heatCoolSetPointValuesCollection ?? (_heatCoolSetPointValuesCollection = new GearedValues<DateTimePoint>());
            set
            {
                _heatCoolSetPointValuesCollection = value;
                RaisePropertyChanged();
            }
        }

        private GearedValues<DateTimePoint> _combinedValuesCollection;
        public GearedValues<DateTimePoint> CombinedValuesCollection
        {
            get => _combinedValuesCollection ?? (_combinedValuesCollection = new GearedValues<DateTimePoint>());
            set
            {
                _combinedValuesCollection = value;
                RaisePropertyChanged();
            }
        }

        private double _minValueAxisY = -90;
        public double MinValueAxisY
        {
            get => _minValueAxisY;
            set
            {
                _minValueAxisY = value;
                RaisePropertyChanged();
            }
        }

        private double _maxValueAxisY = 200;
        public double MaxValueAxisY
        {
            get => _maxValueAxisY;
            set
            {
                _maxValueAxisY = value;
                RaisePropertyChanged();
            }
        }
        public Func<double, string> XFormatter { get; set; } = val => new DateTime((long)val).ToString("dd/MMM HH:mm:ss");
        public Func<double, string> YFormatter { get; set; } = val => Math.Round(val, 2).ToString();

        #endregion
    }
}
