using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Dynamic;
using Birko.SuperFaktura.Entities;

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
            ClientItem clientItem = new ClientItem();
            string startPath = reader.Path;
            while (reader.TokenType != JsonToken.EndObject)
            {
                reader.Read();
                if (
                    (reader.TokenType == JsonToken.StartObject) && 
                    (reader.Path.StartsWith(startPath + ".") || string.IsNullOrEmpty(startPath))
                 )
                {
                    string[] path = reader.Path.Split(new[] { '.' });
                    string lastPath = path.LastOrDefault();
                    switch (lastPath)
                    {
                        case "Client":
                            item.Client = (Entities.Client)serializer.Deserialize(reader, typeof(Entities.Client));
                            clientItem.Client = item.Client;
                            break;
                        case "ClientData":
                            item.ClientData = (ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject));
                            clientItem.ClientData = item.ClientData;
                            break;
                        case "MyData":
                            item.MyData = (ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject));
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
                        case "UnitCount":
                            item.UnitCount = (ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject));
                            break;
                        case "PaymentLink":
                            item.PaymentLink = (string)serializer.Deserialize(reader, typeof(string));
                            break;
                        case "Paypal":
                            item.PayPal = (string)serializer.Deserialize(reader, typeof(string));
                            break;
                        case "Tag":
                            item.Tag = (ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject));
                            break;
                        case "Summary":
                            item.Summary = (ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject));
                            break;
                        case "SummaryInvoice":
                            item.SummaryInvoice = (ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject));
                            break;
                        default:
                            if (lastPath.StartsWith("PostStamp"))
                            {
                                if (item.PostStamp == null)
                                {
                                    item.PostStamp = new List<ExpandoObject>();
                                }
                                item.PostStamp.Add((ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject)));
                            }
                            else if (lastPath.StartsWith("InvoiceItem"))
                            {
                                if (item.InvoiceItems == null)
                                {
                                    item.InvoiceItems = new List<ExpandoObject>();
                                }
                                item.InvoiceItems.Add((ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject)));
                            }
                            else
                            {
                                int index = 0;
                                if (int.TryParse(path.LastOrDefault(), out index))
                                {
                                    item.Add((ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject)));
                                }
                            }
                            break;
                    }
                    reader.Read();
                }
            }

            return (objectType == typeof(InvoiceItem)) ? item: clientItem;
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
            return (objectType == typeof(bool) || objectType == typeof(bool?));
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

    public class LowerBooleanConverter : StringBooleanConverter
    { 
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
