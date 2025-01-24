using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;

namespace Birko.SuperFaktura.Response.Expense
{
    public class VATSummary
    {
        [JsonProperty(PropertyName = "base", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Base { get; set; }

        [JsonProperty(PropertyName = "vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal VAT { get; set; }
    }
}