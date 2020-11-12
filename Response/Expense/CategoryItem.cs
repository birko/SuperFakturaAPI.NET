using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.Expense
{
    public class CategoryItem
    {
        [JsonProperty(PropertyName = "ExpenseCategory", NullValueHandling = NullValueHandling.Ignore)]
        public Category ExpenseCategory { get; set; }
        [JsonProperty(PropertyName = "children", NullValueHandling = NullValueHandling.Ignore)]
        public Category[] Children { get; set; }
    }
}
