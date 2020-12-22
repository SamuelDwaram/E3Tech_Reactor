using E3.FieldDevicesInfoPopulator.Model.Data;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace E3.FieldDevicesInfoPopulator.Converters
{
    public class IndividualItemExtractorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                ObservableCollection<FieldDeviceStatus> collection = value as ObservableCollection<FieldDeviceStatus>;
                if (collection.Count > int.Parse(parameter.ToString()))
                {
                    return collection[int.Parse(parameter.ToString())];
                }
            }
            return new FieldDeviceStatus();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
