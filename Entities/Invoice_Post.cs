using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Invoice
    {
        public class Post
        {
            [JsonProperty(PropertyName = "invoice_id", NullValueHandling = NullValueHandling.Ignore)]
            public int InvoiceID { get; set; }
            [JsonProperty(PropertyName = "delivery_name", NullValueHandling = NullValueHandling.Ignore)]
            public string DeliveryName { get; set; }
            [JsonProperty(PropertyName = "delivery_address", NullValueHandling = NullValueHandling.Ignore)]
            public string DeliveryAddress { get; set; }
            [JsonProperty(PropertyName = "delivery_city", NullValueHandling = NullValueHandling.Ignore)]
            public string DeliveryCity { get; set; }
            [JsonProperty(PropertyName = "delivery_zip", NullValueHandling = NullValueHandling.Ignore)]
            public string DeliveryZIP { get; set; }
            [JsonProperty(PropertyName = "delivery_state", NullValueHandling = NullValueHandling.Ignore)]
            public string DeliveryState{ get; set; }
        }
    }
}
