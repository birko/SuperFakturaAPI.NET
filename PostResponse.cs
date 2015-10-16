using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Birko.SuperFaktura
{
    public class PostResponse<T>
    {
        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        public int? Error { get; set; } = null;
        [JsonProperty(PropertyName = "error_message", NullValueHandling = NullValueHandling.Ignore)]
        public object ErrorMessage { get; set; } = null;
        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; } = default(T);
    }
}
