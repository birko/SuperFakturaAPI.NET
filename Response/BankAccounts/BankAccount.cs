using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Response.BankAccounts
{
    public class BankAccount : Request.BankAccounts.BankAccount
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; internal set; }

        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int UserID { get; set; }

        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int UserProfileID { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Modified { get; set; }
    }

    public class EditBankAccountResponse : ErrorMessageResponse
    {
        [JsonProperty(PropertyName = "message", NullValueHandling = NullValueHandling.Ignore)]
        public BankAccountMessage Message { get; set; } = null;
    }

    public class BankAccountMessage
    {
        [JsonProperty(PropertyName = "BankAccount", NullValueHandling = NullValueHandling.Ignore)]
        public BankAccount BankAccount { get; set; }
    }
}
