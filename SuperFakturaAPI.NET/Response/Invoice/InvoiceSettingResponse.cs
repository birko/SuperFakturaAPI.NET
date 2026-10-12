using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.Invoice
{
    // Invoice settings as returned with the invoice detail (adds read-only fields to the request settings).
    public class InvoiceSettingResponse : Request.Invoice.InvoiceSettings
    {
        [JsonProperty(PropertyName = "force_iban", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? ForceIBAN { get; set; }

        [JsonProperty(PropertyName = "show_summary", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? ShowSummary { get; set; }

        [JsonProperty(PropertyName = "force_paid", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? ForcePaid { get; set; }

        [JsonProperty(PropertyName = "add_proforma_item", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? AddProformaItem { get; set; }

        [JsonProperty(PropertyName = "payme", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? PayMe { get; set; }
    }
}
