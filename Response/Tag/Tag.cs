using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Response.Tag
{
    public class Tag : ErrorMessageResponse
    {
        [JsonProperty(PropertyName = "tag_id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; internal set; }

        [JsonProperty(PropertyName = "tag_name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; internal set; }
    }
}
