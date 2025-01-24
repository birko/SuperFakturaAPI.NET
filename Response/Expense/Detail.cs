using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;

namespace Birko.SuperFaktura.Response.Expense
{
    [JsonConverter(typeof(ExpenseItemConverter))]
    public class Detail//: List<Stats>
    {
        [JsonProperty(PropertyName = "Client", NullValueHandling = NullValueHandling.Ignore)]
        public Client.Client Client { get; set; } = null;

        [JsonProperty(PropertyName = "Document", NullValueHandling = NullValueHandling.Ignore)]
        public Document Document { get; set; } = null;

        [JsonProperty(PropertyName = "Expense", NullValueHandling = NullValueHandling.Ignore)]
        public Expense Expense { get; set; } = null;

        [JsonProperty(PropertyName = "ExpenseBasicRate", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<ExpenseBasicRate> ExpenseBasicRate { get; set; } = null;

        [JsonProperty(PropertyName = "ExpenseCategory", NullValueHandling = NullValueHandling.Ignore)]
        public ValueLists.Category ExpenseCategory { get; set; } = null;

        [JsonProperty(PropertyName = "ExpenseExtra", NullValueHandling = NullValueHandling.Ignore)]
        public Extra ExpenseExtra { get; set; } = null;

        [JsonProperty(PropertyName = "ExpenseItem", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<ExpenseItem> ExpenseItem { get; set; } = null;

        [JsonProperty(PropertyName = "ExpensePayment", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Payment> ExpensePayment { get; set; } = null;

        [JsonProperty(PropertyName = "MyData", NullValueHandling = NullValueHandling.Ignore)]
        public MyData MyData { get; set; } = null;

        [JsonProperty(PropertyName = "RelatedItem", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<RelatedItem> RelatedItem { get; set; } = null;

        [JsonProperty(PropertyName = "Tag", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<int> Tag { get; set; }

        [JsonProperty(PropertyName = "VatSummary", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<VATSummary> VATSummary { get; set; } = null;

        [JsonProperty(PropertyName = "attachments", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<ExpandoObject> Attachments { get; set; } = null;
    }
}
