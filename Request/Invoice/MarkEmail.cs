using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class MarkEmail
    {
        [JsonProperty(PropertyName = "invoice_id", NullValueHandling = NullValueHandling.Ignore)]
        public int InvoiceId { get; set; }
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string EmailAddres { get; set; }
        [JsonProperty(PropertyName = "subject", NullValueHandling = NullValueHandling.Ignore)]
        public string Subject { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; } = string.Empty;
    }
}
