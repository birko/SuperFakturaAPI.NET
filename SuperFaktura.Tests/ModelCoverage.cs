using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperFaktura.Tests
{
    // Compares a real API JSON response with a model's [JsonProperty] names (recursively):
    // - Unmapped: keys the API returns but the model ignores (missing or misnamed properties)
    // - NotReturned: model properties the API did not return (possibly misnamed or request-only)
    // - Mismatched: values that don't fit the property type - deserializing throws, or the property's
    //   converter turns a non-empty value into null (e.g. "due":"-7" read into a DateTime)
    public static class ModelCoverage
    {
        private static readonly DefaultContractResolver Resolver = new DefaultContractResolver();

        public class Result
        {
            public List<string> Unmapped { get; } = new List<string>();
            public List<string> NotReturned { get; } = new List<string>();
            public List<string> Mismatched { get; } = new List<string>();

            public override string ToString()
            {
                return $"unmapped: [{string.Join(", ", Unmapped)}]\nnot returned: [{string.Join(", ", NotReturned)}]\nmismatched: [{string.Join(", ", Mismatched)}]";
            }
        }

        public static Result Compare(JToken json, Type type)
        {
            var result = new Result();
            Compare(json, type, "", result);
            // every array item is checked, so the same path can be reported many times
            Distinct(result.Unmapped);
            Distinct(result.NotReturned);
            Distinct(result.Mismatched);
            return result;
        }

        private static void Distinct(List<string> list)
        {
            var distinct = list.Distinct().ToList();
            list.Clear();
            list.AddRange(distinct);
        }

        private static void Compare(JToken json, Type type, string path, Result result)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;
            var element = ElementType(type);
            if (json is JArray array)
            {
                if (element != null)
                {
                    foreach (var item in array)
                    {
                        Compare(item, element, path.TrimEnd('.') + "[].", result);
                    }
                }
                return;
            }
            if (!(json is JObject obj) || element != null || type == typeof(string) || type.IsPrimitive || type == typeof(object))
            {
                return;
            }
            if (!(Resolver.ResolveContract(type) is JsonObjectContract contract))
            {
                return;
            }

            foreach (var property in obj.Properties())
            {
                var match = contract.Properties.GetClosestMatchProperty(property.Name);
                if (match == null || match.Ignored)
                {
                    result.Unmapped.Add(path + property.Name);
                }
                else
                {
                    CheckType(property.Value, match, path + property.Name, result);
                    Compare(property.Value, match.PropertyType, path + property.Name + ".", result);
                }
            }
            foreach (var property in contract.Properties.Where(p => !p.Ignored && p.Readable))
            {
                if (obj.Properties().All(p => !string.Equals(p.Name, property.PropertyName, StringComparison.OrdinalIgnoreCase)))
                {
                    result.NotReturned.Add(path + property.PropertyName);
                }
            }
        }

        // Objects and arrays are checked value by value through the recursion; only values, simple
        // types and properties with their own converter are deserialized here.
        private static void CheckType(JToken value, JsonProperty property, string path, Result result)
        {
            var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
            if (type == typeof(object) || value.Type == JTokenType.Null || !(value is JValue || IsSimple(type) || property.Converter != null))
            {
                return;
            }
            // without a converter Newtonsoft reads "" as null, which NullValueHandling.Ignore then skips
            if (property.Converter == null && property.NullValueHandling == NullValueHandling.Ignore && value.Type == JTokenType.String && (string)value == "")
            {
                return;
            }
            object read;
            try
            {
                var serializer = new JsonSerializer();
                if (property.Converter != null)
                {
                    var reader = new JTokenReader(value);
                    reader.Read();
                    read = property.Converter.ReadJson(reader, property.PropertyType, null, serializer);
                }
                else
                {
                    read = value.ToObject(property.PropertyType, serializer);
                }
            }
            catch (Exception ex)
            {
                result.Mismatched.Add($"{path} = {value.ToString(Formatting.None)} -> {Name(property.PropertyType)}: {ex.Message.Split('\n')[0]}");
                return;
            }
            // "" and MySQL's zero date "0000-00-00 00:00:00" mean no value
            if (read == null && value is JValue && !(value.Type == JTokenType.String && (string.IsNullOrEmpty((string)value) || ((string)value).StartsWith("0000-00-00"))))
            {
                result.Mismatched.Add($"{path} = {value.ToString(Formatting.None)} -> {Name(property.PropertyType)}: read as null");
            }
        }

        private static bool IsSimple(Type type)
        {
            return type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal) || type == typeof(DateTime);
        }

        private static string Name(Type type)
        {
            var underlying = Nullable.GetUnderlyingType(type);
            return underlying != null ? underlying.Name + "?" : type.Name;
        }

        private static Type ElementType(Type type)
        {
            if (type == typeof(string) || type.IsArray == false && !typeof(System.Collections.IEnumerable).IsAssignableFrom(type))
            {
                return null;
            }
            if (type.IsArray)
            {
                return type.GetElementType();
            }
            var dictionary = type.GetInterfaces().Concat(new[] { type })
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>));
            if (dictionary != null)
            {
                return null;
            }
            var enumerable = type.GetInterfaces().Concat(new[] { type })
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            return enumerable?.GetGenericArguments()[0];
        }
    }
}
