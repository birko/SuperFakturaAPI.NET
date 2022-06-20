using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class Extra
    {
        [JsonProperty(PropertyName = "dimension", NullValueHandling = NullValueHandling.Ignore)]
        public string Dimension { get; set; }
        [JsonProperty(PropertyName = "insurance", NullValueHandling = NullValueHandling.Ignore)]
        public float? Insurance { get; set; }
        [JsonProperty(PropertyName = "parcel_count", NullValueHandling = NullValueHandling.Ignore)]
        public int? ParcelCount { get; set; }
        [JsonProperty(PropertyName = "pickup_point_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? PickupPointID { get; set; }
        [JsonProperty(PropertyName = "tracking_number", NullValueHandling = NullValueHandling.Ignore)]
        public string TrackingNumber { get; set; }
        [JsonProperty(PropertyName = "weight", NullValueHandling = NullValueHandling.Ignore)]
        public float? Weight { get; set; }
        [JsonProperty(PropertyName = "oss", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? OSS { get; set; }
    }
}
