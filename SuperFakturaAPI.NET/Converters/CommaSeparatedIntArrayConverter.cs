using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Birko.SuperFaktura.Converters
{
    // The API takes some id lists as a comma separated string, e.g. "proforma_id": "1,2,3".
    // Null entries are skipped when writing; an empty list is written as null.
    public class CommaSeparatedIntArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(int?[]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            var token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                return token.Select(t => int.TryParse(t.ToString(), out int id) ? id : (int?)null).ToArray();
            }
            var value = token.ToString();
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            return value.Split(',')
                .Select(s => int.TryParse(s.Trim(), out int id) ? id : (int?)null)
                .ToArray();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var ids = (value as int?[])?.Where(id => id.HasValue).ToArray();
            if (ids == null || ids.Length == 0)
            {
                writer.WriteNull();
                return;
            }
            writer.WriteValue(string.Join(",", ids));
        }
    }
}
