using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Response.Invoice
{
    // Bank account as stored on an invoice (MyData.BankAccount). Its shape depends on how the invoice was saved:
    // - API without bank_accounts: every profile account with all fields; show_account copies show.
    // - API with bank_accounts: only the sent accounts; sent without an id they have only the sent fields.
    // - Web UI: every profile account, but the form posts only id, show_account, default, country_id,
    //   bank_name, bank_code, account, iban and swift - Show and Currency are null.
    public class InvoiceBankAccount : Request.BankAccounts.BankAccount
    {
        // Whether this account is shown on this invoice (web UI: chosen per invoice, independent of Show).
        // Use this, not Show, to pick the invoice's accounts.
        [JsonProperty(PropertyName = "show_account", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? ShowAccount { get; set; }

        // Copied from the profile account; missing on accounts sent in bank_accounts without an id.
        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserID { get; set; }

        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserProfileID { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Created { get; set; }

        [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Modified { get; set; }
    }
}
