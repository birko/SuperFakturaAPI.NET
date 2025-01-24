using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Birko.SuperFaktura.Request.Expense
{
    public class ExpenseBasic
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; internal set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }

    public class Expense : ExpenseBasic
    {
        [JsonProperty(PropertyName = "attachment", NullValueHandling = NullValueHandling.Ignore)]
        public string Attachment { get; set; }

        [JsonProperty(PropertyName = "already_paid", NullValueHandling = NullValueHandling.Ignore)]
        public bool AlreadyPaid { get; set; } = false;

        [JsonProperty(PropertyName = "amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "amount2", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Amount2 { get; set; }

        [JsonProperty(PropertyName = "amount3", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Amount3 { get; set; }

        [JsonProperty(PropertyName = "client_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ClientID { get; set; }

        [JsonProperty(PropertyName = "comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "constant", NullValueHandling = NullValueHandling.Ignore)]
        public string Constant { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; set; } = DateTime.Now;

        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; } = RegionInfo.CurrentRegion.ISOCurrencySymbol;

        [JsonProperty(PropertyName = "delivery", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Delivery { get; set; } = DateTime.Now;

        [JsonProperty(PropertyName = "document_number", NullValueHandling = NullValueHandling.Ignore)]
        public int DocumentNumber { get; set; }

        [JsonProperty(PropertyName = "due", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Due { get; set; } = DateTime.Now;

        [JsonProperty(PropertyName = "expense_category_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ExpenseCategoryID { get; internal set; }

        [JsonProperty(PropertyName = "payment_type", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentType { get; set; }

        [JsonProperty(PropertyName = "specific", NullValueHandling = NullValueHandling.Ignore)]
        public string Specific { get; set; }

        [JsonProperty(PropertyName = "taxable_supply", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? TaxableSupply { get; set; }

        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string ExpenseType { get; set; } = Type.Invoice;

        [JsonProperty(PropertyName = "variable", NullValueHandling = NullValueHandling.Ignore)]
        public string Variable { get; set; }

        [JsonProperty(PropertyName = "vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal VAT { get; set; }

        [JsonProperty(PropertyName = "vat2", NullValueHandling = NullValueHandling.Ignore)]
        public decimal VAT2 { get; set; }

        [JsonProperty(PropertyName = "vat3", NullValueHandling = NullValueHandling.Ignore)]
        public decimal VAT3 { get; set; }

        [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; } = ExpenseVersion.Basic;
    }


    public static class ExpenseVersion
    {
        public const string Basic = "basic";
        public const string Items = "items";
    }
}
