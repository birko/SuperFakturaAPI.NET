using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class PostStamp
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; set; }

        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserID { get; set; }

        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserProfileID { get; set; }

        [JsonProperty(PropertyName = "invoice_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? InvoiceID { get; set; }

        [JsonProperty(PropertyName = "invoice_language", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceLanguage { get; set; }

        [JsonProperty(PropertyName = "no_signature", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringBooleanConverter))]
        public bool NoSignature { get; set; }

        [JsonProperty(PropertyName = "payment_info", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringBooleanConverter))]
        public bool PaymentInfo { get; set; }

        [JsonProperty(PropertyName = "bysquare", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringBooleanConverter))]
        public bool BySquare { get; set; }

        [JsonProperty(PropertyName = "recycled", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringBooleanConverter))]
        public bool Recycled { get; set; }

        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "sent_to", NullValueHandling = NullValueHandling.Ignore)]
        public string SendTo { get; set; }

        [JsonProperty(PropertyName = "external_service", NullValueHandling = NullValueHandling.Ignore)]
        public string ExternalService { get; set; }

        [JsonProperty(PropertyName = "external_service_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ExternalServiceID { get; set; }

        [JsonProperty(PropertyName = "external_response", NullValueHandling = NullValueHandling.Ignore)]
        public string ExternalResponse { get; set; }

        [JsonProperty(PropertyName = "external_status", NullValueHandling = NullValueHandling.Ignore)]
        public string ExternalStatus { get; set; }

        [JsonProperty(PropertyName = "errors", NullValueHandling = NullValueHandling.Ignore)]
        public string Errors { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Created { get; set; }

        [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Modified { get; set; }

        [JsonProperty(PropertyName = "requested", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Requested { get; set; }

        [JsonProperty(PropertyName = "sent", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Send { get; set; }
    }
}
