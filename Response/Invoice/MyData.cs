using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class MyData: Request.Invoice.MyData
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; internal  set; }
        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int UserID { get; set; }
        [JsonProperty(PropertyName = "tax_payer", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool TaxPayer { get; set; }
        [JsonProperty(PropertyName = "country", NullValueHandling = NullValueHandling.Ignore)]
        public Country Country { get; set; }
        [JsonProperty(PropertyName = "BankAccount", NullValueHandling = NullValueHandling.Ignore)]
        public BankAccount[] BankAccount { get; set; }
    }
}
