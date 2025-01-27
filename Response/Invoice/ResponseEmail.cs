using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class ResponseEmail
    {
        [JsonProperty(PropertyName = "Invoice", NullValueHandling = NullValueHandling.Ignore)]
        public ResponseEmailInvoice Invoice { get; set; }
    }

    public class ResponseEmailInvoice
    {
        [Obsolete("Not found in API documentation")]
        [JsonProperty(PropertyName = "bcc", NullValueHandling = NullValueHandling.Ignore)]
        public string BCC { get; set; }

        [JsonProperty(PropertyName = "bysquare", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool BySquare { get; set; }

        [JsonProperty(PropertyName = "cc", NullValueHandling = NullValueHandling.Ignore)]
        public string CC { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "invoice_id", NullValueHandling = NullValueHandling.Ignore)]
        public int InvoiceID { get; set; }

        [JsonProperty(PropertyName = "invoice_lang", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceLang { get; set; }

        [JsonProperty(PropertyName = "message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "no-signature", NullValueHandling = NullValueHandling.Ignore)]
        public bool NoSignature { get; set; }

        [JsonProperty(PropertyName = "payment_info", NullValueHandling = NullValueHandling.Ignore)]
        public bool PaymentInfo { get; set; }

        [JsonProperty(PropertyName = "paypal", NullValueHandling = NullValueHandling.Ignore)]
        public bool PayPal { get; set; }

        [JsonProperty(PropertyName = "recipient", NullValueHandling = NullValueHandling.Ignore)]
        public string Recipient { get; set; }

        [JsonProperty(PropertyName = "subject", NullValueHandling = NullValueHandling.Ignore)]
        public string Subject { get; set; } = string.Empty;


        [JsonProperty(PropertyName = "trustpay", NullValueHandling = NullValueHandling.Ignore)]
        public bool TrustPay { get; set; }
    }
}
