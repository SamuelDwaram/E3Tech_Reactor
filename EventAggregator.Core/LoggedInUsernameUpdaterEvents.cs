using E3.UserManager.Model.Data;
using Prism.Events;

namespace EventAggregator.Core
{
    public class GetLoggedInUserName : PubSubEvent
    {
        /* Get the name of the User currently Logged In */
    }

    public class UpdateLoggedInUsername : PubSubEvent<string>
    {
        /* Update the currently logged in User name */
    }

    public class GetLoggedInUserDetails : PubSubEvent
    {
        /* Gets the currently logged in User Details */
    }

    public class UpdateLoggedInUserDetails : PubSubEvent<User>
    {
        /* Updates the Logged In User Details */
    }

}
