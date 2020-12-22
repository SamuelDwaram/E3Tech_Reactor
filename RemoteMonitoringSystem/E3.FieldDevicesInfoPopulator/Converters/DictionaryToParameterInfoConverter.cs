using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace E3.FieldDevicesInfoPopulator.Converters
{
    public class DictionaryToParameterInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrWhiteSpace(value.ToString()))
            {
                Dictionary<string, string> dictionary = value as Dictionary<string, string>;

                if (dictionary.ContainsKey(parameter.ToString()))
                {
                    return dictionary[parameter.ToString()];
                }
            }

            return "NC";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }

    public class DictionaryToParameterAlarmStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrWhiteSpace(value.ToString()))
            {
                Dictionary<string, bool> dictionary = value as Dictionary<string, bool>;

                if (dictionary.ContainsKey(parameter.ToString()))
                {
                    return dictionary[parameter.ToString()];
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
