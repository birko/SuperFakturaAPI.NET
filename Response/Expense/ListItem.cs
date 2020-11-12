using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Birko.SuperFaktura.Response.Expense
{
    [JsonConverter(typeof(ExpenseItemConverter))]
    public class ListItem: List<Stats>
    {
        [JsonProperty(PropertyName = "Client", NullValueHandling = NullValueHandling.Ignore)]
        public Client.Client Client { get; set; } = null;

        [JsonProperty(PropertyName = "Document", NullValueHandling = NullValueHandling.Ignore)]
        public Document Document { get; set; } = null;
        [JsonProperty(PropertyName = "Expense", NullValueHandling = NullValueHandling.Ignore)]
        public Expense Expense { get; set; } = null;
        [JsonProperty(PropertyName = "ExpenseCategory ", NullValueHandling = NullValueHandling.Ignore)]
        public Category ExpenseCategory { get; set; } = null;
        [JsonProperty(PropertyName = "ExpensePaymenty ", NullValueHandling = NullValueHandling.Ignore)]
        public Payment ExpensePayment { get; set; } = null;
    }
}
