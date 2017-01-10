﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class Summary
    {
        [JsonProperty(PropertyName = "discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Discount { get; set; }
        [JsonProperty(PropertyName = "invoice_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal InvoiceTotal { get; set; }
        [JsonProperty(PropertyName = "vat_base_separate", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DictionaryConverter<decimal, decimal>))]
        public Dictionary<decimal, decimal> VATBaseSeparate { get; set; }
        [JsonProperty(PropertyName = "vat_base_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal VATBaseTotal { get; set; }
        [JsonProperty(PropertyName = "vat_separate", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DictionaryConverter<decimal, decimal>))]
        public Dictionary<decimal, decimal> VATSeparate { get; set; }
        [JsonProperty(PropertyName = "vat_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal VATTotal { get; set; }
    }
}
