using System;
using E3.EquipmentUsageTracker.ViewModels;
using Prism.Unity.Ioc;
using Prism.Ioc;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Unity;

namespace E3.EquipmentUsageTracker.Converters
{
    public class ItemExtractorFromCollectionConverter : IValueConverter
    {
        EquipmentOccupancyViewModel equipmentOccupancyViewModel;

        public ItemExtractorFromCollectionConverter()
        {
            UnityContainerExtension container = (UnityContainerExtension)Application.Current.Resources["IoC"];
            equipmentOccupancyViewModel = container.Resolve<EquipmentOccupancyViewModel>();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IList<EquipmentOccupancyViewModel> viewModelCollection = value as IList<EquipmentOccupancyViewModel>;
            int requiredItemIndex = System.Convert.ToInt32(parameter);
            if (requiredItemIndex <= viewModelCollection.Count)
            {
                return viewModelCollection[requiredItemIndex - 1];
            }
            else
            {
                return equipmentOccupancyViewModel;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
