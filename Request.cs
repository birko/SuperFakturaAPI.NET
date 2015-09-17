using Raven.Imports.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura
{
    public class Request
    {
        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }
    }
}
