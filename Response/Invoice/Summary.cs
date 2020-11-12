using Newtonsoft.Json;
using System.Collections.Generic;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class Summary
    {
        [JsonProperty(PropertyName = "discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Discount { get; set; }
        [JsonProperty(PropertyName = "invoice_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal InvoiceTotal { get; set; }
        [JsonProperty(PropertyName = "vat_base_separate", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DictionaryConverter<string, decimal?>))]
        public Dictionary<string, decimal?> VATBaseSeparate { get; set; }
        [JsonProperty(PropertyName = "vat_base_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal VATBaseTotal { get; set; }
        [JsonProperty(PropertyName = "vat_separate", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DictionaryConverter<string, decimal?>))]
        public Dictionary<string, decimal?> VATSeparate { get; set; }
        [JsonProperty(PropertyName = "vat_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal VATTotal { get; set; }
    }
}
