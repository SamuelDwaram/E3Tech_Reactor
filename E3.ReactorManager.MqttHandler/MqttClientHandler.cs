using E3.ReactorManager.MqttHandler.Data;
using E3.ReactorManager.MqttHandler.Interfaces;
using E3.SerializationHandler;
using System;
using System.Configuration;
using System.Text;
using Unity;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace E3.ReactorManager.MqttHandler
{
    public class MqttClientHandler : IMqttClientHandler
    {
        MqttClient mqttClient;
        ISerializationHandler serializationHandler;

        public MqttClientHandler(IUnityContainer containerProvider)
        {
            serializationHandler = containerProvider.Resolve<ISerializationHandler>();

            Initialize();
        }

        public void ConnectToHostServer()
        {
            try
            {
                mqttClient.Connect(Guid.NewGuid().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateQueueTopic(MessageTopic topic)
        {
            string message = "Created Topic " + topic;
            mqttClient.Publish(topic.ToString(), Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        public void PublishMessage<T>(MessageTopic topic, T data)
        {
            mqttClient.Publish(topic.ToString(), Encoding.UTF8.GetBytes(serializationHandler.Serialize<T>(data)), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        public void SubscribeToTopic(MessageTopic topic)
        {
            mqttClient.Subscribe(new string[] { topic.ToString() }, new byte[] { 0 });
        }

        private void MqttServerConnectionClosed(object sender, EventArgs e)
        {
            MqttConnectionClosed?.Invoke(this, new EventArgs());
        }

        public bool GetConnectionStatus()
        {
            if (mqttClient != null)
            {
                return mqttClient.IsConnected;
            }

            return false;
        }

        public void NewMessageReceived(object sender, MqttMsgPublishEventArgs publishReceivedArgs)
        {
            var messageTopic = ParseMessageTopic(publishReceivedArgs.Topic);

            var queueMessageReceivedEventArgs = new QueueMessageEventArgs
            {
                Message = Encoding.UTF8.GetString(publishReceivedArgs.Message),
                Topic = messageTopic,
            };

            NewMessageReceivedFromMqttServer?.Invoke(this, queueMessageReceivedEventArgs);
        }

        public event NewMessageReceivedEventHandler NewMessageReceivedFromMqttServer;
        public event EventHandler MqttMsgSubscribed;
        public event EventHandler MqttMsgPublished;
        public event EventHandler MqttConnectionClosed;
        public event EventHandler MqttClientUnsubscribed;

        #region Mqtt Server Internal Handlers

        private void Initialize()
        {
            string serverIp = ConfigurationManager.AppSettings["MqttServerIP"];
            mqttClient = new MqttClient(serverIp)
            {
                ProtocolVersion = MqttProtocolVersion.Version_3_1_1
            };

            mqttClient.MqttMsgSubscribed += NewClientSubscribed;
            mqttClient.MqttMsgPublished += NewMessagePublished;
            mqttClient.MqttMsgUnsubscribed += ClientUnsubscribed;
            mqttClient.MqttMsgPublishReceived += NewMessageReceived;
            mqttClient.ConnectionClosed += MqttServerConnectionClosed;
        }

        private void ClientUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            MqttClientUnsubscribed?.Invoke(this, new EventArgs());
        }
       
        public MessageTopic ParseMessageTopic(string givenMessageTopic)
        {
            bool parsingStatus = Enum.TryParse(givenMessageTopic, out MessageTopic result);

            if (parsingStatus)
            {
                return result;
            }
            else
            {
                return MessageTopic.NotRecognized;
            }
        }

        public void NewClientSubscribed(object sender, MqttMsgSubscribedEventArgs subscribedArgs)
        {
            MqttMsgSubscribed?.Invoke(this, subscribedArgs);
        }

        public void NewMessagePublished(object sender, MqttMsgPublishedEventArgs publishedArgs)
        {
            MqttMsgPublished?.Invoke(this, publishedArgs);
        }

        #endregion

    }
}
