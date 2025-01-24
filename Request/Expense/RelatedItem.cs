using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;

namespace Birko.SuperFaktura.Request.Expense
{
    public class RelatedItem
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; set; }

        [JsonProperty(PropertyName = "parent_id", NullValueHandling = NullValueHandling.Ignore)]
        public int ParentID { get; set; }

        [JsonProperty(PropertyName = "parent_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentType { get; set; }

        [JsonProperty(PropertyName = "child_id", NullValueHandling = NullValueHandling.Ignore)]
        public int ChildID { get; set; }

        [JsonProperty(PropertyName = "child_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ChildType { get; set; }
    }
}