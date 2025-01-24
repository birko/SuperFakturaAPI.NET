using Birko.SuperFaktura.Response;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Linq;

namespace Birko.SuperFaktura.Converters
{
    public class ItemListConverter<T> : JsonConverter
    {
        public static int TimeoutSeconds { get; set; } = 180;
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType == typeof(ItemList<T>) || objectType.BaseType == typeof(ItemList<T>))
            {
                string startPath = reader.Path;
                ItemList<T> items = (ItemList<T>)Activator.CreateInstance(objectType);
                if (reader.TokenType == JsonToken.StartArray)
                {
                    items = (ItemList<T>)serializer.Deserialize(reader, objectType);
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
                                default:
                                    string inputData = path.LastOrDefault();
                                    var pathtest = int.TryParse(inputData, out int _);
                                    if (pathtest)
                                    {
                                        items.Add((T)serializer.Deserialize(reader, typeof(T)));
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
}
