using Prism.Unity.Ioc;
using Unity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Prism.Ioc;

namespace E3.FieldDevicesInfoPopulator.UserControls
{
    /// <summary>
    /// Interaction logic for ParameterInfoViewUserControl.xaml
    /// </summary>
    public partial class ParameterInfoViewUserControl : UserControl
    {
        UnityContainerExtension unityContainer;

        public ParameterInfoViewUserControl()
        {
            InitializeComponent();
            unityContainer = (UnityContainerExtension)Application.Current.Resources["IoC"];
        }

        #region Parameter Name
        public static readonly DependencyProperty ParameterNameProperty =
           DependencyProperty.Register("ParameterName", typeof(string), typeof(ParameterInfoViewUserControl), new
              PropertyMetadata("", new PropertyChangedCallback(OnParameterNameChanged)));

        public string ParameterName
        {
            get { return (string)GetValue(ParameterNameProperty); }
            set { SetValue(ParameterNameProperty, value); }
        }

        private static void OnParameterNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ParameterInfoViewUserControl parameterInfoViewUserControl = d as ParameterInfoViewUserControl;
            parameterInfoViewUserControl.OnParameterNameChanged(e);
        }

        private void OnParameterNameChanged(DependencyPropertyChangedEventArgs e)
        {
            //Do something when this property changed
            if (!string.IsNullOrWhiteSpace(ParameterName))
            {
                paramName.Content = e.NewValue;

                if (paramName.Content.ToString() == "R")
                {
                    paramName.Foreground = Brushes.Red;
                }
                else if(paramName.Content.ToString() == "Y")
                {
                    paramName.Foreground = Brushes.Yellow;
                }
                else if (paramName.Content.ToString() == "B")
                {
                    paramName.Foreground = Brushes.Blue;
                }
            }
        }
        #endregion

        #region Parameter Value
        public static readonly DependencyProperty ParameterValueProperty =
           DependencyProperty.Register("ParameterValue", typeof(string), typeof(ParameterInfoViewUserControl), new
              PropertyMetadata("", new PropertyChangedCallback(OnParameterValueChanged)));

        public string ParameterValue
        {
            get { return (string)GetValue(ParameterValueProperty); }
            set { SetValue(ParameterValueProperty, value); }
        }

        private static void OnParameterValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ParameterInfoViewUserControl parameterInfoViewUserControl = d as ParameterInfoViewUserControl;
            parameterInfoViewUserControl.OnParameterValueChanged(e);
        }

        private void OnParameterValueChanged(DependencyPropertyChangedEventArgs e)
        {
            //Do something when this property changed
            if (!string.IsNullOrWhiteSpace(ParameterValue))
            {
                paramValue.Content = e.NewValue;
            }
        }
        #endregion

        #region Parameter Units
        public static readonly DependencyProperty ParameterUnitsProperty =
           DependencyProperty.Register("ParameterUnits", typeof(string), typeof(ParameterInfoViewUserControl), new
              PropertyMetadata("", new PropertyChangedCallback(OnParameterUnitsChanged)));

        public string ParameterUnits
        {
            get { return (string)GetValue(ParameterUnitsProperty); }
            set { SetValue(ParameterUnitsProperty, value); }
        }

        private static void OnParameterUnitsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ParameterInfoViewUserControl parameterInfoViewUserControl = d as ParameterInfoViewUserControl;
            parameterInfoViewUserControl.OnParameterUnitsChanged(e);
        }

        private void OnParameterUnitsChanged(DependencyPropertyChangedEventArgs e)
        {
            //Do something when this property changed
            if (!string.IsNullOrWhiteSpace(ParameterUnits))
            {
                paramUnits.Content = e.NewValue;
            }
        }
        #endregion

        #region Device Id
        public static readonly DependencyProperty DeviceIdProperty =
           DependencyProperty.Register("DeviceId", typeof(string), typeof(ParameterInfoViewUserControl), new
              PropertyMetadata("", new PropertyChangedCallback(OnDeviceIdChanged)));

        public string DeviceId
        {
            get { return (string)GetValue(DeviceIdProperty); }
            set { SetValue(DeviceIdProperty, value); }
        }

        private static void OnDeviceIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ParameterInfoViewUserControl parameterInfoViewUserControl = d as ParameterInfoViewUserControl;
            parameterInfoViewUserControl.OnDeviceIdChanged(e);
        }

        private void OnDeviceIdChanged(DependencyPropertyChangedEventArgs e)
        {
            //Do something when this property changed
        }
        #endregion

        #region Device Label
        public static readonly DependencyProperty DeviceLabelProperty =
           DependencyProperty.Register("DeviceLabel", typeof(string), typeof(ParameterInfoViewUserControl), new
              PropertyMetadata("", new PropertyChangedCallback(OnDeviceLabelChanged)));

        public string DeviceLabel
        {
            get { return (string)GetValue(DeviceLabelProperty); }
            set { SetValue(DeviceLabelProperty, value); }
        }

        private static void OnDeviceLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ParameterInfoViewUserControl parameterInfoViewUserControl = d as ParameterInfoViewUserControl;
            parameterInfoViewUserControl.OnDeviceLabelChanged(e);
        }

        private void OnDeviceLabelChanged(DependencyPropertyChangedEventArgs e)
        {
            //Do something when this property changed
        }
        #endregion

        #region FieldPointId
        public static readonly DependencyProperty FieldPointIdProperty =
           DependencyProperty.Register("FieldPointId", typeof(string), typeof(ParameterInfoViewUserControl), new
              PropertyMetadata("", new PropertyChangedCallback(OnFieldPointIdChanged)));

        public string FieldPointId
        {
            get { return (string)GetValue(FieldPointIdProperty); }
            set { SetValue(FieldPointIdProperty, value); }
        }

        private static void OnFieldPointIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ParameterInfoViewUserControl parameterInfoViewUserControl = d as ParameterInfoViewUserControl;
            parameterInfoViewUserControl.OnFieldPointIdChanged(e);
        }

        private void OnFieldPointIdChanged(DependencyPropertyChangedEventArgs e)
        {
            //Do something when this property changed
        }
        #endregion

        #region Parameter Alarm Status
        public static readonly DependencyProperty ParameterAlarmStatusProperty =
           DependencyProperty.Register("ParameterAlarmStatus", typeof(bool), typeof(ParameterInfoViewUserControl), new
              PropertyMetadata(false, new PropertyChangedCallback(OnParameterAlarmStatusChanged)));

        public bool ParameterAlarmStatus
        {
            get { return (bool)GetValue(ParameterAlarmStatusProperty); }
            set { SetValue(ParameterAlarmStatusProperty, value); }
        }

        private static void OnParameterAlarmStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ParameterInfoViewUserControl parameterInfoViewUserControl = d as ParameterInfoViewUserControl;
            parameterInfoViewUserControl.OnParameterAlarmStatusChanged(e);
        }

        private void OnParameterAlarmStatusChanged(DependencyPropertyChangedEventArgs e)
        {
            //Do something when this property changed
            if ((bool)e.NewValue)
            {
                paramValue.Background = Brushes.Red;
            }
            else
            {
                BrushConverter converter = new BrushConverter();
                paramValue.Background = (Brush)converter.ConvertFromString("#1a202c");
            }
        }
        #endregion
    }
}
