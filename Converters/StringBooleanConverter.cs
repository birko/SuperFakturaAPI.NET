using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Converters
{
    public class StringBooleanConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool) || objectType == typeof(bool?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string value = reader.Value.ToString();
            return !string.IsNullOrEmpty(value) && value.Trim().Equals("1") ? true : (object)false;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            bool convertedValue = (bool)value;
            writer.WriteValue(convertedValue ? "1" : "0");
        }
    }

    public class LowerBooleanConverter : StringBooleanConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string value = reader.Value.ToString();
            return !string.IsNullOrEmpty(value) && !value.Trim().Equals("true") ? true : base.ReadJson(reader, objectType, existingValue, serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            bool convertedValue = (bool)value;
            writer.WriteValue(convertedValue ? "true" : "false");
        }
    }
}
