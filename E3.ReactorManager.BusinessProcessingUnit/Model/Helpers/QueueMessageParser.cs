using E3.ReactorManager.MqttHandler.Data;
using E3.SerializationHandler;
using Unity;

namespace E3.ReactorManager.BusinessProcessingUnit.Model.Helpers
{
    public class QueueMessageParser
    {
        ISerializationHandler serializationHandler;

        public QueueMessageParser(IUnityContainer containerProvider)
        {
            serializationHandler = containerProvider.Resolve<ISerializationHandler>();
        }

        public T ParseQueueMessage<T>(QueueMessageEventArgs queueMessageEventArgs)
        {
            return serializationHandler.Deserialize<T>(queueMessageEventArgs.Message);
        }

    }
}
