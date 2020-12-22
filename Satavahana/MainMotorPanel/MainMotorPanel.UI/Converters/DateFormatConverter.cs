using System;
using System.Globalization;
using System.Windows.Data;

namespace MainMotorPanel.UI.Converters
{
    public class DateFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value.ToString()))
            {
                return value;
            }

            return System.Convert.ToDateTime(value).ToString("HH:mm:ss dd-MM-yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
