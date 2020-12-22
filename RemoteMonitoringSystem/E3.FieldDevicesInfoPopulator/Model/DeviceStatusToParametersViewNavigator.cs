namespace E3.FieldDevicesInfoPopulator.Model
{
    public class DeviceStatusToParametersViewNavigator
    {
        public event DeviceStatusToParametersViewNavigatorEventHandler NavigateToParametersView;

        public void SwitchToParametersView(string deviceType)
        {
            NavigateToParametersView?.BeginInvoke(this, deviceType, null, null);
        }
    }

    public delegate void DeviceStatusToParametersViewNavigatorEventHandler(object sender, string deviceType);
}
