using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.CashRegister
{
    public class SummaryValue {

        [JsonProperty(PropertyName = "formatted", NullValueHandling = NullValueHandling.Ignore)]
        public string Formatted { get; set; }

        [JsonProperty(PropertyName = "value", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Value { get;  set; }
    }

    public class Summary
    {
        [JsonProperty(PropertyName = "minus", NullValueHandling = NullValueHandling.Ignore)]
        public SummaryValue Minus { get; set; }

        [JsonProperty(PropertyName = "plus", NullValueHandling = NullValueHandling.Ignore)]
        public SummaryValue Plus { get; set; }

        [JsonProperty(PropertyName = "total", NullValueHandling = NullValueHandling.Ignore)]
        public SummaryValue Total { get; set; }
    }
}
