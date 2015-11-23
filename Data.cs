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
        public IList<Invoice.Item> InvoiceItem { get; internal set; } = null;
        [JsonProperty(PropertyName = "Tag", NullValueHandling = NullValueHandling.Ignore)]
        public IList<int> Tag { get; internal set; } = null;
        [JsonProperty(PropertyName = "Expense", NullValueHandling = NullValueHandling.Ignore)]
        public Expense Expense { get; set; }
        [JsonProperty(PropertyName = "Invoice", NullValueHandling = NullValueHandling.Ignore)]
        public Invoice Invoice { get; set; }
        [JsonProperty(PropertyName = "StockLog", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Stock.Movement> StockLog { get; internal set; } = null;
        [JsonProperty(PropertyName = "Client", NullValueHandling = NullValueHandling.Ignore)]
        public Entities.Client Client { get; internal set; }
    }

    public class PostData
    {
        [JsonProperty(PropertyName = "Post", NullValueHandling = NullValueHandling.Ignore)]
        public Invoice.Post Post { get; internal set; } = null;
    }

    public class EmailData
    {
        [JsonProperty(PropertyName = "Email", NullValueHandling = NullValueHandling.Ignore)]
        public Invoice.Email Email { get; internal set; } = null;
    }
}
