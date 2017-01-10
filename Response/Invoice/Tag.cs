using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class Tag
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; internal set; }
        [JsonProperty(PropertyName = "client_count", NullValueHandling = NullValueHandling.Ignore)]
        public int ClientCount { get; set; }
        [JsonProperty(PropertyName = "expense_count", NullValueHandling = NullValueHandling.Ignore)]
        public int ExpenseCount { get; set; }
        [JsonProperty(PropertyName = "invoice_count", NullValueHandling = NullValueHandling.Ignore)]
        public int InvoiceCount { get; set; }
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int UserProfileID { get; set; }
    }
}
