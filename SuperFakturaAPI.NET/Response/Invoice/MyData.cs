using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class MyData : Request.Invoice.MyData
    {

        [JsonProperty(PropertyName = "BankAccount", NullValueHandling = NullValueHandling.Ignore)]
        public InvoiceBankAccount[] BankAccount { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserProfileID { get; set; }

        [JsonProperty(PropertyName = "logo_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? LogoID { get; set; }

        [JsonProperty(PropertyName = "logo_key", NullValueHandling = NullValueHandling.Ignore)]
        public string LogoKey { get; set; }

        [JsonProperty(PropertyName = "web", NullValueHandling = NullValueHandling.Ignore)]
        public string Web { get; set; }

        [JsonProperty(PropertyName = "travel_agencies", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? TravelAgencies { get; set; }
    }
}
