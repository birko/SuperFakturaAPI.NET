using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Birko.SuperFaktura.Response.Invoice
{
    [JsonConverter(typeof(InvoiceItemConverter))]
    public class ListItem: List<Stats>
    {
        [JsonProperty(PropertyName = "Client", NullValueHandling = NullValueHandling.Ignore)]
        public Client.Client Client { get; set; } = null;
        [JsonProperty(PropertyName = "Invoice", NullValueHandling = NullValueHandling.Ignore)]
        public Invoice Invoice { get; set; } = null;
        [JsonProperty(PropertyName = "InvoicePayment", NullValueHandling = NullValueHandling.Ignore)]
        public Payment InvoicePayment { get; set; } = null;
        [JsonProperty(PropertyName = "InvoiceEmail", NullValueHandling = NullValueHandling.Ignore)]
        public Email InvoiceEmail { get; set; } = null;
        [JsonProperty(PropertyName = "PostStamp", NullValueHandling = NullValueHandling.Ignore)]
        public PostStamp PostStamp { get; set; } = null;
        [JsonProperty(PropertyName = "_InvoiceSettings", NullValueHandling = NullValueHandling.Ignore)]
        public ExpandoObject InvoiceSettings { get; set; } = null;
    }
}
