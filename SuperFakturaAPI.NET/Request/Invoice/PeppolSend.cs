using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request.Invoice
{
    // invoice.md "Send invoice via Peppol" (POST invoices/sendPeppol, JSON body).
    public class PeppolSend
    {
        [JsonProperty(PropertyName = "invoice_id")]
        public int InvoiceID { get; set; }

        // Public HTTPS URL without credentials or fragment; redirects are not followed.
        [JsonProperty(PropertyName = "webhook_url", NullValueHandling = NullValueHandling.Ignore)]
        public string WebhookUrl { get; set; }

        // 1-512 characters, sent together with WebhookUrl; used to sign the webhook request.
        [JsonProperty(PropertyName = "webhook_secret", NullValueHandling = NullValueHandling.Ignore)]
        public string WebhookSecret { get; set; }
    }
}
