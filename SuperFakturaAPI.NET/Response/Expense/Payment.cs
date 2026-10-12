using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Response.Expense
{
    public class Payment : Request.Expense.Payment
    {
        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; set; }
        [JsonProperty(PropertyName = "exchange_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ExchangeRate { get; set; }
        [JsonProperty(PropertyName = "expense_exchange_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ExpenseExchangeRate { get; set; }
        [JsonProperty(PropertyName = "country_exchange_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? CountryExchangeRate { get; set; }
        [JsonProperty(PropertyName = "amount_home", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AmountHome { get; set; }
        [JsonProperty(PropertyName = "amount_country_home", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AmountCountryHome { get; set; }
        [JsonProperty(PropertyName = "amount_expense", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AmountExpense { get; set; }
        [JsonProperty(PropertyName = "vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? VAT { get; set; }
        [JsonProperty(PropertyName = "total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Total { get; set; }
        [JsonProperty(PropertyName = "total_home", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TotalHome { get; set; }
        [JsonProperty(PropertyName = "total_country_home", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TotalCountryHome { get; set; }
        [JsonProperty(PropertyName = "total_expense", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TotalExpense { get; set; }
        [JsonProperty(PropertyName = "document_no", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentNo { get; set; }
        [JsonProperty(PropertyName = "force_paid", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? ForcePaid { get; set; }
        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserID { get; set; }
        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserProfileID { get; set; }
    }
}
