using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;

namespace Birko.SuperFaktura.Response
{
    public class RelatedItem
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "relation_id", NullValueHandling = NullValueHandling.Ignore)]
        public int RelationID { get; set; }

        [JsonProperty(PropertyName = "hard_link", NullValueHandling = NullValueHandling.Ignore)]
        public int HardLink { get; set; }

        [JsonProperty(PropertyName = "Invoice", NullValueHandling = NullValueHandling.Ignore)]
        public Request.Invoice.InvoiceInfo Invoice { get; set; }

        [JsonProperty(PropertyName = "Expense", NullValueHandling = NullValueHandling.Ignore)]
        public Expense.Expense Expense { get; set; }
    }
}