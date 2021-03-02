using System;
using System.Windows;
using System.Windows.Controls;

namespace E3.RampManager.Helpers
{
    public class UiHelpers
    {
        #region StepIndex
        public static object GetStepIndex(DependencyObject obj)
        {
            return obj.GetValue(StepIndexProperty);
        }

        public static void SetStepIndex(DependencyObject obj, object value)
        {
            obj.SetValue(StepIndexProperty, value);
        }

        // Using a DependencyProperty as the backing store for StepIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepIndexProperty =
            DependencyProperty.RegisterAttached("StepIndex", typeof(object), typeof(UiHelpers), new PropertyMetadata(-1, StepIndexChanged));

        private static void StepIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is ContentControl))
            {
                return;
            }

            ContentControl contentControl = d as ContentControl;
            contentControl.SetValue(ContentControl.ContentProperty, Convert.ToInt32(e.NewValue) + 1);
        }
        #endregion
    }
}
