using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request.BankAccounts
{
    // Every field is optional; null means "not sent" so Edit changes only what was set.
    public class BankAccount : Data
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; set; }

        [JsonProperty(PropertyName = "default", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? Default { get; set; }

        [JsonProperty(PropertyName = "country_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? CountryID { get; set; }

        [JsonProperty(PropertyName = "bank_name", NullValueHandling = NullValueHandling.Ignore)]
        public string BankName { get; set; }

        [JsonProperty(PropertyName = "bank_code", NullValueHandling = NullValueHandling.Ignore)]
        public string BankCode { get; set; }

        // Docs list "bank_account" for requests, but the API stores only "account" (verified on sandbox).
        [JsonProperty(PropertyName = "account", NullValueHandling = NullValueHandling.Ignore)]
        public string Account { get; set; }

        [JsonProperty(PropertyName = "iban", NullValueHandling = NullValueHandling.Ignore)]
        public string IBAN { get; set; }

        [JsonProperty(PropertyName = "swift", NullValueHandling = NullValueHandling.Ignore)]
        public string SWIFT { get; set; }

        // Account setting "show on invoices". Whether the account is shown on a particular invoice is
        // Response.Invoice.InvoiceBankAccount.ShowAccount.
        [JsonProperty(PropertyName = "show", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? Show { get; set; }

        // Not documented, but bank_accounts/add stores it. The API never filters an invoice's accounts
        // by invoice currency.
        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }
    }
}
