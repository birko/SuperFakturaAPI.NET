using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Request.Expense
{
    public class Extra
    {
        [JsonProperty(PropertyName = "vat_transfer", NullValueHandling = NullValueHandling.Ignore)]
        public int? VATTransfer { get; internal set; }
    }
}
