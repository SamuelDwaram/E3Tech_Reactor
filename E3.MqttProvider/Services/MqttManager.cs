using E3.MqttProvider.Models;
using System;
using System.Configuration;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace E3.MqttProvider.Services
{
    public class MqttManager : IMqttManager
    {
        private readonly ISerializer serializer;
        private MqttClient mqttClient;

        public MqttManager(ISerializer serializer)
        {
            this.serializer = serializer;
            Initialize();
        }

        private void Initialize()
        {
            string brokerIp = ConfigurationManager.AppSettings["MqttBrokerIp"];
            if (string.IsNullOrWhiteSpace(brokerIp))
            {
                // No MqttServerIp found. Try with "127.0.0.1".
                brokerIp = "127.0.0.1";
            }
            mqttClient = new MqttClient(brokerIp)
            {
                ProtocolVersion = MqttProtocolVersion.Version_3_1_1,
            };
            mqttClient.MqttMsgSubscribed += MqttClient_MqttMsgSubscribed;
            mqttClient.MqttMsgPublished += MqttClient_MqttMsgPublished;
            mqttClient.MqttMsgUnsubscribed += MqttClient_MqttMsgUnsubscribed;
            mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
            mqttClient.ConnectionClosed += MqttClient_ConnectionClosed;
        }

        private void MqttClient_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            if (MqttMessagePublished == null)
            {
                return;
            }

            Delegate[] receivers = MqttMessagePublished.GetInvocationList();
            foreach (Delegate receiver in receivers)
            {
                ((MqttMessagePublishedEventHandler)receiver).BeginInvoke(mqttClient.ClientId, e.MessageId, null, null);
            }
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            MqttMessage receivedMsg = serializer.Deserialize<MqttMessage>(GetString(e.Message));
            receivedMsg.ExtractedMessage = serializer.Deserialize(GetString(receivedMsg.Message), Type.GetType(receivedMsg.MessageType));

            if (MqttMessageReceived == null)
            {
                return;
            }

            Delegate[] receivers = MqttMessageReceived.GetInvocationList();
            foreach (Delegate receiver in receivers)
            {
                ((MqttMessageReceivedEventHandler)receiver).BeginInvoke(mqttClient.ClientId, receivedMsg, null, null);
            }
        }

        private void MqttClient_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            if (MqttTopicSubscribed == null)
            {
                return;
            }

            Delegate[] receivers = MqttTopicSubscribed.GetInvocationList();
            foreach (Delegate receiver in receivers)
            {
                ((MqttClientEventHandler)receiver).BeginInvoke(mqttClient.ClientId, e.MessageId.ToString(), null, null);
            }
        }

        private void MqttClient_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            if (MqttTopicUnsubscribed == null)
            {
                return;
            }

            Delegate[] receivers = MqttTopicUnsubscribed.GetInvocationList();
            foreach (Delegate receiver in receivers)
            {
                ((MqttClientEventHandler)receiver).BeginInvoke(mqttClient.ClientId, e.MessageId.ToString(), null, null);
            }
        }

        private void MqttClient_ConnectionClosed(object sender, EventArgs e)
        {
            if (MqttConnectionClosed == null)
            {
                return;
            }

            Delegate[] receivers = MqttConnectionClosed.GetInvocationList();
            foreach (Delegate receiver in receivers)
            {
                ((MqttClientEventHandler)receiver).BeginInvoke(mqttClient.ClientId, string.Empty, null, null);
            }
        }

        public void Connect()
        {
            string clientId = ConfigurationManager.AppSettings["MqttClientId"];
            if (string.IsNullOrWhiteSpace(clientId))
            {
                // No ClientId found. Assing Random Guid.
                clientId = Guid.NewGuid().ToString();
            }
            mqttClient.Connect(clientId);
        }

        public void CreateTopic(string topic)
        {
            Publish(topic, $"Created Topic : {topic}");
        }

        public bool IsConnected() => mqttClient.IsConnected;

        public void Publish<T>(string topic, T data)
        {
            mqttClient.Publish(topic, GetBytes(new MqttMessage { Topic = topic,
                MessageType = typeof(T).AssemblyQualifiedName, Message = GetBytes(data) }),
                MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        public string GetString(byte[] bytes) => Encoding.ASCII.GetString(bytes);

        public byte[] GetBytes<T>(T data) => Encoding.ASCII.GetBytes(serializer.Serialize(data));

        public void SubscribeToTopic(string topic) => mqttClient.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

        public void DisConnect()
        {
            if (mqttClient.IsConnected)
            {
                mqttClient.Disconnect();
            }
        }

        public event MqttMessageReceivedEventHandler MqttMessageReceived;
        public event MqttClientEventHandler MqttTopicSubscribed;
        public event MqttClientEventHandler MqttTopicUnsubscribed;
        public event MqttMessageReceivedEventHandler MqttMessagePublished;
        public event MqttClientEventHandler MqttConnectionClosed;
    }

}
