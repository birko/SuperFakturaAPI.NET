﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class SummaryInvoice
    {
        [JsonProperty(PropertyName = "vat_base_separate_negative", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DictionaryConverter<string, decimal?>))]
        public IDictionary<string, decimal?>  VATBaseSeparateNegative { get; set; }

        [JsonProperty(PropertyName = "vat_base_separate_positive", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DictionaryConverter<string, decimal?>))]

        public IDictionary<string, decimal?> VATBaseSeparatePositive { get; set; }

        [JsonProperty(PropertyName = "vat_separate_negative", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DictionaryConverter<string, decimal?>))]
        public IDictionary<string, decimal?> VATSeparateNegative { get; set; }

        [JsonProperty(PropertyName = "vat_separate_positive", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DictionaryConverter<string, decimal?>))]
        public IDictionary<string, decimal?> VATSeparatePositive { get; set; }
    }
}
