using Birko.SuperFaktura.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Birko.SuperFaktura.Converters
{
    public class ListConverter: JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType.GetInterfaces().Contains(typeof(IEnumerable)))
            {
                string startPath = reader.Path;
                Type type = objectType.GenericTypeArguments?.FirstOrDefault();

                if (reader.TokenType == JsonToken.StartArray)
                {
                    return (IEnumerable<object>)serializer.Deserialize(reader, objectType);
                }
                else if (reader.TokenType == JsonToken.StartObject)
                {
                    var items = (objectType.GetInterfaces().Contains(typeof(IList)))
                        ? Activator.CreateInstance(objectType)
                        : Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
                    do
                    {
                        reader.Read(); // read next json token
                        if (reader.TokenType == JsonToken.PropertyName)
                        {
                            string[] path = reader.Path.Split(new[] { '.' });
                            string lastPath = path.LastOrDefault();
                            reader.Read();// read property value
                            if (int.TryParse(lastPath, out int _))
                            {
                                (items as IList).Add(serializer.Deserialize(reader, type));
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
                    return items;
                }
            }
            return serializer.Deserialize(reader, objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
