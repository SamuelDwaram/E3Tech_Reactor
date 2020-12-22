namespace E3.SerializationHandler
{
    public interface ISerializationHandler
    {
        string Serialize<T>(T data);

        T Deserialize<T>(string data);
    }
}
