namespace DZzzz.Net.Serialization.Interfaces
{
    public interface ISerializer<T>
    {
        T Serialize<TK>(TK @object);
        TK Deserialize<TK>(T value);
    }
}