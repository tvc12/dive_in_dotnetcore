using Newtonsoft.Json;

namespace CatBasicExample.Utils
{
    public static class JsonUtils
    {
        
        public static string ToJson(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static T FromJson<T>(string json)
        {
            return (T)JsonConvert.DeserializeObject(json);
        }
    }
}