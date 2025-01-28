using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace Birko.SuperFaktura.Response.Expense
{
    public class Expense : Request.Expense.Expense
    {
        [JsonProperty(PropertyName = "accounting_date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime AccountingDate { get; set; }

        [JsonProperty(PropertyName = "amount_country_home", NullValueHandling = NullValueHandling.Ignore)]
        public decimal AmountCountryHome { get; set; }

        [JsonProperty(PropertyName = "amount_home", NullValueHandling = NullValueHandling.Ignore)]
        public decimal AmountHome { get; set; }

        [JsonProperty(PropertyName = "amount_paid", NullValueHandling = NullValueHandling.Ignore)]
        public decimal AmountPaid { get; set; }

        [JsonProperty(PropertyName = "amount_paid_vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal AmountPaidVAT { get; set; }

        [JsonProperty(PropertyName = "client_data", NullValueHandling = NullValueHandling.Ignore)]
        public string ClientData { get; set; }

        [JsonProperty(PropertyName = "country_exchange_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal CountryExchangeRate { get; set; }

        [JsonProperty(PropertyName = "demo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringBooleanConverter))]
        public bool? Demo { get; set; } = null;

        [JsonProperty(PropertyName = "discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Discount { get; set; }

        [JsonProperty(PropertyName = "discount_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal DiscountTotal { get; set; }

        [JsonProperty(PropertyName = "exchange_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ExchangeRate { get; set; }

        [JsonProperty(PropertyName = "expense_no", NullValueHandling = NullValueHandling.Ignore)]
        public int? ExpenseNumber { get; set; }

        [JsonProperty(PropertyName = "flag", NullValueHandling = NullValueHandling.Ignore)]
        public string Flag { get; set; }

        [JsonProperty(PropertyName = "home_currency", NullValueHandling = NullValueHandling.Ignore)]
        public string HomeCurrency { get; set; }

        [JsonProperty(PropertyName = "missing_bank_account", NullValueHandling = NullValueHandling.Ignore)]
        public bool MissingBankAccount { get; set; }

        [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Modified { get; set; }

        [JsonProperty(PropertyName = "my_data", NullValueHandling = NullValueHandling.Ignore)]
        public string MyData { get; set; }

        [JsonProperty(PropertyName = "number", NullValueHandling = NullValueHandling.Ignore)]
        public string Number { get; set; }

        [JsonProperty(PropertyName = "paid", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Paid { get; set; }

        [JsonProperty(PropertyName = "paid_vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal PaidVAT { get; set; }

        [JsonProperty(PropertyName = "paydate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? PayDate { get; set; }

        [JsonProperty(PropertyName = "qr", NullValueHandling = NullValueHandling.Ignore)]
        public string QR { get; set; }

        [JsonProperty(PropertyName = "qr_type", NullValueHandling = NullValueHandling.Ignore)]
        public string QRType { get; set; }

        [JsonProperty(PropertyName = "qr_url", NullValueHandling = NullValueHandling.Ignore)]
        public string QRURL { get; set; }

        [JsonProperty(PropertyName = "qr_ur_max", NullValueHandling = NullValueHandling.Ignore)]
        public string QRURLMAx { get; set; }

        [JsonProperty(PropertyName = "rates", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DictionaryConverter<decimal, Rate>))]
        public IDictionary<decimal, Rate> Rates { get; set; }

        [JsonProperty(PropertyName = "recuring", NullValueHandling = NullValueHandling.Ignore)]
        public string Recuring { get; set; }

        [JsonProperty(PropertyName = "sequence_id", NullValueHandling = NullValueHandling.Ignore)]
        public string SequenceID { get; set; }

        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> tags { get; set; }

        [JsonProperty(PropertyName = "tax", NullValueHandling = NullValueHandling.Ignore)]
        public string Tax { get; set; }

        [JsonProperty(PropertyName = "tax_code", NullValueHandling = NullValueHandling.Ignore)]
        public string TaxCode { get; set; }

        [JsonProperty(PropertyName = "taxdate", NullValueHandling = NullValueHandling.Ignore)]
        public string TaxDate { get; set; }

        [JsonProperty(PropertyName = "total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Total { get; set; }

        [JsonProperty(PropertyName = "total_country_home", NullValueHandling = NullValueHandling.Ignore)]
        public decimal TotalCountryHome { get; set; }

        [JsonProperty(PropertyName = "total_home", NullValueHandling = NullValueHandling.Ignore)]
        public decimal TotalHome { get; set; }

        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserID { get; set; }

        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserProfileID { get; set; }
    }
}
