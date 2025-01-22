using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Birko.SuperFaktura.Converters
{
    public class ZeroObjectConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(object);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                return null;
            }
            string value = reader.Value?.ToString();
            return value == "0" ? null : JObject.Load(reader).ToObject(objectType, serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject.FromObject(value, serializer).WriteTo(writer);
        }
    }
}
