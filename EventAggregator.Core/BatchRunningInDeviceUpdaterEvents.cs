using Prism.Events;

namespace EventAggregator.Core
{
    public class GetDeviceIdForUpdatingExperimentInfo : PubSubEvent
    {
        /* Gets the Device Id for Updating the Experiment Info */
    }

    public class UpdateExperimentInfoOfDeviceEvent : PubSubEvent<string>
    {
        /* Pass the Field Device Identifier for Updating the Experiment Info */
    }
}
