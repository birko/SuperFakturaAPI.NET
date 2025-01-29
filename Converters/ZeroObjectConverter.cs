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
            return value == "0" ? null : serializer.Deserialize(reader, objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
