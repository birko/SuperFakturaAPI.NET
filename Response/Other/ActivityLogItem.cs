using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Other
{
    public class ActivityLogItem
    {

        [JsonProperty(PropertyName = "ActivityLog", NullValueHandling = NullValueHandling.Ignore)]
        public ActivityLog ActivityLog { get; set; }

        [JsonProperty(PropertyName = "User", NullValueHandling = NullValueHandling.Ignore)]
        public User User { get; set; } = null;
    }
}
