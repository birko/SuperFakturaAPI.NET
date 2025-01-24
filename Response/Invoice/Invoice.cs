﻿using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class Invoice : Birko.SuperFaktura.Request.Invoice.Invoice
    {
        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserID { get; set; }
        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserProfileID { get; set; }
        [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Modified { get; set; } = null;
        [JsonProperty(PropertyName = "import_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ImportType { get; set; }
        [JsonProperty(PropertyName = "import_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ImportID { get; set; }
        [JsonProperty(PropertyName = "import_parent_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ImportParentID { get; set; }
        [JsonProperty(PropertyName = "lang", NullValueHandling = NullValueHandling.Ignore)]
        public string Lang { get; set; }
        [JsonProperty(PropertyName = "client_data", NullValueHandling = NullValueHandling.Ignore)]
        public string ClientData { get; set; }
        [JsonProperty(PropertyName = "my_data", NullValueHandling = NullValueHandling.Ignore)]
        public string MyData { get; set; }
        [JsonProperty(PropertyName = "items_data", NullValueHandling = NullValueHandling.Ignore)]
        public string ItemsData { get; set; }
        [JsonProperty(PropertyName = "invoice_no", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceNumber { get; set; }
        [JsonProperty(PropertyName = "mask", NullValueHandling = NullValueHandling.Ignore)]
        public string Mask { get; set; }
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceStatus { get; set; }
        [JsonProperty(PropertyName = "token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }
        [JsonProperty(PropertyName = "demo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringBooleanConverter))]
        public bool? Demo { get; set; } = null;
        [JsonProperty(PropertyName = "vat_transfer", NullValueHandling = NullValueHandling.Ignore)]
        public string VATTransfer { get; set; }
        [JsonProperty(PropertyName = "special_vat_scheme", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic SpecialVATScheme { get; set; }
        [JsonProperty(PropertyName = "summary_invoice", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic SummaryInvoice { get; set; }
        [JsonProperty(PropertyName = "paid", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Paid { get; set; } = null;
        [JsonProperty(PropertyName = "amount_paid", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AmountPaid { get; set; } = null;
        [JsonProperty(PropertyName = "recurring", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Recurring { get; set; }
        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)]
        public string Tags { get; set; }
        [JsonProperty(PropertyName = "home_currency", NullValueHandling = NullValueHandling.Ignore)]
        public string HomeCurrency { get; set; } = RegionInfo.CurrentRegion.ISOCurrencySymbol;
        [JsonProperty(PropertyName = "exchange_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ExchangeRate { get; set; } = 1;
        [JsonProperty(PropertyName = "amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Amount { get; set; } = null;
        [JsonProperty(PropertyName = "vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? VAT { get; set; } = null;
    }
}
