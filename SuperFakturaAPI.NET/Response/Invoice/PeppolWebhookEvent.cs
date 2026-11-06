using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Response.Invoice
{
    // invoice.md "Peppol status webhook" body; read it with PeppolWebhook.Parse after PeppolWebhook.VerifySignature.
    public class PeppolWebhookEvent
    {
        // Same across retries of the event - deduplicate by it.
        [JsonProperty(PropertyName = "event_id", NullValueHandling = NullValueHandling.Ignore)]
        public string EventID { get; set; }

        // PeppolWebhook.StatusChangedEventType
        [JsonProperty(PropertyName = "event_type", NullValueHandling = NullValueHandling.Ignore)]
        public string EventType { get; set; }

        [JsonProperty(PropertyName = "occurred_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? OccurredAt { get; set; }

        // operation_id, invoice_id, status, provider_document_id, provider_status, failure_code.
        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public PeppolSendStatus Data { get; set; }
    }
}
