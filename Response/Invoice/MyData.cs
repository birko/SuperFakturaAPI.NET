using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class MyData: Request.Invoice.MyData
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; internal  set; }

        [JsonProperty(PropertyName = "country", NullValueHandling = NullValueHandling.Ignore)]
        public Country Country { get; set; }

        [JsonProperty(PropertyName = "BankAccount", NullValueHandling = NullValueHandling.Ignore)]
        public BankAccounts.BankAccount[] BankAccount { get; set; }
    }
}
