using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Response.Invoice
{
    // invoice.md "Get Peppol sending status" response data; also the data of the Peppol status webhook.
    public class PeppolSendStatus : PeppolSendResult
    {
        [JsonProperty(PropertyName = "outbound_document_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? OutboundDocumentID { get; set; }

        [JsonProperty(PropertyName = "provider_document_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ProviderDocumentID { get; set; }

        // Request.ValueLists.PeppolProviderStatus; "accepted" is the final positive outcome.
        [JsonProperty(PropertyName = "provider_status", NullValueHandling = NullValueHandling.Ignore)]
        public string ProviderStatus { get; set; }

        [JsonProperty(PropertyName = "failure_code", NullValueHandling = NullValueHandling.Ignore)]
        public string FailureCode { get; set; }

        [JsonProperty(PropertyName = "created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CreatedAt { get; set; }

        // Completion of the SuperFaktura send operation; can precede the provider's final outcome.
        [JsonProperty(PropertyName = "completed_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CompletedAt { get; set; }
    }
}
