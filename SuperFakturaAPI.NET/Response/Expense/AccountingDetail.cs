using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.Expense
{
    public class AccountingDetail : Request.Expense.AccountingDetail
    {
        [JsonProperty(PropertyName = "document_id", NullValueHandling = NullValueHandling.Ignore)]
        public int DocumentID { get; set; }

        [JsonProperty(PropertyName = "document_item_id", NullValueHandling = NullValueHandling.Ignore)]
        public int DocumentItemID { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "item_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ItemType { get; set; }

        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int UserProfileID { get; set; }
    }
}
