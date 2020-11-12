using Birko.SuperFaktura.Response.Invoice;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Linq;

namespace Birko.SuperFaktura.Converters
{
    public class InvoiceItemConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            ListItem item = new ListItem();
            string startPath = reader.Path;
            do
            {
                reader.Read(); // Read next json token
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    reader.Read(); // read value from property
                    string[] path = reader.Path.Split(new[] { '.' });
                    string lastPath = path.LastOrDefault();
                    switch (lastPath)
                    {
                        case "Client":
                            item.Client = (Response.Client.Client)serializer.Deserialize(reader, typeof(Response.Client.Client));
                            break;
                        case "Invoice":
                            item.Invoice = (Invoice)serializer.Deserialize(reader, typeof(Invoice));
                            break;
                        case "InvoicePayment":
                            item.InvoicePayment = (Payment)serializer.Deserialize(reader, typeof(Payment));
                            break;
                        case "PostStamp":
                            item.PostStamp = (PostStamp)serializer.Deserialize(reader, typeof(PostStamp));
                            break;
                        case "InvoiceEmail":
                            item.InvoiceEmail = (Email)serializer.Deserialize(reader, typeof(Email));
                            break;
                        case "_InvoiceSettings":
                            item.InvoiceSettings = (ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject));
                            break;
                        default:
                            if (int.TryParse(path.LastOrDefault(), out int _))
                            {
                                item.Add((Stats)serializer.Deserialize(reader, typeof(Stats)));
                            }
                            break;
                    }
                }
            }
            while (reader.TokenType != JsonToken.None && !((reader.TokenType == JsonToken.EndObject || reader.TokenType == JsonToken.EndArray) && startPath == reader.Path));

            return item;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
