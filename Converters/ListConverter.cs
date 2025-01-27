using Birko.SuperFaktura.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Birko.SuperFaktura.Converters
{
    public class ListConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType.GetInterfaces().Contains(typeof(IEnumerable<T>)))
            {
                string startPath = reader.Path;
                var items = Activator.CreateInstance(objectType);
                if (reader.TokenType == JsonToken.StartArray)
                {
                    foreach (var i in (IEnumerable<T>)serializer.Deserialize(reader, typeof(IEnumerable<T>)))
                    {
                        (items as IList<T>).Add(i);
                    }
                }
                else if (reader.TokenType == JsonToken.StartObject)
                {
                    do
                    {
                        reader.Read(); // read next json token
                        if (reader.TokenType == JsonToken.PropertyName)
                        {
                            reader.Read();// read property value
                            string[] path = reader.Path.Split(new[] { '.' });
                            string lastPath = path.LastOrDefault();
                            if (int.TryParse(lastPath, out int _))
                            {
                                (items as IList<T>).Add((T)serializer.Deserialize(reader, typeof(T)));
                            }
                            else
                            {
                                var prop = objectType.GetProperties()
                                    .FirstOrDefault(p => Attribute.IsDefined(p, typeof(JsonPropertyAttribute)) && p.GetCustomAttributes(true).Any(x => (x as JsonPropertyAttribute)?.PropertyName == lastPath));
                                if (prop == null)
                                {
                                    continue;
                                }
                                prop.SetValue(items, serializer.Deserialize(reader, prop.PropertyType));
                            }
                        }
                    }
                    while (startPath != reader.Path);
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
