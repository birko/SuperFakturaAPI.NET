using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Converters
{

    public class DateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (DateTime.TryParse(reader.Value.ToString(), out DateTime datetimme))
            {
                return datetimme;
            }
            return objectType == typeof(DateTime?) ? (DateTime?)null : new DateTime();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }
}
