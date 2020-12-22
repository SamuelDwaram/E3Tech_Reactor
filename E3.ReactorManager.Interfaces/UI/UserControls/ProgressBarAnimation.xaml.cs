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
    /// Interaction logic for ProgressBarAnimation.xaml
    /// </summary>
    public partial class ProgressBarAnimation : UserControl
    {
        public ProgressBarAnimation()
        {
            InitializeComponent();
        }

        #region Positive Maximum
        public static readonly DependencyProperty PositiveMaximumValueProperty =
           DependencyProperty.Register("PositiveMaximumValue", typeof(int), typeof(ProgressBarAnimation), new
              PropertyMetadata(200, new PropertyChangedCallback(OnPositiveMaximumValueChanged)));

        public int PositiveMaximumValue
        {
            get { return (int)GetValue(PositiveMaximumValueProperty); }
            set { SetValue(PositiveMaximumValueProperty, value); }
        }

        private static void OnPositiveMaximumValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressBarAnimation toggleButtonUserControl = d as ProgressBarAnimation;
            toggleButtonUserControl.OnPositiveMaximumValueChanged(e);
        }

        private void OnPositiveMaximumValueChanged(DependencyPropertyChangedEventArgs e)
        {
            //Do something when this property changed
            NegativeProgressBarArea.Width = (float)NegativeMaximumValue / (NegativeMaximumValue + PositiveMaximumValue) * 117;
            PositiveProgressBarArea.Width = (float)PositiveMaximumValue / (NegativeMaximumValue + PositiveMaximumValue) * 117;
        }
        #endregion

        #region Negative Maximum
        public static readonly DependencyProperty NegativeMaximumValueProperty =
           DependencyProperty.Register("NegativeMaximumValue", typeof(int), typeof(ProgressBarAnimation), new
              PropertyMetadata(90, new PropertyChangedCallback(OnNegativeMaximumValueChanged)));

        public int NegativeMaximumValue
        {
            get { return (int)GetValue(NegativeMaximumValueProperty); }
            set { SetValue(NegativeMaximumValueProperty, value); }
        }

        private static void OnNegativeMaximumValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressBarAnimation toggleButtonUserControl = d as ProgressBarAnimation;
            toggleButtonUserControl.OnNegativeMaximumValueChanged(e);
        }

        private void OnNegativeMaximumValueChanged(DependencyPropertyChangedEventArgs e)
        {
            //Do something when this property changed
            NegativeProgressBarArea.Width = (float)NegativeMaximumValue / (NegativeMaximumValue + PositiveMaximumValue) * 117;
            PositiveProgressBarArea.Width = (float)PositiveMaximumValue / (NegativeMaximumValue + PositiveMaximumValue) * 117;
        }
        #endregion

        #region Current Value
        public static readonly DependencyProperty CurrentValueProperty =
           DependencyProperty.Register("CurrentValue", typeof(string), typeof(ProgressBarAnimation), new
              PropertyMetadata("0", new PropertyChangedCallback(OnCurrentValueChanged)));

        public string CurrentValue
        {
            get { return (string)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }

        private static void OnCurrentValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressBarAnimation toggleButtonUserControl = d as ProgressBarAnimation;
            toggleButtonUserControl.OnCurrentValueChanged(e);
        }

        private void OnCurrentValueChanged(DependencyPropertyChangedEventArgs e)
        {
            //Do something when this property changed
            if (string.IsNullOrWhiteSpace(CurrentValue))
            {
                CurrentValue = "0";
            }
            else if((float.Parse(CurrentValue) <= PositiveMaximumValue && float.Parse(CurrentValue) >= 0)
                    || (Math.Abs(float.Parse(CurrentValue)) >= 0 && Math.Abs(float.Parse(CurrentValue)) <= NegativeMaximumValue))
            {
                /*
                 * Update the Current Value only if the value is in between Positive Maximum and Negative maximum
                 */
                if (float.Parse(CurrentValue) < 0)
                {
                    ProgressBarPositiveGraphic.Width = PositiveProgressBarArea.Width;
                    ProgressBarNegativeGraphic.Width = Math.Abs((float.Parse(CurrentValue) / NegativeMaximumValue) * NegativeProgressBarArea.Width);
                }
                else if (float.Parse(CurrentValue) > 0)
                {
                    ProgressBarPositiveGraphic.Width = Math.Abs(PositiveProgressBarArea.Width - ((float.Parse(CurrentValue) / PositiveMaximumValue) * PositiveProgressBarArea.Width));
                    ProgressBarNegativeGraphic.Width = 0;
                }
                else if (float.Parse(CurrentValue) == 0)
                {
                    ProgressBarPositiveGraphic.Width = PositiveProgressBarArea.Width;
                    ProgressBarNegativeGraphic.Width = 0;
                }
            }
        }
        #endregion
    }
}
