using Newtonsoft.Json;
using System.Collections.Generic;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class InvoiceSettings
    {
        [JsonProperty(PropertyName = "settings", NullValueHandling = NullValueHandling.Ignore)]
        public string Settings { get; set; }
    }
    public class InvoiceData : Data
    {
        public Invoice Invoice { get; set;}
        public IEnumerable<Item> InvoiceItem { get; set; }
        public IEnumerable<Item> ExpenseItem { get; set; }
        public IEnumerable<int> Tag { get; set; }
        public Client.Client Client { get; set; }
        public InvoiceSettings InvoiceSetting { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Extra InvoiceExtra { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MyData MyData { get; set; }
    }
}
