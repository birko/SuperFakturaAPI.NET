using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response
{
    public class User : Request.User
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string ID { get;  internal set;}
        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "__password", NullValueHandling = NullValueHandling.Ignore)]
        public string __Password { get; set; }
        [JsonProperty(PropertyName = "group_id", NullValueHandling = NullValueHandling.Ignore)]
        public int GroupID { get; set; }
        [JsonProperty(PropertyName = "otl", NullValueHandling = NullValueHandling.Ignore)]
        public string OTL { get; set; }
        [JsonProperty(PropertyName = "redirect", NullValueHandling = NullValueHandling.Ignore)]
        public string Redirect { get; set; }
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public int Status { get; set; }
        [JsonProperty(PropertyName = "timezone", NullValueHandling = NullValueHandling.Ignore)]
        public int TimeZone { get; set; }
        [JsonProperty(PropertyName = "token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }
        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

    }
}
