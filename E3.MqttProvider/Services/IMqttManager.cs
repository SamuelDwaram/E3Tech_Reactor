using E3.MqttProvider.Models;

namespace E3.MqttProvider.Services
{
    public interface IMqttManager
    {
        event MqttMessageReceivedEventHandler MqttMessagePublished; //Message Published to Mqtt Server successfully
        event MqttMessageReceivedEventHandler MqttMessageReceived;  //MessageReceived from Mqtt Server
        event MqttClientEventHandler MqttTopicSubscribed;   //MqttClient subscribed to a topic
        event MqttClientEventHandler MqttTopicUnsubscribed; //MqttClient unsubscribed from a topic
        event MqttClientEventHandler MqttConnectionClosed;  //MqttClient got disconnected

        bool IsConnected();

        /// <summary>
        /// Connects to the Mqtt server
        /// </summary>
        void Connect();

        /// <summary>
        /// Disconnects from Mqtt Server
        /// </summary>
        void DisConnect();

        /// <summary>
        /// Publish Message to Queue Topic
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="data"></param>
        void Publish<T>(string topic, T data);

        /// <summary>
        /// Create Queue Topic
        /// </summary>
        /// <param name="topic"></param>
        void CreateTopic(string topic);

        /// <summary>
        /// Start Listening to Topic(subscribing to Topic)
        /// </summary>
        /// <param name="topic"></param>
        void SubscribeToTopic(string topic);
    }

    public delegate void MqttMessagePublishedEventHandler(string clientId, ushort messageId);
    public delegate void MqttMessageReceivedEventHandler(string clientId, MqttMessage mqttMessage);
    public delegate void MqttClientEventHandler(string clientId, string messageId = "");
}