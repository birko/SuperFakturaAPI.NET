using Birko.SuperFaktura.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura
{
    public class Data
    {
        [JsonProperty(PropertyName = "InvoiceItem", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Invoice.Item> InvoiceItem { get; internal set; } = new List<Invoice.Item>();
        [JsonProperty(PropertyName = "Tag", NullValueHandling = NullValueHandling.Ignore)]
        public IList<int> Tag { get; internal set; } = new List<int>();
        [JsonProperty(PropertyName = "Expense", NullValueHandling = NullValueHandling.Ignore)]
        public Expense Expense { get; set; }
        [JsonProperty(PropertyName = "Invoice", NullValueHandling = NullValueHandling.Ignore)]
        public Invoice Invoice { get; set; }
        [JsonProperty(PropertyName = "StockLog", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Stock.Movement> StockLog { get; internal set; } = new List<Stock.Movement>();
        [JsonProperty(PropertyName = "Client", NullValueHandling = NullValueHandling.Ignore)]
        public Entities.Client Client { get; internal set; }
    }
}
