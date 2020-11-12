﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;

namespace Birko.SuperFaktura.Response
{
    [JsonArray]
    public class ItemList<T>: List<T>
    {
        [JsonProperty(PropertyName = "_InvoiceSettings", NullValueHandling = NullValueHandling.Ignore)]
        public ExpandoObject InvoiceSettings { get; set; } = null;
    }
}
