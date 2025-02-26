using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class Invoice : Birko.SuperFaktura.Request.Invoice.Invoice
    {
        [JsonProperty(PropertyName = "accounting_date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime AccountingDate { get; set; }

        [JsonProperty(PropertyName = "amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Amount { get; set; } = null;

        [JsonProperty(PropertyName = "amount_paid", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AmountPaid { get; set; } = null;

        [JsonProperty(PropertyName = "client_data", NullValueHandling = NullValueHandling.Ignore)]
        public string ClientData { get; set; }

        [JsonProperty(PropertyName = "demo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringBooleanConverter))]
        public bool? Demo { get; set; } = null;

        [JsonProperty(PropertyName = "flag", NullValueHandling = NullValueHandling.Ignore)]
        public string Flag { get; set; }

        [JsonProperty(PropertyName = "import_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ImportID { get; set; }

        [JsonProperty(PropertyName = "import_parent_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ImportParentID { get; set; }

        [JsonProperty(PropertyName = "import_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ImportType { get; set; }

        [JsonProperty(PropertyName = "invoice_no", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceNumber { get; set; }

        [JsonProperty(PropertyName = "invoice_no_formatted_length", NullValueHandling = NullValueHandling.Ignore)]
        public int InvoiceNumberFormattedLength { get; set; }

        [JsonProperty(PropertyName = "invoice_no_formatted_raw", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceNumberFormattedRaw { get; set; }

        [JsonProperty(PropertyName = "items_data", NullValueHandling = NullValueHandling.Ignore)]
        public string ItemsData { get; set; }

        [JsonProperty(PropertyName = "items_name", NullValueHandling = NullValueHandling.Ignore)]
        public string ItemsName { get; set; }

        [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Modified { get; set; } = null;

        [JsonProperty(PropertyName = "my_data", NullValueHandling = NullValueHandling.Ignore)]
        public string MyData { get; set; }

        [JsonProperty(PropertyName = "paid", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Paid { get; set; } = null;

        [JsonProperty(PropertyName = "recurring", NullValueHandling = NullValueHandling.Ignore)]
        public string Recurring { get; set; }

        [JsonProperty(PropertyName = "show_items_with_dph", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShowItemsWithVAT { get; set; } = null;

        [JsonProperty(PropertyName = "show_special_vat", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShowSpecialVAT { get; set; } = null;

        [JsonProperty(PropertyName = "special_vat_scheme", NullValueHandling = NullValueHandling.Ignore)]
        public string SpecialVATScheme { get; set; }

        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "summary_invoice", NullValueHandling = NullValueHandling.Ignore)]
        public int SummaryInvoice { get; set; }

        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)]
        public string Tags { get; set; }

        [JsonProperty(PropertyName = "taxdate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? TaxDate { get; set; }

        [JsonProperty(PropertyName = "token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserID { get; set; }

        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserProfileID { get; set; }

        [JsonProperty(PropertyName = "variable_raw", NullValueHandling = NullValueHandling.Ignore)]
        public string VariableRAW { get; set; }

        [JsonProperty(PropertyName = "vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? VAT { get; set; }
    }
}
