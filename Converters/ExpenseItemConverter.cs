using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.Expense;
using Birko.SuperFaktura.Response.ValueLists;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Birko.SuperFaktura.Converters
{
    public class ExpenseItemConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Detail item = new Detail();
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
                            item.Client = (reader.TokenType != JsonToken.StartArray)
                                    ? (Response.Client.Client)serializer.Deserialize(reader, typeof(Response.Client.Client))
                                    : null;
                            break;
                        case "Document":
                            item.Document = (reader.TokenType != JsonToken.StartArray)
                                ? (Document)serializer.Deserialize(reader, typeof(Document))
                                : null;
                            break;
                        case "Expense":
                            item.Expense = (reader.TokenType != JsonToken.StartArray)
                                ? (Expense)serializer.Deserialize(reader, typeof(Expense))
                                : null;
                            break;
                        case "ExpenseCategory":
                            item.ExpenseCategory = (reader.TokenType != JsonToken.StartArray)
                                ? (Category)serializer.Deserialize(reader, typeof(Category))
                                : null;
                            break;
                        case "ExpenseBasicRate":
                            item.ExpenseBasicRate = (IEnumerable<ExpenseBasicRate>)serializer.Deserialize(reader, typeof(IEnumerable<ExpenseBasicRate>));
                            break;
                        case "ExpenseExtra":
                            item.ExpenseExtra = (reader.TokenType != JsonToken.StartArray)
                                ? (Extra)serializer.Deserialize(reader, typeof(Extra))
                                : null;
                            break;
                        case "ExpenseItem":
                            item.ExpenseItem = (IEnumerable<ExpenseItem>)serializer.Deserialize(reader, typeof(IEnumerable<ExpenseItem>));
                            break;
                        case "ExpensePayment":
                            item.ExpensePayment = (IEnumerable<Payment>)serializer.Deserialize(reader, typeof(IEnumerable<Payment>));
                            break;
                        case "MyData":
                            item.MyData = (reader.TokenType != JsonToken.StartArray)
                                ? (MyData)serializer.Deserialize(reader, typeof(MyData))
                                : null;
                            break;
                        case "RelatedItem":
                            item.RelatedItem = (IEnumerable<RelatedItem>)serializer.Deserialize(reader, typeof(IEnumerable<RelatedItem>));
                            break;
                        case "Tag":
                            item.Tag = (IEnumerable<int>)serializer.Deserialize(reader, typeof(IEnumerable<int>));
                            break;
                        case "VatSummary":
                            item.VATSummary = (IEnumerable<VATSummary>)serializer.Deserialize(reader, typeof(IEnumerable<VATSummary>));
                            break;
                        case "attachments":
                            item.Attachments = (IEnumerable<ExpandoObject>)serializer.Deserialize(reader, typeof(IEnumerable<ExpandoObject>));
                            break;
                        default:
                            if (int.TryParse(path.LastOrDefault(), out int _))
                            {
                                throw new ArgumentException($"Path '{lastPath}'  id notdefined");
                                //item.Add((Stats)serializer.Deserialize(reader, typeof(Stats)));
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
