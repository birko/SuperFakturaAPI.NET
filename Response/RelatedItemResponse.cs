using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response
{
    public class RelatedItemResponse
    {
        [JsonProperty(PropertyName = "Expense", NullValueHandling = NullValueHandling.Ignore)]
        public Expense.Expense Expense { get; set; }


        [JsonProperty(PropertyName = "RelatedItem", NullValueHandling = NullValueHandling.Ignore)]
        public RelatedItem RelatedItem { get; set; }
    }
}
