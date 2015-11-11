using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Invoice
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; internal set; }
        [JsonProperty(PropertyName = "already_paid", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AlreadyPaid { get; set; } = null;
        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreatedDate { get; set; } = null;
         [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ModifiedDate { get; set; } = null;
        [JsonProperty(PropertyName = "comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "constant", NullValueHandling = NullValueHandling.Ignore)]
        public string Constant { get; set; }
        [JsonProperty(PropertyName = "delivery", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DeliveryDate { get; set; } = null;
        [JsonProperty(PropertyName = "delivery_type", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryType { get; set; }
        [JsonProperty(PropertyName = "deposit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Deposit { get; set; } = null;
        [JsonProperty(PropertyName = "discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Discount { get; set; } = null;
        [JsonProperty(PropertyName = "due", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DueDate { get; set; } = null;
        [JsonProperty(PropertyName = "estimate_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? EstimateID { get; set; }
        [JsonProperty(PropertyName = "header_comment", NullValueHandling = NullValueHandling.Ignore)]
        public string HeaderComment { get; set; }
        [JsonProperty(PropertyName = "internal_comment", NullValueHandling = NullValueHandling.Ignore)]
        public string InternalComment{ get; set; }
        [JsonProperty(PropertyName = "invoice_currency", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceCurrency { get; set; } = Invoice.Currency.Euro;
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
        public string PaymentType { get; set; } = Payment.BankTransfer;
        [JsonProperty(PropertyName = "proforma_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ProformaId { get; set; }
        [JsonProperty(PropertyName = "rounding", NullValueHandling = NullValueHandling.Ignore)]
        public string RoundingType { get; set; } = Rounding.Item;
        [JsonProperty(PropertyName = "specific", NullValueHandling = NullValueHandling.Ignore)]
        public string Specific { get; set; }
        [JsonProperty(PropertyName = "sequence_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? SequenceId { get; set; }
        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceType { get; set; } = Type.Regular;
        [JsonProperty(PropertyName = "variable", NullValueHandling = NullValueHandling.Ignore)]
        public string Variable { get; set; }
        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserID { get; set; }
        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserProfileID { get; set; }
        [JsonProperty(PropertyName = "client_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ClientID { get; set; }
        [JsonProperty(PropertyName = "parent_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ParentID { get; set; }
        [JsonProperty(PropertyName = "import_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ImportType { get; set; }
        [JsonProperty(PropertyName = "import_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ImportID { get; set; }
        [JsonProperty(PropertyName = "import_parent_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ImportParentID { get; set; }
        [JsonProperty(PropertyName = "tax_document", NullValueHandling = NullValueHandling.Ignore)]
        public string TaxDocument { get; set; }
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
        [JsonProperty(PropertyName = "order_no", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderNumber { get; set; }
        [JsonProperty(PropertyName = "mask", NullValueHandling = NullValueHandling.Ignore)]
        public string Mask { get; set; }
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceStatus { get; set; }
        [JsonProperty(PropertyName = "home_currency", NullValueHandling = NullValueHandling.Ignore)]
        public string HomeCurrency { get; set; } = Invoice.Currency.Euro;
        [JsonProperty(PropertyName = "exchange_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ExchangeRate{ get; set; } = 1;
        [JsonProperty(PropertyName = "amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Amount { get; set; } = null;
        [JsonProperty(PropertyName = "vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? VAT { get; set; } = null;
        [JsonProperty(PropertyName = "paid", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Paid { get; set; } = null;
        [JsonProperty(PropertyName = "amount_paid", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AmountPaid { get; set; } = null;
        [JsonProperty(PropertyName = "recurring", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Recurring{ get; set; }
        [JsonProperty(PropertyName = "paydate", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic PayDate{ get; set; }
        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Tags{ get; set; }
        [JsonProperty(PropertyName = "token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }
        [JsonProperty(PropertyName = "demo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringBooleanConverter))]
        public bool? Demo { get; set; } = null;
        [JsonProperty(PropertyName = "vat_transfer", NullValueHandling = NullValueHandling.Ignore)]
        public string VATTransfer{ get; set; }
        [JsonProperty(PropertyName = "special_vat_scheme", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic SpecialVATScheme{ get; set; }
        [JsonProperty(PropertyName = "summary_invoice", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic summary_invoice{ get; set; }
    }
}
