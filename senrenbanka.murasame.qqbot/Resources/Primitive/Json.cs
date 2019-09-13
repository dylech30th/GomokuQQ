using System.IO;
using Newtonsoft.Json;

namespace senrenbanka.murasame.qqbot.Resources.Primitive
{
    public static class Json
    {
        public static T FromJson<T>(this string source)
        {
            return JsonConvert.DeserializeObject<T>(source);
        }

        public static string ToJson<T>(this T obj)
        {
            var serializer = new JsonSerializer();
            var writer = new StringWriter();
            var jsonTextWriter = new JsonTextWriter(writer)
            {
                Formatting  = Formatting.Indented,
                Indentation = 4,
                IndentChar  = ' '
            };
            serializer.Serialize(jsonTextWriter, obj);
            return writer.ToString();
        }
    }
}