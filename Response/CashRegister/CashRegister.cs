using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.CashRegister
{
    public class CashRegisterData {
        [JsonProperty(PropertyName = "CashRegister", NullValueHandling = NullValueHandling.Ignore)]
        public CashRegister CashRegister { get; internal set; }
    }

    public class CashRegister
    {
        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; internal set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; internal set; }

        [JsonProperty(PropertyName = "eet_certificate_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? EETCertificateID { get; internal set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; internal set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; internal set; }

        [JsonProperty(PropertyName = "sequencein_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? SequenceInID { get; internal set; }

        [JsonProperty(PropertyName = "sequenceout_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? SequenceOutID { get; internal set; }
        [JsonProperty(PropertyName = "total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Total { get; internal set; }

        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserID { get; internal set; }

        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserProfileID { get; internal set; }
    }
}
