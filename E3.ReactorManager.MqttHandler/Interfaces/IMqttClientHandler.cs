using E3.ReactorManager.MqttHandler.Data;
using System;

namespace E3.ReactorManager.MqttHandler.Interfaces
{
    /// <summary>
    /// Contarct for message Queue Client
    /// </summary>
    public interface IMqttClientHandler
    {
        /// <summary>
        /// Invokes when a new message is received by the Mqtt Server
        /// </summary>
        event NewMessageReceivedEventHandler NewMessageReceivedFromMqttServer;

        /// <summary>
        /// Invokes when a new client is subscribed to a topic in the Mqtt Server
        /// </summary>
        event EventHandler MqttMsgSubscribed;

        /// <summary>
        /// Invokes when an existing client unsubscribed
        /// </summary>
        event EventHandler MqttClientUnsubscribed;

        /// <summary>
        /// Invokes when a new message is published to a topic in the Mqtt Server
        /// by a client
        /// </summary>
        event EventHandler MqttMsgPublished;

        /// <summary>
        /// Invokes when the connection to the Mqtt Server is lost
        /// </summary>
        event EventHandler MqttConnectionClosed;

        bool GetConnectionStatus();

        /// <summary>
        /// Connects to the Mosquitto server
        /// </summary>
        void ConnectToHostServer();

        /// <summary>
        /// Publish Message to Queue Topic
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="data"></param>
        void PublishMessage<T>(MessageTopic topic, T data);

        /// <summary>
        /// Create Queue Topic
        /// </summary>
        /// <param name="topic"></param>
        void CreateQueueTopic(MessageTopic topic);

        /// <summary>
        /// Start Listening to Topic(subscribing to Topic)
        /// </summary>
        /// <param name="topic"></param>
        void SubscribeToTopic(MessageTopic topic);
    }

    public delegate void NewMessageReceivedEventHandler(object sender, QueueMessageEventArgs queueMessageEventArgs);
}
