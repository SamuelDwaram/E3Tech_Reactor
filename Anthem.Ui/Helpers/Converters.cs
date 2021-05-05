using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Anathem.Ui.Helpers
{
    public class TabHeaderSizeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            TabControl tabControl = values[0] as TabControl;
            double width = tabControl.ActualWidth / (tabControl.Items.Count);
            //Subtract 2, otherwise we could overflow to two rows.
            return (width <= 1) ? 0 : (width - 2);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class ParameterExtractorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null || string.IsNullOrWhiteSpace(parameter.ToString()))
            {
                return string.Empty;
            }

            Dictionary<string, string> dict = value as Dictionary<string, string>;
            string fieldPoint = parameter.ToString();
            return dict.ContainsKey(fieldPoint) ? dict[fieldPoint] : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
