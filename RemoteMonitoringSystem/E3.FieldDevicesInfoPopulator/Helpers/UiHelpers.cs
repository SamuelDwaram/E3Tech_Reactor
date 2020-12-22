using E3.FieldDevicesInfoPopulator.Views;
using System.Windows;
using E3.FieldDevicesInfoPopulator.ViewModels;
using System;

namespace E3.FieldDevicesInfoPopulator.Helpers
{
    public class UiHelpers
    {
        #region DeviceId
        public static string GetDeviceId(DependencyObject obj)
        {
            return (string)obj.GetValue(DeviceIdProperty);
        }

        public static void SetDeviceId(DependencyObject obj, string value)
        {
            obj.SetValue(DeviceIdProperty, value);
        }

        public static readonly DependencyProperty DeviceIdProperty =
            DependencyProperty.RegisterAttached("DeviceId", typeof(string), typeof(UiHelpers), new PropertyMetadata(DeviceIdChanged));

        private static void DeviceIdChanged(DependencyObject uiElement, DependencyPropertyChangedEventArgs e)
        {
            if (!(uiElement is DeviceRunningTimeView))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(Convert.ToString(e.NewValue)))
            {
                //Skip.
            }
            else
            {
                DeviceRunningTimeView view = uiElement as DeviceRunningTimeView;
                DeviceRunningTimeViewModel viewModel = (DeviceRunningTimeViewModel)view.DataContext;
                viewModel.SetDeviceIdCommand.Execute(GetDeviceId(uiElement));
            }
        }
        #endregion
    }
}
