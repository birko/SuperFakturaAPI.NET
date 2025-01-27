using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class Mark : ErrorMessageResponse
    {
        [JsonProperty(PropertyName = "marked", NullValueHandling = NullValueHandling.Ignore)]
        public bool Marked { get; set; }
    }
}
