using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class SummaryInvoice
    {

        [JsonProperty(PropertyName = "vat_base_separate_negative", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DictionaryConverter<decimal, decimal>))]
        public Dictionary<decimal, decimal>  VATBaseSeparateNegative { get; set; }
        [JsonProperty(PropertyName = "vat_base_separate_positive", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DictionaryConverter<decimal, decimal>))]
        public Dictionary<decimal, decimal> VATBaseSeparatePositive { get; set; }
        [JsonProperty(PropertyName = "vat_separate_negative", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DictionaryConverter<decimal, decimal>))]
        public Dictionary<decimal, decimal> VATSeparateNegative { get; set; }
        [JsonProperty(PropertyName = "vat_separate_positive", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DictionaryConverter<decimal, decimal>))]
        public Dictionary<decimal, decimal> VATSeparatePositive { get; set; }

    }
}
