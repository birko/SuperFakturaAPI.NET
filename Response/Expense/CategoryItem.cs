using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
