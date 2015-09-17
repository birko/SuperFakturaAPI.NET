using Raven.Imports.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Invoice
    {
        public class MarkEmail
        {
            [JsonProperty(PropertyName = "invoice_id")]
            public int InvoiceId { get; set; }
            [JsonProperty(PropertyName = "email")]
            public string EmailAddres { get; set; }
            [JsonProperty(PropertyName = "subject")]
            public string Subject { get; set; } = string.Empty;
            [JsonProperty(PropertyName = "message")]
            public string Message { get; set; } = string.Empty;
        }

        public class Email
        {
            [JsonProperty(PropertyName = "invoice_id")]
            public int InvoiceId { get; set; }
            [JsonProperty(PropertyName = "to")]
            public string To { get; set; }
            [JsonProperty(PropertyName = "cc")]
            public IEnumerable<string> CC { get; set; }
            [JsonProperty(PropertyName = "bcc")]
            public IEnumerable<string> BCC { get; set; }
            [JsonProperty(PropertyName = "subject")]
            public string Subject { get; set; } = string.Empty;
            [JsonProperty(PropertyName = "body")]
            public string Body { get; set; } = string.Empty;
        }
    }
}
