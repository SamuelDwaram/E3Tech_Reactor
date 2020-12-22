using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.RemoteClient.Ui.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace E3.RemoteClient.Ui.Converters
{
    public class DeviceStatusExtractor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                /* Updating Used Now status on page load */
                if (parameter.ToString().Contains("|"))
                {
                    //parameter[0] is FieldDevice Identifier
                    //parameter[1] is FieldPoint Identifier
                    string[] parameters = parameter.ToString().Split('|');

                    return AnyInstanceExtractor.GetInstance<IFieldDevicesCommunicator>().ReadFieldPointValue<bool>(parameters[0], parameters[1]);
                }
            }
            else
            {
                /* Updating Used Now status on LiveData is received from Hardware Layer */
                string[] liveData = value as string[];
                string deviceId = liveData[0];
                string fieldPointId = liveData[1];
                string newFieldPointData = liveData[2];

                if (parameter.ToString().Contains("|"))
                {
                    string[] parameters = parameter.ToString().Split('|');

                    if (parameters[0] == deviceId && parameters[1] == fieldPointId)
                    {
                        return bool.Parse(newFieldPointData);
                    }
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class DeviceParameterExtractor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return false;
            }
            else
            {
                Dictionary<string, string> fieldDeviceStatus = value as Dictionary<string, string>;
                return fieldDeviceStatus.ContainsKey(parameter.ToString()) ? fieldDeviceStatus[parameter.ToString()] : "0";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
