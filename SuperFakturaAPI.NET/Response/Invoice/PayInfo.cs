using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class PayInfo
    {
        [JsonProperty(PropertyName = "sent_by", NullValueHandling = NullValueHandling.Ignore)]
        public string SentBy { get; set; }

        [JsonProperty(PropertyName = "sent_to_email", NullValueHandling = NullValueHandling.Ignore)]
        public string SentToEmail { get; set; }

        [JsonProperty(PropertyName = "sent_to_email_cc", NullValueHandling = NullValueHandling.Ignore)]
        public string SentToEmailCC { get; set; }

        [JsonProperty(PropertyName = "to_pay", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ToPay { get; set; }

        [JsonProperty(PropertyName = "to_pay_in_invoice_currency", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ToPayInInvoiceCurrency { get; set; }

        [JsonProperty(PropertyName = "total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Total { get; set; }
    }
}
