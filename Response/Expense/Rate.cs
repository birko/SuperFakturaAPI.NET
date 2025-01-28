using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Expense
{
    public class Rate
    {
        [JsonProperty(PropertyName = "base", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Base { get; set; }

        [JsonProperty(PropertyName = "vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal VAT { get; set; }
    }
}
