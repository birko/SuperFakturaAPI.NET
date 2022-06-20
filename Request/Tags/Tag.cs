using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request.Tags
{
    public class Tag: Data
    {
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }
}
