using Raven.Imports.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Invoice
    {
        public class Post
        {
            [JsonProperty(PropertyName = "invoice_id")]
            public int InvoiceID { get; set; }
            [JsonProperty(PropertyName = "delivery_address")]
            public string DeliveryAddress { get; set; }
            [JsonProperty(PropertyName = "delivery_city")]
            public string DeliveryCity { get; set; }
            [JsonProperty(PropertyName = "delivery_state")]
            public string DeliveryState{ get; set; }
        }
    }
}
