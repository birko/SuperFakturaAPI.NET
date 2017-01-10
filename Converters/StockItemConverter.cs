using Birko.SuperFaktura.Response.Stock;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Birko.SuperFaktura.Converters
{
    public class StockItemConverter : JsonConverter
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
                        case "StockItem":
                            item.StockItem = (Response.Stock.Item)serializer.Deserialize(reader, typeof(Response.Stock.Item));
                            break;
                        default:
                            int index = 0;
                            if (int.TryParse(path.LastOrDefault(), out index))
                            {

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
