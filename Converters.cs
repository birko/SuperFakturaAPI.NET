using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Dynamic;

namespace Birko.SuperFaktura
{

    public class ResponseListItemConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            InvoiceItem item = new InvoiceItem();
            string startPath = reader.Path;
            while (reader.TokenType != JsonToken.EndObject)
            {
                reader.Read();
                if (reader.TokenType == JsonToken.StartObject && reader.Path.StartsWith(startPath + "."))
                {
                    string[] path = reader.Path.Split(new[] { '.' });
                    switch (path.LastOrDefault())
                    {
                        case "Client":
                            item.Client = (Entities.Client)serializer.Deserialize(reader, typeof(Entities.Client));
                            break;
                        case "Invoice":
                            item.Invoice = (Entities.Invoice)serializer.Deserialize(reader, typeof(Entities.Invoice));
                            break;
                        case "InvoicePayment":
                            item.InvoicePayment = (ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject));
                            break;
                        case "InvoiceEmail":
                            item.InvoiceEmail = (ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject));
                            break;
                        case "_InvoiceSettings":
                            item.InvoiceSettings = (ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject));
                            break;
                        case "PostStamp":
                            item.PostStamp = (ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject));
                            break;
                        default:
                            item.Add((ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject)));
                            break;
                    }
                    reader.Read();
                }
            }

            return item;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
    public class ResponseItemListConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType == typeof(ItemList<InvoiceItem>))
            {
                string startPath = reader.Path;
                ItemList<InvoiceItem> items = new ItemList<InvoiceItem>();
                while (reader.TokenType != JsonToken.EndObject)
                {
                    reader.Read();
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        if (reader.Path == startPath + "._InvoiceSettings")
                        {
                            items.InvoiceSettings = (ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject));
                            reader.Read();
                        }
                        else if (reader.Path.StartsWith(startPath + "."))
                        {
                            items.Add((InvoiceItem)serializer.Deserialize(reader, typeof(InvoiceItem)));
                            reader.Read();
                        }
                    }
                }
                return items;
            }

            return serializer.Deserialize(reader, objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
    public class StringBooleanConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(bool));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string value = reader.Value.ToString();
            if (!string.IsNullOrEmpty(value) && !value.Trim().Equals("0"))
            {
                return true;
            }
            return false;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            bool convertedValue = (bool)value;
            writer.WriteValue(convertedValue ? "1" : "0");
        }
    }

    public class LowerBooleanConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(bool));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string value = reader.Value.ToString();
            if (!string.IsNullOrEmpty(value) && !value.Trim().Equals("true"))
            {
                return true;
            }
            return false;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            bool convertedValue = (bool)value;
            writer.WriteValue(convertedValue ? "true" : "false");
        }
    }
}
