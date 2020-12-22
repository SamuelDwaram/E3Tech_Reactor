using System;
using System.Web.Script.Serialization;

namespace E3.SerializationHandler
{
    public class DefaultSerializationHandler : ISerializationHandler
    {
        public T Deserialize<T>(string data)
        {
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;

            return serializer.Deserialize<T>(data);
        }

        public string Serialize<T>(T data)
        {
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;

            return serializer.Serialize(data);
        }
    }
}
