using E3.ReactorManager.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Eisai.ReactorManager.UI.Converters
{
    public class FieldDeviceUsageStatusCheckerConverter : IValueConverter
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

                    return FieldDevicesCommunicatorInstanceProvider.GetInstance<IFieldDevicesCommunicator>()
                                .ReadFieldPointValue<bool>(parameters[0], parameters[1]);
                }
            }
            else
            {
                /* Updating Used Now status on LiveData is received from field devices communicator */
                var liveData = value as FieldPointDataReceivedArgs;

                if (parameter.ToString().Contains("|"))
                {
                    string[] parameters = parameter.ToString().Split('|');

                    if (parameters[0] == liveData.FieldDeviceIdentifier && parameters[1] == liveData.FieldPointIdentifier)
                    {
                        return bool.Parse(liveData.NewFieldPointData);
                    }
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
