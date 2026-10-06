using Birko.SuperFaktura.Response.BankAccounts;
using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class MyData : Request.Invoice.MyData
    {

        [JsonProperty(PropertyName = "BankAccount", NullValueHandling = NullValueHandling.Ignore)]
        public BankAccount[] BankAccount { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }
    }
}
