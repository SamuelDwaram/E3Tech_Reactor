using System;
using PylonC.NET;
using System.Collections.Generic;

namespace PylonC.NETSupportLibrary
{
    /* Data class used for holding device data. */
    public class CameraDevice
    {
        public string Name { get; internal set; } /* The friendly name of the device. */
        public string FullName { get; internal set; } /* The full name string which is unique. */
        public uint Index { get; internal set; } /* The index of the device. */
        public string Tooltip { get; internal set; } /* The displayed tooltip */

    }

    /* Provides methods for listing all available devices. */
    public static class DeviceEnumerator
    {
        /* Queries the number of available devices and creates a list with device data. */
        public static List<CameraDevice> EnumerateDevices()
        {
            /* Create a list for the device data. */
            List<CameraDevice> list = new List<CameraDevice>();

            /* Enumerate all camera devices. You must call
            PylonEnumerateDevices() before creating a device. */
            uint count = Pylon.EnumerateDevices();

            /* Get device data from all devices. */
            for( uint i = 0; i < count; ++i)
            {
                /* Create a new data packet. */
                CameraDevice device = new CameraDevice();
                /* Get the device info handle of the device. */
                PYLON_DEVICE_INFO_HANDLE hDi = Pylon.GetDeviceInfoHandle(i);
                /* Get the name. */
                device.Name = Pylon.DeviceInfoGetPropertyValueByName(hDi, Pylon.cPylonDeviceInfoFriendlyNameKey);
                /* Get the serial number */
                device.FullName = Pylon.DeviceInfoGetPropertyValueByName(hDi, Pylon.cPylonDeviceInfoFullNameKey );
                /* Set the index. */
                device.Index = i;

                /* Create tooltip */
                string tooltip = "";
                uint propertyCount = Pylon.DeviceInfoGetNumProperties(hDi);

                if (propertyCount > 0)
                {
                    for (uint j = 0; j < propertyCount; j++)
                    {
                        tooltip += Pylon.DeviceInfoGetPropertyName(hDi, j) + ": " + Pylon.DeviceInfoGetPropertyValueByIndex(hDi, j);
                        if (j != propertyCount - 1)
                        {
                            tooltip += "\n";
                        }
                    }
                }
                device.Tooltip = tooltip;
                /* Add to the list. */
                list.Add(device);
            }
            return list;
        }
    }
}
