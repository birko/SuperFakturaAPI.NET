using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class Signature
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; internal set; }
        [JsonProperty(PropertyName = "model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }
        [JsonProperty(PropertyName = "foreign_key", NullValueHandling = NullValueHandling.Ignore)]
        public int? ForeignKey { get; internal set; }
        [JsonProperty(PropertyName = "dirname", NullValueHandling = NullValueHandling.Ignore)]
        public string DirName { get; set; }
        [JsonProperty(PropertyName = "basename", NullValueHandling = NullValueHandling.Ignore)]
        public string BaseName { get; set; }
        [JsonProperty(PropertyName = "checksum", NullValueHandling = NullValueHandling.Ignore)]
        public string ChekSum { get; set; }
        [JsonProperty(PropertyName = "group", NullValueHandling = NullValueHandling.Ignore)]
        public string Group { get; set; }
        [JsonProperty(PropertyName = "alternative", NullValueHandling = NullValueHandling.Ignore)]
        public string Alternative { get; set; }
        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; set; }
        [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Modified { get; set; }
    }
}
