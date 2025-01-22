using Birko.SuperFaktura.Response.Invoice;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Birko.SuperFaktura.Response.Client
{
    public class Client: Request.Client.Client
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; internal set; }

        [JsonProperty(PropertyName = "bank_account_id", NullValueHandling = NullValueHandling.Ignore)]
        public string BankAccountID { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "bank_account_prefix", NullValueHandling = NullValueHandling.Ignore)]
        public string BankAccountPrefix { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserID { get; set; }

        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserProfileID { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Created { get; set; }

        [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Modified { get; set; }
    }

    public class ResponseClient
    {
        public Client Client { get; set; }
    }

    public class DetailClient: TagResponse
    {
        public Client Client { get; set; }
        public Country Country { get; set; }
        public IEnumerable<ContactPersons.ContactPerson> ContactPerson { get; set; }
    }
}
