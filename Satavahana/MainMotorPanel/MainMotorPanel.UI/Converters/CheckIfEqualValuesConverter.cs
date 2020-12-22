using System;
using System.Globalization;
using System.Windows.Data;

namespace MainMotorPanel.UI.Converters
{
    public class CheckIfEqualValuesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length >= 2)
            {
                if (values[0] != null && values[1] != null)
                {
                    if (values[0].ToString() == values[1].ToString())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot Convert Back");
        }
    }
}
