using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Response.Expense
{
    public class Document
    {
        [JsonProperty(PropertyName = "alternative", NullValueHandling = NullValueHandling.Ignore)]
        public string Alternative { get; set; }
        [JsonProperty(PropertyName = "basename", NullValueHandling = NullValueHandling.Ignore)]
        public string BaseName { get; set; }
        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; set; }
        [JsonProperty(PropertyName = "default", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Default { get; set; }
        [JsonProperty(PropertyName = "dirname", NullValueHandling = NullValueHandling.Ignore)]
        public string DirName { get; set; }
        [JsonProperty(PropertyName = "foreign_key", NullValueHandling = NullValueHandling.Ignore)]
        public int? ForeignKey { get; set; }
        [JsonProperty(PropertyName = "group", NullValueHandling = NullValueHandling.Ignore)]
        public string Group { get; set; }
        [JsonProperty(PropertyName = "checksum", NullValueHandling = NullValueHandling.Ignore)]
        public string Cheksum { get; set; }
        [JsonProperty(PropertyName = "ID", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; set; }
        [JsonProperty(PropertyName = "model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }
        [JsonProperty(PropertyName = "Modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Modified { get; set; }
    }
}
