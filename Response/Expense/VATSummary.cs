using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;

namespace Birko.SuperFaktura.Response.Expense
{
    public class VATSummary
    {
        // VAT rate in percent; taken from the VatSummary key, not from the item itself.
        [JsonIgnore]
        public decimal Rate { get; set; }

        [JsonProperty(PropertyName = "base", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Base { get; set; }

        [JsonProperty(PropertyName = "vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal VAT { get; set; }
    }
}