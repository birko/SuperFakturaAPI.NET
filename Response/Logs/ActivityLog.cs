using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Logs
{
    public class ActivityLog
    {

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }
        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public string Data { get; set; } = null;
        [JsonProperty(PropertyName = "item_id", NullValueHandling = NullValueHandling.Ignore)]
        public int ItemID { get; set; }
        [JsonProperty(PropertyName = "item_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ItemType { get; set; }
        [JsonProperty(PropertyName = "Event_type", NullValueHandling = NullValueHandling.Ignore)]
        public string EventType { get; set; }
        [JsonProperty(PropertyName = "client_id", NullValueHandling = NullValueHandling.Ignore)]
        public int ClientID { get; set; }
        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int UserID { get; set; }
    }
}
