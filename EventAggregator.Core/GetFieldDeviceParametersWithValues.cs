using Prism.Events;
using System.Collections.Generic;

namespace EventAggregator.Core
{
    public class GetFieldDeviceParametersWithValues : PubSubEvent<string>
    {
        // Requests for the parameters of Field Device by passing DeviceId
    }

    public class ReturnFieldDeviceParametersWithValues : PubSubEvent<Dictionary<string, string>>
    {
        // Returns parameters of Field Device using DeviceId from "GetFieldDeviceParametersWithValues" event
    }
}
