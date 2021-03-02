using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace E3.RampManager.UserControls
{
    /// <summary>
    /// Interaction logic for RampStep.xaml
    /// </summary>
    public partial class RampStep : UserControl
    {
        public RampStep()
        {
            InitializeComponent();
        }

        #region RemainingTime
        public string RemainingTime
        {
            get { return (string)GetValue(RemainingTimeProperty); }
            set { SetValue(RemainingTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RemainingTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RemainingTimeProperty =
            DependencyProperty.Register("RemainingTime", typeof(string), typeof(RampStep), new PropertyMetadata(string.Empty, RemainingTimeChanged));

        private static void RemainingTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RampStep rampStep = d as RampStep;
            //show remaining time only if this is current step
            rampStep.RemainingTimeChanged(rampStep.grid.Background == Brushes.Orange ? Convert.ToString(e.NewValue) : string.Empty);
        }

        private void RemainingTimeChanged(string remainingTime) => remainingTimeText.Content = remainingTime;
        #endregion

        #region RampStatus
        public bool RampStatus
        {
            get { return (bool)GetValue(RampStatusProperty); }
            set { SetValue(RampStatusProperty, value); }
        }
        
        public static readonly DependencyProperty RampStatusProperty =
            DependencyProperty.Register("RampStatus", typeof(bool), typeof(RampStep), new PropertyMetadata(false, RampStatusChanged));

        private static void RampStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RampStep rampStep = d as RampStep;
            rampStep.RampStatusChanged((bool)e.NewValue);
        }

        public void RampStatusChanged(bool rampStatus)
        {
            setPointText.IsReadOnly = rampStatus;
            minsToMaintainText.IsReadOnly = rampStatus;
            skipButton.Visibility = rampStatus ? Visibility.Visible : Visibility.Hidden;
            removeButton.Visibility = rampStatus ? Visibility.Hidden : Visibility.Visible;
            //Update current step
            UpdateBackground(((Models.RampStep)DataContext).StepIndex == CurrentStep && rampStatus);
        }
        #endregion

        #region CurrentStep
        public int CurrentStep
        {
            get { return (int)GetValue(CurrentStepProperty); }
            set { SetValue(CurrentStepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentStep.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentStepProperty =
            DependencyProperty.Register("CurrentStep", typeof(int), typeof(RampStep), new PropertyMetadata(-1, CurrentStepChanged));

        private static void CurrentStepChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RampStep rampStep = d as RampStep;
            rampStep.UpdateBackground(((Models.RampStep)rampStep.DataContext).StepIndex == (int)e.NewValue && rampStep.RampStatus);
        }

        public void UpdateBackground(bool isRampCurrentStep)
        {
            grid.Background = isRampCurrentStep ? Brushes.Orange : new SolidColorBrush(Color.FromRgb(39, 46, 62));
        }
        #endregion
    }
}
