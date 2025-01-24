using Newtonsoft.Json;
using System.Collections.Generic;

namespace Birko.SuperFaktura.Response.ValueLists
{
    public class Category
    {
        [JsonProperty(PropertyName = "children", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Category> Children { get; set; }

        [JsonProperty(PropertyName = "icon", NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        [JsonProperty(PropertyName = "icon_color", NullValueHandling = NullValueHandling.Ignore)]
        public string IconColor { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }
}
