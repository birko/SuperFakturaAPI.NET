using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Birko.SuperFaktura.Converters
{
    // Invoice view/list responses return tags nested under an inner "Tag" wrapper:
    //   "Tag": [ { "Tag": { "id": "477", "name": "...", ... } } ]
    // This converter unwraps each element so it maps onto a flat Response.Invoice.Tag.
    public class InvoiceTagConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Response.Invoice.Tag[]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            var token = JToken.Load(reader);
            if (token.Type != JTokenType.Array)
            {
                return null;
            }
            var result = new List<Response.Invoice.Tag>();
            foreach (var element in (JArray)token)
            {
                var tagToken = (element.Type == JTokenType.Object && element["Tag"] != null)
                    ? element["Tag"]
                    : element;
                result.Add(tagToken.ToObject<Response.Invoice.Tag>(serializer));
            }
            return result.ToArray();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
