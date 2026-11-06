using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.Invoice
{
    // invoice.md "Send invoice via Peppol" response data (HTTP 202 Accepted).
    public class PeppolSendResult
    {
        [JsonProperty(PropertyName = "invoice_id", NullValueHandling = NullValueHandling.Ignore)]
        public int InvoiceID { get; set; }

        // Matches later status responses and webhook notifications to this send operation.
        [JsonProperty(PropertyName = "operation_id", NullValueHandling = NullValueHandling.Ignore)]
        public string OperationID { get; set; }

        // Request.ValueLists.PeppolStatus
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
    }
}
