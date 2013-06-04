using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace Nasty.Utils
{

    /**
     * Methods for object serialization/deserialization
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class SerializationUtils
    {

        public static byte[] SerializeObject(object obj) {
                var stream = new MemoryStream();
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
				return stream.ToArray();
	    }

        public static object DeserializeObject(byte[] bytes) {
            var stream = new MemoryStream(bytes) {Position = 0};
            var formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
	    }
    }
}