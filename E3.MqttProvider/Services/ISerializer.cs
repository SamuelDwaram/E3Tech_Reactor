using System;

namespace E3.MqttProvider.Services
{
    public interface ISerializer
    {
        string Serialize<T>(T data);

        T Deserialize<T>(string data);

        object Deserialize(string data, Type type);
    }
}
