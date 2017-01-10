using Birko.SuperFaktura.Response.Expense;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

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
                            item.Client = (Response.Client.Client)serializer.Deserialize(reader, typeof(Response.Client.Client));
                            break;
                        case "Document":
                            item.Document = (Document)serializer.Deserialize(reader, typeof(Document));
                            break;
                        case "Expense":
                            item.Expense = (Expense)serializer.Deserialize(reader, typeof(Expense));
                            break;
                        case "ExpenseCategory":
                            item.ExpenseCategory = (Category)serializer.Deserialize(reader, typeof(Category));
                            break;
                        default:
                            int index = 0;
                            if (int.TryParse(path.LastOrDefault(), out index))
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
