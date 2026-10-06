using Newtonsoft.Json;
using System;
using System.Globalization;

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

    // Attributes of type "date" are sent as YYYY-MM-DD (intro.md); reading accepts date with or without time.
    public class DateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null && DateTime.TryParse(reader.Value.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return date;
            }
            return objectType == typeof(DateTime?) ? (DateTime?)null : new DateTime();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
        }
    }
}
