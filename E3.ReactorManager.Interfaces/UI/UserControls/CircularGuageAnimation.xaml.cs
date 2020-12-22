using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace E3.ReactorManager.Interfaces.UI.UserControls
{
    /// <summary>
    /// Interaction logic for CircularGuageAnimation.xaml
    /// </summary>
    public partial class CircularGuageAnimation : UserControl
    {
        public CircularGuageAnimation()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty MaximumValueProperty =
           DependencyProperty.Register("MaximumValue", typeof(int), typeof(CircularGuageAnimation), new
              PropertyMetadata(200, new PropertyChangedCallback(OnMaximumValueChanged)));

        public int MaximumValue
        {
            get { return (int)GetValue(MaximumValueProperty); }
            set { SetValue(MaximumValueProperty, value); }
        }

        private static void OnMaximumValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircularGuageAnimation toggleButtonUserControl = d as CircularGuageAnimation;
            toggleButtonUserControl.OnMaximumValueChanged(e);
        }

        private void OnMaximumValueChanged(DependencyPropertyChangedEventArgs e)
        {
            if (CurrentValue != null && CurrentValue.Length > 0)
            {
                CurrentValueLabel.Content = CurrentValue;

                //Do something when this property changed
                var currentValue = float.Parse(CurrentValue);
                if (currentValue == 0)
                {
                    ColouredArc.EndAngle = -135;
                }
                else if ((currentValue / MaximumValue) > 0.5)
                {
                    ColouredArc.EndAngle = (currentValue / (MaximumValue / 2) - 1) * 135;
                }
                else if ((currentValue / MaximumValue) < 0.5)
                {
                    ColouredArc.EndAngle = currentValue / (MaximumValue / 2) * 135 - 135;
                }
                else if ((currentValue / MaximumValue) == 0.5)
                {
                    ColouredArc.EndAngle = 0;
                }
            }
        }

        public static readonly DependencyProperty CurrentValueProperty =
           DependencyProperty.Register("CurrentValue", typeof(string), typeof(CircularGuageAnimation), new
              PropertyMetadata("0", new PropertyChangedCallback(OnCurrentValueChanged)));

        public string CurrentValue
        {
            get { return (string)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }

        private static void OnCurrentValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircularGuageAnimation toggleButtonUserControl = d as CircularGuageAnimation;
            toggleButtonUserControl.OnCurrentValueChanged(e);
        }

        private void OnCurrentValueChanged(DependencyPropertyChangedEventArgs e)
        {
            if (CurrentValue != null && CurrentValue.Length > 0)
            {
                CurrentValueLabel.Content = CurrentValue;

                //Do something when this property changed
                var currentValue = float.Parse(CurrentValue);
                if (currentValue == 0)
                {
                    ColouredArc.EndAngle = -135;
                }
                else if ((currentValue / MaximumValue) > 0.5)
                {
                    ColouredArc.EndAngle = (currentValue / (MaximumValue / 2) - 1) * 135;
                }
                else if ((currentValue / MaximumValue) < 0.5)
                {
                    ColouredArc.EndAngle = currentValue / (MaximumValue / 2) * 135 - 135;
                }
                else if ((currentValue / MaximumValue) == 0.5)
                {
                    ColouredArc.EndAngle = 0;
                }
            }
        }

        public static readonly DependencyProperty UnitsValueProperty =
           DependencyProperty.Register("UnitsValue", typeof(string), typeof(CircularGuageAnimation), new
              PropertyMetadata("", new PropertyChangedCallback(OnUnitsValueChanged)));

        public string UnitsValue
        {
            get { return (string)GetValue(UnitsValueProperty); }
            set { SetValue(UnitsValueProperty, value); }
        }

        private static void OnUnitsValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircularGuageAnimation toggleButtonUserControl = d as CircularGuageAnimation;
            toggleButtonUserControl.OnUnitsValueChanged(e);
        }

        private void OnUnitsValueChanged(DependencyPropertyChangedEventArgs e)
        {
            if (UnitsValue != null)
            {
                UnitsLabel.Content = UnitsValue;
            }
        }
    }
}
