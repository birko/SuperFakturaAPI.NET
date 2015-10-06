using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Invoice
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

        public class Email
        {
            [JsonProperty(PropertyName = "invoice_id", NullValueHandling = NullValueHandling.Ignore)]
            public int InvoiceId { get; set; }
            [JsonProperty(PropertyName = "to", NullValueHandling = NullValueHandling.Ignore)]
            public string To { get; set; }
            [JsonProperty(PropertyName = "cc", NullValueHandling = NullValueHandling.Ignore)]
            public IEnumerable<string> CC { get; set; }
            [JsonProperty(PropertyName = "bcc", NullValueHandling = NullValueHandling.Ignore)]
            public IEnumerable<string> BCC { get; set; }
            [JsonProperty(PropertyName = "subject", NullValueHandling = NullValueHandling.Ignore)]
            public string Subject { get; set; } = string.Empty;
            [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
            public string Body { get; set; } = string.Empty;
        }
    }
}
