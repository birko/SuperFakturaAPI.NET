﻿using Birko.SuperFaktura.Request.BankAccounts;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class InvoiceBasic
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; internal set; }

        [JsonProperty(PropertyName = "invoice_no_formatted", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceNumberFormatted { get; set; }
    }

    public class InvoiceInfo : InvoiceBasic
    {
        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type{ get; internal set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "tax_document", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? TaxDocument { get; set; }

    }

    public class Invoice : InvoiceInfo
    {
        [JsonProperty(PropertyName = "add_rounding_item", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? AddRoundingItem { get; set; } = null;

        [JsonProperty(PropertyName = "already_paid", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AlreadyPaid { get; set; } = null;

        [JsonProperty(PropertyName = "bank_accounts", NullValueHandling = NullValueHandling.Ignore)]
        public BankAccount[] BankAccounts { get; set; }

        [JsonProperty(PropertyName = "comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "constant", NullValueHandling = NullValueHandling.Ignore)]
        public string Constant { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Created { get; set; } = null;

        [JsonProperty(PropertyName = "country_exchange_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal CountryExchangeRate { get; set; } = 1;

        [JsonProperty(PropertyName = "delivery", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Delivery { get; set; } = null;

        [JsonProperty(PropertyName = "delivery_type", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryType { get; set; } = null;

        [JsonProperty(PropertyName = "deposit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Deposit { get; set; } = null;

        [JsonProperty(PropertyName = "discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Discount { get; set; } = null;

        [JsonProperty(PropertyName = "discount_total ", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? DiscountTotal { get; set; } = null;

        [JsonProperty(PropertyName = "due", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DueDate { get; set; } = null;

        [JsonProperty(PropertyName = "estimate_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? EstimateID { get; set; } = null;

        [JsonProperty(PropertyName = "exchange_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ExchangeRate { get; set; } = 1;

        [JsonProperty(PropertyName = "header_comment", NullValueHandling = NullValueHandling.Ignore)]
        public string HeaderComment { get; set; }

        [JsonProperty(PropertyName = "internal_comment", NullValueHandling = NullValueHandling.Ignore)]
        public string InternalComment { get; set; }

        [JsonProperty(PropertyName = "invoice_currency", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceCurrency { get; set; } = RegionInfo.CurrentRegion.ISOCurrencySymbol;

        [JsonProperty(PropertyName = "issued_by", NullValueHandling = NullValueHandling.Ignore)]
        public string IssuedBy { get; set; }

        [JsonProperty(PropertyName = "issued_by_phone", NullValueHandling = NullValueHandling.Ignore)]
        public string IssuedByPhone { get; set; }

        [JsonProperty(PropertyName = "issued_by_email", NullValueHandling = NullValueHandling.Ignore)]
        public string IssuedByEmail { get; set; }

        [JsonProperty(PropertyName = "issued_by_web", NullValueHandling = NullValueHandling.Ignore)]
        public string IssuedByWeb { get; set; }

        [JsonProperty(PropertyName = "logo_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? LogoID { get; set; }

        [JsonProperty(PropertyName = "mark_sent", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool MarkSent { get; set; }

        [JsonProperty(PropertyName = "mark_sent_message", NullValueHandling = NullValueHandling.Ignore)]
        public string MarkSentMessage { get; set; }

        [JsonProperty(PropertyName = "mark_sent_subject", NullValueHandling = NullValueHandling.Ignore)]
        public string MarkSentSubject { get; set; }

        [JsonProperty(PropertyName = "order_no", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderNumber { get; set; }

        [JsonProperty(PropertyName = "parent_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ParentID { get; set; }

        [JsonProperty(PropertyName = "paydate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? PayDate { get; set; } = null;

        [JsonProperty(PropertyName = "payment_type", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentType { get; set; } = ValueLists.PaymentType.BankTransfer;

        [JsonProperty(PropertyName = "proforma_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ProformaID { get; set; } = null;

        [JsonProperty(PropertyName = "rounding", NullValueHandling = NullValueHandling.Ignore)]
        public string RoundingType { get; set; } = ValueLists.RoundingType.Item;

        [JsonProperty(PropertyName = "sequence_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? SequenceID { get; set; } = null;

        [JsonProperty(PropertyName = "specific", NullValueHandling = NullValueHandling.Ignore)]
        public string Specific { get; set; }

        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceType { get; set; } = ValueLists.InvoiceType.Regular;

        [JsonProperty(PropertyName = "variable", NullValueHandling = NullValueHandling.Ignore)]
        public string Variable { get; set; }

        [JsonProperty(PropertyName = "vat_transfer", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool VATTransfer { get; set; }
    }

}
