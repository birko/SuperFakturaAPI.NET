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
                            item.Client = (Entities.Client)serializer.Deserialize(reader, typeof(Entities.Client));
                            clientItem.Client = item.Client;
                            break;
                        case "Invoice":
                            item.Invoice = (Entities.Invoice)serializer.Deserialize(reader, typeof(Entities.Invoice));
                            break;
                        case "InvoicePayment":
                            item.InvoicePayment = (ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject));
                            break;
                        case "PostStamp":
                            item.PostStamp = (ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject));
                            break;
                        case "InvoiceEmail":
                            item.InvoiceEmail = (ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject));
                            break;
                        case "_InvoiceSettings":
                            item.InvoiceSettings = (ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject));
                            break;
                        default:
                            int index = 0;
                            if (int.TryParse(path.LastOrDefault(), out index))
                            {
                                item.Add((ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject)));
                            }
                            break;
                    }
                }
            }
            while (reader.TokenType != JsonToken.None && !((reader.TokenType == JsonToken.EndObject || reader.TokenType == JsonToken.EndArray) && startPath == reader.Path));

            return (objectType == typeof(InvoiceItem)) ? item : clientItem;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
    public class ResponseItemListConverter : JsonConverter
    {
        public static int TimeoutSeconds { get; set; } = 180;
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
                if (reader.TokenType == JsonToken.StartArray)
                {
                    items = (ItemList<InvoiceItem>)serializer.Deserialize(reader, typeof(ItemList<InvoiceItem>));
                }
                else if (reader.TokenType == JsonToken.StartObject)
                {
                    var startTime = DateTime.UtcNow;
                    do
                    {
                        reader.Read(); // read next json token
                        if (reader.TokenType == JsonToken.PropertyName)
                        {
                            reader.Read();// read property value
                            string[] path = reader.Path.Split(new[] { '.' });
                            string lastPath = path.LastOrDefault();
                            switch (lastPath)
                            {
                                case "_InvoiceSettings":
                                    items.InvoiceSettings = (ExpandoObject)serializer.Deserialize(reader, typeof(ExpandoObject));
                                    break;
                                default:

                                    string inputData = path.LastOrDefault();
                                    int index = 0;
                                    var pathtest = int.TryParse(inputData, out index);
                                    if (pathtest)
                                    {
                                        items.Add((InvoiceItem)serializer.Deserialize(reader, typeof(InvoiceItem)));
                                    }
                                    break;
                            }
                        }

                        if (TimeoutSeconds != int.MaxValue)
                        {
                            var timeDiff = DateTime.UtcNow - startTime;
                            if (timeDiff.Seconds > TimeoutSeconds)
                            {
                                throw new TimeoutException("SuperFaktura invoice list parsing takes to long. Check the source data or adjust timeout interval");
                            }
                        }
                    }
                    while (reader.TokenType != JsonToken.None && !((reader.TokenType == JsonToken.EndObject || reader.TokenType == JsonToken.EndArray) && startPath == reader.Path));
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