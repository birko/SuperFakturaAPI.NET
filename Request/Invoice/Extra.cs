using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class Extra
    {
        [JsonProperty(PropertyName = "pickup_point_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? PickupPointID { get; set; } = null;
    }
}
