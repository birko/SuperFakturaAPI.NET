﻿using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Response.ValueLists
{
    public class Logo
    {
        [JsonProperty(PropertyName = "alternative", NullValueHandling = NullValueHandling.Ignore)]
        public string Alternative { get; set; }

        [JsonProperty(PropertyName = "basename", NullValueHandling = NullValueHandling.Ignore)]
        public string BaseName { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "default", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringBooleanConverter))]
        public bool Default { get; set; }

        [JsonProperty(PropertyName = "dirname", NullValueHandling = NullValueHandling.Ignore)]
        public string DirName { get; set; }

        [JsonProperty(PropertyName = "foreign_key", NullValueHandling = NullValueHandling.Ignore)]
        public int? ForeignKey { get; set; }

        [JsonProperty(PropertyName = "group", NullValueHandling = NullValueHandling.Ignore)]
        public string Group { get; set; }

        [JsonProperty(PropertyName = "checksum", NullValueHandling = NullValueHandling.Ignore)]
        public string CheckSum { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; internal set; }

        [JsonProperty(PropertyName = "model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }

        [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Modified { get; set; }

        [JsonProperty(PropertyName = "path", NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "size", NullValueHandling = NullValueHandling.Ignore)]
        public int Size { get; set; }

        [JsonProperty(PropertyName = "url", NullValueHandling = NullValueHandling.Ignore)]
        public string URL { get; set; }
    }
}
