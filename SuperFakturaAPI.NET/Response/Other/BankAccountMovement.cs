using Birko.SuperFaktura.Response.Expense;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Other
{
    public class BankAccountMovement
    {

        [JsonProperty(PropertyName = "account_id", NullValueHandling = NullValueHandling.Ignore)]
        public int AccountID { get; set; }

        [JsonProperty(PropertyName = "account_settings", NullValueHandling = NullValueHandling.Ignore)]
        public string AccountSettings { get; set; } = null;

        [JsonProperty(PropertyName = "amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "client_account", NullValueHandling = NullValueHandling.Ignore)]
        public string ClientAccount { get; set; }

        [JsonProperty(PropertyName = "comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "documents", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Document> documents { get; set; }

        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }

        [JsonProperty(PropertyName = "from_type", NullValueHandling = NullValueHandling.Ignore)]
        public string FromType { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "transaction_id", NullValueHandling = NullValueHandling.Ignore)]
        public string TransactionID { get; set; }

        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "variable", NullValueHandling = NullValueHandling.Ignore)]
        public string Variable { get; set; }
    }
}
