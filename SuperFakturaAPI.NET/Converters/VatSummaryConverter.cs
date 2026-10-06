using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Birko.SuperFaktura.Converters
{
    // VatSummary is a PHP array keyed by VAT rate: JSON array when the only rate is 0 ([{...}]),
    // otherwise an object ({"20": {...}, "10": {...}}). The key is stored in VATSummary.Rate.
    public class VatSummaryConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IEnumerable<Response.Expense.VATSummary>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var result = new List<Response.Expense.VATSummary>();
            if (token is JArray array)
            {
                for (int i = 0; i < array.Count; i++)
                {
                    result.Add(Read(array[i], i, serializer));
                }
            }
            else if (token is JObject obj)
            {
                foreach (var property in obj.Properties())
                {
                    decimal.TryParse(property.Name, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal rate);
                    result.Add(Read(property.Value, rate, serializer));
                }
            }
            return result;
        }

        private static Response.Expense.VATSummary Read(JToken token, decimal rate, JsonSerializer serializer)
        {
            var summary = token.ToObject<Response.Expense.VATSummary>(serializer);
            summary.Rate = rate;
            return summary;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var array = new JArray();
            foreach (var summary in (IEnumerable<Response.Expense.VATSummary>)value)
            {
                array.Add(JObject.FromObject(summary));
            }
            array.WriteTo(writer);
        }
    }
}
