using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.Invoice
{
    // Invoice settings as returned with the invoice detail (adds read-only fields to the request settings).
    public class InvoiceSettingResponse : Request.Invoice.InvoiceSettings
    {
        [JsonProperty(PropertyName = "force_iban", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? ForceIBAN { get; set; }
    }
}
