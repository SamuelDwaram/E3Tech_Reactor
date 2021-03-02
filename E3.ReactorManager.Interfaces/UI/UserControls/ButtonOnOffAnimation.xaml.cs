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
    /// Interaction logic for ButtonOnOffAnimation.xaml
    /// </summary>
    public partial class ButtonOnOffAnimation : UserControl
    {
        public ButtonOnOffAnimation()
        {
            InitializeComponent();
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(ButtonOnOffAnimation), new PropertyMetadata(LabelChanged));

        private static void LabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ButtonOnOffAnimation buttonOnOffAnimation = d as ButtonOnOffAnimation;
            buttonOnOffAnimation.LabelChanged(e.NewValue.ToString());
        }

        public void LabelChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                PathControl.Visibility = Visibility.Visible;
                buttonLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                PathControl.Visibility = Visibility.Hidden;
                buttonLabel.Visibility = Visibility.Visible;
                buttonLabel.Content = value;
            }
        }

        public static readonly DependencyProperty CurrentStatusProperty =
           DependencyProperty.Register("CurrentStatus", typeof(string), typeof(ButtonOnOffAnimation), new
              PropertyMetadata(bool.FalseString, new PropertyChangedCallback(OnCurrentStatusChanged)));

        public string CurrentStatus
        {
            get { return (string)GetValue(CurrentStatusProperty); }
            set { SetValue(CurrentStatusProperty, value); }
        }

        private static void OnCurrentStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ButtonOnOffAnimation toggleButtonUserControl = d as ButtonOnOffAnimation;
            toggleButtonUserControl.OnCurrentStatusChanged(e);
        }

        private void OnCurrentStatusChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null || !bool.TryParse(e.NewValue.ToString(), out bool result))
            {
                return;
            }

            if (FailureStatus != null)
            {
                if (bool.Parse(FailureStatus))
                {
                    return;
                }
            }
            //Do something when this property changed
            if (CurrentStatus != null)
            {
                if (bool.Parse(CurrentStatus))
                {
                    EllipseControl.Fill = new SolidColorBrush(Color.FromRgb(91, 201, 208));

                    PathControl.Fill = new SolidColorBrush(Color.FromRgb(30, 36, 50));
                    PathControl.Data = Geometry.Parse("M-378.5,1230.2c-0.7,0-1.4-0.2-2-0.5c-1.9-1.1-2.6-3.6-1.5-5.5c0.4-0.6,0.9-1.1,1.5-1.5c0.3-0.2,0.6-0.1,0.8,0.2c0.2,0.3,0.1,0.6-0.2,0.8c-0.4,0.3-0.8,0.6-1.1,1.1c-0.8,1.4-0.3,3.2,1.1,4c0.7,0.4,1.5,0.5,2.2,0.3c0.8-0.2,1.4-0.7,1.8-1.4c0.4-0.7,0.5-1.5,0.3-2.2c-0.2-0.8-0.7-1.4-1.4-1.8c-0.3-0.2-0.4-0.5-0.2-0.8c0.2-0.3,0.5-0.4,0.8-0.2c0.9,0.5,1.6,1.4,1.9,2.5s0.1,2.1-0.4,3.1c-0.5,0.9-1.4,1.6-2.5,1.9C-377.8,1230.1-378.1,1230.2-378.5,1230.2zM-378.5,1221.8L-378.5,1221.8c0.3,0,0.6,0.3,0.6,0.6v2.6c0,0.3-0.2,0.6-0.6,0.6l0,0c-0.3,0-0.6-0.3-0.6-0.6v-2.6C-379,1222.1-378.8,1221.8-378.5,1221.8z");
                }
                else
                {
                    EllipseControl.Fill = new SolidColorBrush(Color.FromRgb(30, 36, 50));

                    PathControl.Fill = new SolidColorBrush(Color.FromRgb(142, 148, 161));
                    PathControl.Data = Geometry.Parse("M-378.5,1221.8L-378.5,1221.8c0.3,0,0.6,0.3,0.6,0.6v2.6c0,0.3-0.2,0.6-0.6,0.6l0,0c-0.3,0-0.6-0.3-0.6-0.6v-2.6C-379,1222.1-378.8,1221.8-378.5,1221.8zM-378.5,1230.2c-0.7,0-1.4-0.2-2-0.5c-1.9-1.1-2.6-3.6-1.5-5.5c0.4-0.6,0.9-1.1,1.5-1.5c0.3-0.2,0.6-0.1,0.8,0.2c0.2,0.3,0.1,0.6-0.2,0.8c-0.4,0.3-0.8,0.6-1.1,1.1c-0.8,1.4-0.3,3.2,1.1,4c0.7,0.4,1.5,0.5,2.2,0.3c0.8-0.2,1.4-0.7,1.8-1.4c0.4-0.7,0.5-1.5,0.3-2.2c-0.2-0.8-0.7-1.4-1.4-1.8c-0.3-0.2-0.4-0.5-0.2-0.8c0.2-0.3,0.5-0.4,0.8-0.2c0.9,0.5,1.6,1.4,1.9,2.5s0.1,2.1-0.4,3.1c-0.5,0.9-1.4,1.6-2.5,1.9C-377.8,1230.1-378.1,1230.2-378.5,1230.2z");
                }
            }
        }

        public static readonly DependencyProperty FailureStatusProperty =
           DependencyProperty.Register("FailureStatus", typeof(string), typeof(ButtonOnOffAnimation), new
              PropertyMetadata(bool.FalseString, new PropertyChangedCallback(OnFailureStatusChanged)));

        public string FailureStatus
        {
            get { return (string)GetValue(FailureStatusProperty); }
            set { SetValue(FailureStatusProperty, value); }
        }

        private static void OnFailureStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ButtonOnOffAnimation toggleButtonUserControl = d as ButtonOnOffAnimation;
            toggleButtonUserControl.OnFailureStatusChanged(e);
        }

        private void OnFailureStatusChanged(DependencyPropertyChangedEventArgs e)
        {
            if (!bool.TryParse(e.NewValue.ToString(), out bool result))
            {
                return;
            }

            //Do something when this property changed
            if (FailureStatus != null)
            {
                if (bool.Parse(FailureStatus))
                {
                    EllipseControl.Fill = new SolidColorBrush(Color.FromRgb(255, 165, 0));

                    PathControl.Fill = new SolidColorBrush(Color.FromRgb(30, 36, 50));
                    PathControl.Data = Geometry.Parse("M-378.5,1230.2c-0.7,0-1.4-0.2-2-0.5c-1.9-1.1-2.6-3.6-1.5-5.5c0.4-0.6,0.9-1.1,1.5-1.5c0.3-0.2,0.6-0.1,0.8,0.2c0.2,0.3,0.1,0.6-0.2,0.8c-0.4,0.3-0.8,0.6-1.1,1.1c-0.8,1.4-0.3,3.2,1.1,4c0.7,0.4,1.5,0.5,2.2,0.3c0.8-0.2,1.4-0.7,1.8-1.4c0.4-0.7,0.5-1.5,0.3-2.2c-0.2-0.8-0.7-1.4-1.4-1.8c-0.3-0.2-0.4-0.5-0.2-0.8c0.2-0.3,0.5-0.4,0.8-0.2c0.9,0.5,1.6,1.4,1.9,2.5s0.1,2.1-0.4,3.1c-0.5,0.9-1.4,1.6-2.5,1.9C-377.8,1230.1-378.1,1230.2-378.5,1230.2zM-378.5,1221.8L-378.5,1221.8c0.3,0,0.6,0.3,0.6,0.6v2.6c0,0.3-0.2,0.6-0.6,0.6l0,0c-0.3,0-0.6-0.3-0.6-0.6v-2.6C-379,1222.1-378.8,1221.8-378.5,1221.8z");
                }
                else
                {
                    if (bool.Parse(CurrentStatus))
                    {
                        EllipseControl.Fill = new SolidColorBrush(Color.FromRgb(91, 201, 208));

                        PathControl.Fill = new SolidColorBrush(Color.FromRgb(30, 36, 50));
                        PathControl.Data = Geometry.Parse("M-378.5,1230.2c-0.7,0-1.4-0.2-2-0.5c-1.9-1.1-2.6-3.6-1.5-5.5c0.4-0.6,0.9-1.1,1.5-1.5c0.3-0.2,0.6-0.1,0.8,0.2c0.2,0.3,0.1,0.6-0.2,0.8c-0.4,0.3-0.8,0.6-1.1,1.1c-0.8,1.4-0.3,3.2,1.1,4c0.7,0.4,1.5,0.5,2.2,0.3c0.8-0.2,1.4-0.7,1.8-1.4c0.4-0.7,0.5-1.5,0.3-2.2c-0.2-0.8-0.7-1.4-1.4-1.8c-0.3-0.2-0.4-0.5-0.2-0.8c0.2-0.3,0.5-0.4,0.8-0.2c0.9,0.5,1.6,1.4,1.9,2.5s0.1,2.1-0.4,3.1c-0.5,0.9-1.4,1.6-2.5,1.9C-377.8,1230.1-378.1,1230.2-378.5,1230.2zM-378.5,1221.8L-378.5,1221.8c0.3,0,0.6,0.3,0.6,0.6v2.6c0,0.3-0.2,0.6-0.6,0.6l0,0c-0.3,0-0.6-0.3-0.6-0.6v-2.6C-379,1222.1-378.8,1221.8-378.5,1221.8z");
                    }
                    else
                    {
                        EllipseControl.Fill = new SolidColorBrush(Color.FromRgb(30, 36, 50));

                        PathControl.Fill = new SolidColorBrush(Color.FromRgb(142, 148, 161));
                        PathControl.Data = Geometry.Parse("M-378.5,1221.8L-378.5,1221.8c0.3,0,0.6,0.3,0.6,0.6v2.6c0,0.3-0.2,0.6-0.6,0.6l0,0c-0.3,0-0.6-0.3-0.6-0.6v-2.6C-379,1222.1-378.8,1221.8-378.5,1221.8zM-378.5,1230.2c-0.7,0-1.4-0.2-2-0.5c-1.9-1.1-2.6-3.6-1.5-5.5c0.4-0.6,0.9-1.1,1.5-1.5c0.3-0.2,0.6-0.1,0.8,0.2c0.2,0.3,0.1,0.6-0.2,0.8c-0.4,0.3-0.8,0.6-1.1,1.1c-0.8,1.4-0.3,3.2,1.1,4c0.7,0.4,1.5,0.5,2.2,0.3c0.8-0.2,1.4-0.7,1.8-1.4c0.4-0.7,0.5-1.5,0.3-2.2c-0.2-0.8-0.7-1.4-1.4-1.8c-0.3-0.2-0.4-0.5-0.2-0.8c0.2-0.3,0.5-0.4,0.8-0.2c0.9,0.5,1.6,1.4,1.9,2.5s0.1,2.1-0.4,3.1c-0.5,0.9-1.4,1.6-2.5,1.9C-377.8,1230.1-378.1,1230.2-378.5,1230.2z");
                    }
                }
            }
        }
    }
}
