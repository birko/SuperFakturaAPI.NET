using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.Expense
{
    public class Category
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "parent_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ParentID { get; set; }

        [JsonProperty(PropertyName = "lft", NullValueHandling = NullValueHandling.Ignore)]
        public int? Left { get; set; }
        [JsonProperty(PropertyName = "rght", NullValueHandling = NullValueHandling.Ignore)]
        public int? Right { get; set; }
        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public string UserID { get; set; }
        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public string UserProfileID { get; set; }
    }
}
