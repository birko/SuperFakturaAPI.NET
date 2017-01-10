using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Converters
{
    public class DictionaryConverter<T1, T2> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                var val = (T2[])serializer.Deserialize(reader, typeof(T2[]));
                return new Dictionary<T1, T2>();
            }
            else if (reader.TokenType == JsonToken.StartObject)
            {
                return (Dictionary<T1, T2>)serializer.Deserialize(reader, typeof(Dictionary<T1, T2>));
            }
            else
            {
                return null;
            }

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
