using System;

namespace E3.ReactorManager.MqttHandler.Data
{
    /// <summary>
    /// Queue Message Event Args
    /// </summary>
    public class QueueMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Message Topic
        /// </summary>
        public MessageTopic Topic
        {
            set; get;
        }

        /// <summary>
        /// Message
        /// </summary>
        public string Message
        {
            set; get;
        }

    }
}
