using Newtonsoft.Json;
using System.Collections.Generic;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class Email
    {
        [JsonProperty(PropertyName = "invoice_id", NullValueHandling = NullValueHandling.Ignore)]
        public int InvoiceID { get; set; }
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
        [JsonProperty(PropertyName = "pdf_language", NullValueHandling = NullValueHandling.Ignore)]
        public string PDFLanguage { get; set; } = LanguageType.Slovak;
    }
}
