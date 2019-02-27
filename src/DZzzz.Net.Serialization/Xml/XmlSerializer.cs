using System.IO;
using System.Text;

using DZzzz.Net.Serialization.Interfaces;

namespace DZzzz.Net.Serialization.Xml
{
    public class XmlSerializer : ISerializer<string>
    {
        public string Serialize<TK>(TK @object)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(TK));

            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, @object);

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        public TK Deserialize<TK>(string value)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(TK));

            byte[] bytes = Encoding.UTF8.GetBytes(value);

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return (TK)serializer.Deserialize(stream);
            }
        }
    }
}