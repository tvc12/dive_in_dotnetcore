using System;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CatBasicExample.Utils
{
    public static class JsonUtils
    {
        private static MvcNewtonsoftJsonOptions serializerOptions = getOptions();

        private static MvcNewtonsoftJsonOptions getOptions()
        {
            var options = new MvcNewtonsoftJsonOptions();
            options.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() };
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            return options;
        }

        public static string ToJson(object value)
        {
            return JsonConvert.SerializeObject(value, serializerOptions.SerializerSettings);
        }

        public static T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, serializerOptions.SerializerSettings);
        }
    }
}