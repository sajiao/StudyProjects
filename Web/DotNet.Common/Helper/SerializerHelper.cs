using System.Linq;
using System.Text;

namespace DotNet.Common
{
    using System.IO;
    using System.Runtime.Serialization;

    public class SerializerHelper
    {
        public static T DeepCopy<T>(T obj)
        {
            object retval;
            using (var ms = new MemoryStream())
            {
                var ser = new DataContractSerializer(typeof(T));
                ser.WriteObject(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = ser.ReadObject(ms);
            }
            return (T)retval;
        }

        public static string SerializeToString<T>(T obj)
        {
            string contentString = string.Empty;
            using (var ms = new MemoryStream())
            {
                var ser = new DataContractSerializer(typeof(T));
                ser.WriteObject(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                var reader = new StreamReader(ms, Encoding.UTF8);
                contentString = reader.ReadToEnd();
                reader.Close();
            }
            return contentString;
        }
    }
}
