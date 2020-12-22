namespace E3.ReactorManager.MqttHandler.Data
{
    /// <summary>
    /// Class to represent Message Topic
    /// </summary>
    public enum MessageTopic
    {
        InitializeFieldDevices,
        AlarmRaised,
        NewDataLoggedIntoDatabase,
        CommandToDevice,
        LiveData,
        SystemShutDown,
        NotRecognized,
    }
}
