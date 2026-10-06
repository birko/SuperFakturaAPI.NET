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
    public static class ModelCoverage
    {
        private static readonly DefaultContractResolver Resolver = new DefaultContractResolver();

        public class Result
        {
            public List<string> Unmapped { get; } = new List<string>();
            public List<string> NotReturned { get; } = new List<string>();

            public override string ToString()
            {
                return $"unmapped: [{string.Join(", ", Unmapped)}]\nnot returned: [{string.Join(", ", NotReturned)}]";
            }
        }

        public static Result Compare(JToken json, Type type)
        {
            var result = new Result();
            Compare(json, type, "", result);
            return result;
        }

        private static void Compare(JToken json, Type type, string path, Result result)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;
            var element = ElementType(type);
            if (json is JArray array)
            {
                if (element != null)
                {
                    foreach (var item in array.Take(3))
                    {
                        Compare(item, element, path + "[]", result);
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
