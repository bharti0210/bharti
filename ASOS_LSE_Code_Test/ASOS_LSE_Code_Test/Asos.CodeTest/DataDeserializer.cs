namespace Asos.CodeTest
{
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    public class DataDeserializer
    {
        public static T Deserialize<T>(string data) where T : class
        {
            var js = new DataContractJsonSerializer(typeof(T));

            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(data)))
            {
                var output = js.ReadObject(ms) as T;
                return output;
            }
        }
    }

}