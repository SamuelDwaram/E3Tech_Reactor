using System;
using System.Web.Script.Serialization;

namespace E3.MqttProvider.Services
{
    public class DefaultSerializer : ISerializer
    {
        private readonly JavaScriptSerializer serializer = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };

        public T Deserialize<T>(string data) => serializer.Deserialize<T>(data);

        public object Deserialize(string data, Type type) => serializer.Deserialize(data, type);

        public string Serialize<T>(T data) => serializer.Serialize(data);
    }
}
