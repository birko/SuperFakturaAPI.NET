using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class PayInfo
    {

        [JsonProperty(PropertyName = "to_pay", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ToPay { get; set; }

        [JsonProperty(PropertyName = "to_pay_in_invoice_currency", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ToPayInInvoiceCurrency { get; set; }

        [JsonProperty(PropertyName = "total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Total { get; set; }
    }
}
