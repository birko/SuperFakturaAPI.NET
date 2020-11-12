using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Response.Stock
{
    public class Item : Request.Stock.Item
    {
        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int UserID { get; internal set; }
        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int UserProfileID { get; internal set; }
        [JsonProperty(PropertyName = "import_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ImportID { get; internal set; }
        [JsonProperty(PropertyName = "import_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ImportType { get; internal set; }
        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; internal set; }
        [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Modified { get; internal set; }
    }

    public class Detail : Item
    {
        [JsonProperty(PropertyName = "StockItem", NullValueHandling = NullValueHandling.Ignore)]
        public Item StockItem { get; set; }
        [JsonProperty(PropertyName = "StockLog", NullValueHandling = NullValueHandling.Ignore)]
        public Log[] StockLog { get; set; }
    }
}
