using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Response.ValueLists
{
    public class Sequence
    {
        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "default", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool Default { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "item_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ItemType { get; set; }

        [JsonProperty(PropertyName = "mask", NullValueHandling = NullValueHandling.Ignore)]
        public string Mask { get; set; }

        [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Modified { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "period_type", NullValueHandling = NullValueHandling.Ignore)]
        public string PeriodType { get; set; }

        [JsonProperty(PropertyName = "sequence", NullValueHandling = NullValueHandling.Ignore)]
        public int SequenceNumber { get; set; }

        [JsonProperty(PropertyName = "sequence_formatted", NullValueHandling = NullValueHandling.Ignore)]
        public string SequenceFormatted { get; set; }

        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int UserProfileID { get; set; }
    }
}
