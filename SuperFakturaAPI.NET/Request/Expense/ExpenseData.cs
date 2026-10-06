using Newtonsoft.Json;
using System.Collections.Generic;

namespace Birko.SuperFaktura.Request.Expense
{
    public class ExpenseData : TagData
    {
        public Expense Expense { get; set; }

        public Extra ExpenseExtra { get; set; }

        public Client.Client Client { get; set; }

        [JsonProperty(PropertyName = "ExpenseItem", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<ExpenseItem> ExpenseItems { get; set; }
    }
}
