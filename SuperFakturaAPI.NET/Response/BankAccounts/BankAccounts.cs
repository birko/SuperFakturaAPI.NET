using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Birko.SuperFaktura.Response.BankAccounts
{
    public class BankAccounts: ErrorResponse
    {
        [JsonProperty(PropertyName = "BankAccounts", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<BankAccountData> Accounts { get; set; }
    }

    public class BankAccountData: ErrorResponse
    {
        [JsonProperty(PropertyName = "BankAccount", NullValueHandling = NullValueHandling.Ignore)]
        public BankAccount BankAccount { get; set; }
    }
}
