using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Linq;

namespace Birko.SuperFaktura.Converters
{
    public class ArrayObjectConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(object);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType.GetInterfaces().Contains(typeof(IEnumerable)) && reader.TokenType == JsonToken.StartObject)
            {
                Type type = objectType.GenericTypeArguments?.FirstOrDefault();
                return new[] { serializer.Deserialize(reader, type) };
            }
            else if (!objectType.GetInterfaces().Contains(typeof(IEnumerable)) && reader.TokenType == JsonToken.StartArray)
            {
                return null;
            }
            return serializer.Deserialize(reader, objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
