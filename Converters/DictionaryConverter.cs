using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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
                IEnumerable<T2> val = (IEnumerable<T2>)serializer.Deserialize(reader, typeof(IEnumerable<T2>));
                if (typeof(T1) == typeof(T2) && val?.Count() > 0)
                {
                    var dictionary = new Dictionary<T2, T2>();
                    foreach (var item in val)
                    {
                        dictionary.Add(item, item);
                    }
                    return dictionary;
                }
                else if (typeof(T1) == typeof(string) && val?.Count() > 0)
                {
                    var dictionary = new Dictionary<string, T2>();
                    foreach (var item in val)
                    {
                        dictionary.Add(item.ToString(), item);
                    }
                    return dictionary;
                }
                return new Dictionary<T1, T2>();
            }
            else if (reader.TokenType == JsonToken.StartObject)
            {
                return (IDictionary<T1, T2>)serializer.Deserialize(reader, typeof(Dictionary<T1, T2>));
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
