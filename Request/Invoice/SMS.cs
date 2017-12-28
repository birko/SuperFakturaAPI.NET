using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class SMS
    {
        [JsonProperty(PropertyName = "invoice_id", NullValueHandling = NullValueHandling.Ignore)]
        public int InvoiceID { get; set; }
        [JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
        [JsonProperty(PropertyName = "phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }
    }
}
