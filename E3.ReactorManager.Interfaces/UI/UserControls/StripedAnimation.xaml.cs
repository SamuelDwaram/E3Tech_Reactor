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
    /// Interaction logic for StripedAnimation.xaml
    /// </summary>
    public partial class StripedAnimation : UserControl
    {
        public StripedAnimation()
        {
            InitializeComponent();
            BlackColourPoint.Point = new Point(77.5, 0);
        }

        public static readonly DependencyProperty MaximumValueProperty =
           DependencyProperty.Register("MaximumValue", typeof(int), typeof(StripedAnimation), new
              PropertyMetadata(200, new PropertyChangedCallback(OnMaximumValueChanged)));

        public int MaximumValue
        {
            get { return (int)GetValue(MaximumValueProperty); }
            set { SetValue(MaximumValueProperty, value); }
        }

        private static void OnMaximumValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StripedAnimation toggleButtonUserControl = d as StripedAnimation;
            toggleButtonUserControl.OnMaximumValueChanged(e);
        }

        private void OnMaximumValueChanged(DependencyPropertyChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CurrentValue))
            {
                /*
                 * If Current Value is null or Empty then make it zero
                 */
                CurrentValue = "0";
            }
            else if(float.Parse(CurrentValue) <= MaximumValue && float.Parse(CurrentValue) >= 0)
            {
                /*
                 * Update the BlackColourPoint.Point value only if the Current Value is in between 0 and Maximum Value
                 */
                BlackColourPoint.Point = new Point((float.Parse(CurrentValue) / MaximumValue) * 77.5, 0);
            }
        }

        public static readonly DependencyProperty CurrentValueProperty =
           DependencyProperty.Register("CurrentValue", typeof(string), typeof(StripedAnimation), new
              PropertyMetadata("0", new PropertyChangedCallback(OnCurrentValueChanged)));

        public string CurrentValue
        {
            get { return (string)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }

        private static void OnCurrentValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StripedAnimation toggleButtonUserControl = d as StripedAnimation;
            toggleButtonUserControl.OnCurrentValueChanged(e);
        }

        private void OnCurrentValueChanged(DependencyPropertyChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CurrentValue))
            {
                /*
                 * If Current Value is null or Empty then make it zero
                 */
                CurrentValue = "0";
            }
            else if (float.Parse(CurrentValue) <= MaximumValue && float.Parse(CurrentValue) >= 0)
            {
                /*
                 * Update the BlackColourPoint.Point value only if the Current Value is in between 0 and Maximum Value
                 */
                BlackColourPoint.Point = new Point((float.Parse(CurrentValue) / MaximumValue) * 77.5, 0);
            }
        }
    }
}
