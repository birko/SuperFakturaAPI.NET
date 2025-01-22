using Birko.SuperFaktura.Response.Expense;
using Newtonsoft.Json;
using System;
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
