using Birko.SuperFaktura.Entities;
using Raven.Imports.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura
{
    public class Data
    {
        [JsonProperty(PropertyName = "InvoiceItem")]
        public IList<Invoice.Item> InvoiceItem { get; internal set; } = new List<Invoice.Item>();
        [JsonProperty(PropertyName = "Tag")]
        public IList<int> Tag { get; internal set; } = new List<int>();
        [JsonProperty(PropertyName = "Expense")]
        public Expense Expense { get; set; }
        [JsonProperty(PropertyName = "Invoice")]
        public Invoice Invoice { get; set; }
        [JsonProperty(PropertyName = "StockLog")]
        public IList<Stock.Movement> StockLog { get; internal set; } = new List<Stock.Movement>();
        [JsonProperty(PropertyName = "Client")]
        public Entities.Client Client { get; internal set; }
    }
}
