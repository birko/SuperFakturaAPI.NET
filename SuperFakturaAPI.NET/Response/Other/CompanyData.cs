using Birko.SuperFaktura.Response.BankAccounts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Other
{
    public class CompanyData
    {
        [JsonProperty(PropertyName = "BankAccount", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<BankAccount> BankAccount { get; set; }

        [JsonProperty(PropertyName = "UserProfile", NullValueHandling = NullValueHandling.Ignore)]
        public UserProfile UserProfile { get; set; }
    }
}
