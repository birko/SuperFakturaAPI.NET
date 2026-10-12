using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Response.Invoice
{
    // Invoice extra data as returned with the invoice detail (adds read-only fields to the request extra).
    public class InvoiceExtraResponse : Request.Invoice.Extra
    {
        // Not documented; set on recurring invoices, e.g. "2026-10-10 09:25"
        [JsonProperty(PropertyName = "recurring_start_email_process", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DateTimeConverter))]
        public DateTime? RecurringStartEmailProcess { get; set; }

        [JsonProperty(PropertyName = "recurring_start_post_process", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DateTimeConverter))]
        public DateTime? RecurringStartPostProcess { get; set; }
    }
}
