using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class Invoice
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; internal set; }
        [JsonProperty(PropertyName = "already_paid", NullValueHandling = NullValueHandling.Ignore)]
        public bool AlreadyPaid { get; set; } = false;
        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Created { get; set; } = null;
        [JsonProperty(PropertyName = "client_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ClientID { get; set; }
        [JsonProperty(PropertyName = "comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "constant", NullValueHandling = NullValueHandling.Ignore)]
        public string Constant { get; set; }
        [JsonProperty(PropertyName = "delivery", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Delivery { get; set; } = null;
        [JsonProperty(PropertyName = "delivery_type", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryType { get; set; } = null;
        [JsonProperty(PropertyName = "deposit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Deposit { get; set; } = 0;
        [JsonProperty(PropertyName = "due", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DueDate { get; set; } = null;
        [JsonProperty(PropertyName = "estimate_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? EstimateID { get; set; }
        [JsonProperty(PropertyName = "header_comment", NullValueHandling = NullValueHandling.Ignore)]
        public string HeaderComment { get; set; }
        [JsonProperty(PropertyName = "internal_comment", NullValueHandling = NullValueHandling.Ignore)]
        public string InternalComment { get; set; }
        [JsonProperty(PropertyName = "invoice_currency", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceCurrency { get; set; } = Birko.SuperFaktura.Request.Invoice.CurrencyType.Euro;
        [JsonProperty(PropertyName = "invoice_no_formatted", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceNoFormatted { get; set; }
        [JsonProperty(PropertyName = "issued_by", NullValueHandling = NullValueHandling.Ignore)]
        public string IssuedBy { get; set; }
        [JsonProperty(PropertyName = "issued_by_phone", NullValueHandling = NullValueHandling.Ignore)]
        public string IssuedByPhone { get; set; }
        [JsonProperty(PropertyName = "issued_by_email", NullValueHandling = NullValueHandling.Ignore)]
        public string IssuedByEmail { get; set; }
        [JsonProperty(PropertyName = "issued_by_web", NullValueHandling = NullValueHandling.Ignore)]
        public string IssuedByWeb { get; set; }
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "payment_type", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentType { get; set; } = Birko.SuperFaktura.Request.Invoice.PaymentType.BankTransfer;
        [JsonProperty(PropertyName = "proforma_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ProformaID { get; set; }
        [JsonProperty(PropertyName = "parent_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ParentID { get; set; }
        [JsonProperty(PropertyName = "rounding", NullValueHandling = NullValueHandling.Ignore)]
        public string RoundingType { get; set; } = Birko.SuperFaktura.Request.Invoice.RoundingType.Item;
        [JsonProperty(PropertyName = "specific", NullValueHandling = NullValueHandling.Ignore)]
        public string Specific { get; set; }
        [JsonProperty(PropertyName = "sequence_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? SequenceID { get; set; }
        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceType { get; set; } = Birko.SuperFaktura.Request.Invoice.Type.Regular;
        [JsonProperty(PropertyName = "variable", NullValueHandling = NullValueHandling.Ignore)]
        public string Variable { get; set; }
        [JsonProperty(PropertyName = "bank_accounts", NullValueHandling = NullValueHandling.Ignore)]
        public BankAccount[] BankAccounts { get; set; }
        [JsonProperty(PropertyName = "order_no", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderNumber { get; set; }
        [JsonProperty(PropertyName = "logo_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? LogoID { get; set; }
        [JsonProperty(PropertyName = "tax_document", NullValueHandling = NullValueHandling.Ignore)]
        public bool? TaxDocument { get; set; }

    }
}
