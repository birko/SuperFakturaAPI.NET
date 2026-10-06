using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Request.Expense
{
    public class ExpenseBasic
    {
        // Required for Edit.
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }

    // Fields left null are not sent; the server applies its defaults on Add and keeps stored values on Edit.
    public class Expense : ExpenseBasic
    {
        [JsonProperty(PropertyName = "attachment", NullValueHandling = NullValueHandling.Ignore)]
        public string Attachment { get; set; }

        [JsonProperty(PropertyName = "already_paid", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? AlreadyPaid { get; set; }

        // amount*/vat* are used only with version "basic".
        [JsonProperty(PropertyName = "amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Amount { get; set; }

        [JsonProperty(PropertyName = "amount2", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Amount2 { get; set; }

        [JsonProperty(PropertyName = "amount3", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Amount3 { get; set; }

        [JsonProperty(PropertyName = "client_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ClientID { get; set; }

        [JsonProperty(PropertyName = "comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "constant", NullValueHandling = NullValueHandling.Ignore)]
        public string Constant { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DateConverter))]
        public DateTime? Created { get; set; }

        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "delivery", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DateConverter))]
        public DateTime? Delivery { get; set; }

        // E.g. supplier's invoice number "FA2026/0042".
        [JsonProperty(PropertyName = "document_number", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentNumber { get; set; }

        [JsonProperty(PropertyName = "due", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DateConverter))]
        public DateTime? Due { get; set; }

        [JsonProperty(PropertyName = "expense_category_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ExpenseCategoryID { get; set; }

        [JsonProperty(PropertyName = "payment_type", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentType { get; set; }

        [JsonProperty(PropertyName = "specific", NullValueHandling = NullValueHandling.Ignore)]
        public string Specific { get; set; }

        [JsonProperty(PropertyName = "taxable_supply", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DateConverter))]
        public DateTime? TaxableSupply { get; set; }

        // Server default: ValueLists.ExpenseType.Invoice.
        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string ExpenseType { get; set; }

        [JsonProperty(PropertyName = "variable", NullValueHandling = NullValueHandling.Ignore)]
        public string Variable { get; set; }

        [JsonProperty(PropertyName = "vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? VAT { get; set; }

        [JsonProperty(PropertyName = "vat2", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? VAT2 { get; set; }

        [JsonProperty(PropertyName = "vat3", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? VAT3 { get; set; }

        // Server default: ValueLists.ExpenseVersion.Basic.
        [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }
    }
}
