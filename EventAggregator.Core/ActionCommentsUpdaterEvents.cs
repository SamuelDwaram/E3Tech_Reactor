using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAggregator.Core
{
    public class GetDeviceIdForUpdatingActionComments : PubSubEvent
    {
        /* Get Device Id for updating the Action Comments of that particular Device */
    }

    public class UpdateActionCommentsForGivenDeviceId : PubSubEvent<string>
    {
        /* Update the Action Comments of the given Device Id */
    }
}
