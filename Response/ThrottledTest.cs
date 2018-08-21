using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response
{
    public class ThrottledTest
    {
        [JsonProperty(PropertyName = "throttled", NullValueHandling = NullValueHandling.Ignore)]
        public String Throttled { get; set; } = null;
    }
}
