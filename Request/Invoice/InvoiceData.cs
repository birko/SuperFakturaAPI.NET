using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class InvoiceData : TagData
    {
        public Invoice Invoice { get; set;}

        public IEnumerable<Item> InvoiceItem { get; set; }

        public IEnumerable<Item> ExpenseItem { get; set; }

        public Client.Client Client { get; set; }

        [JsonConverter(typeof(ZeroObjectConverter))]
        public InvoiceSettings InvoiceSetting { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Extra InvoiceExtra { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MyData MyData { get; set; }
    }
}
