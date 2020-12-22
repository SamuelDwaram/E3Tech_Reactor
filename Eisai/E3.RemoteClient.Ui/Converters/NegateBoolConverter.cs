using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace E3.RemoteClient.Ui.Converters
{
    public class NegateBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Dictionary<string, string> fieldDeviceStatus = value as Dictionary<string, string>;
            return fieldDeviceStatus.Count > 0 ? (!System.Convert.ToBoolean(new DeviceParameterExtractor().Convert(value, targetType, parameter, culture))).ToString() : bool.FalseString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
