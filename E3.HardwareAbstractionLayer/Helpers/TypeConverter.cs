using System;
using System.ComponentModel;

namespace E3.HardwareAbstractionLayer.Helpers
{
    public static class TConverter
    {
        public static T ChangeType<T>(object value)
        {
            if (typeof(T).IsPrimitive)
            {
                return (T)ChangeType(typeof(T), value);
            }

            return (T)value;
        }

        public static object ChangeType(Type t, object value)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(t);
            return value == null ? default : tc.ConvertFrom(value);
        }

        public static void RegisterTypeConverter<T, TC>() where TC : TypeConverter
        {

            TypeDescriptor.AddAttributes(typeof(T), new TypeConverterAttribute(typeof(TC)));
        }
    }
}
