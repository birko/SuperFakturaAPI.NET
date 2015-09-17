using Raven.Imports.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Invoice
    {
        [JsonProperty(PropertyName = "id")]
        public int? ID { get; internal set; }
        [JsonProperty(PropertyName = "already_paid")]
        public bool AlreadyPaid { get; set; } = false;
        [JsonProperty(PropertyName = "created")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }
        [JsonProperty(PropertyName = "constant")]
        public string Constant { get; set; }
        [JsonProperty(PropertyName = "delivery")]
        public DateTime DeliveryDate { get; set; }
        [JsonProperty(PropertyName = "delivery_date")]
        public string DeliveryType { get; set; } = Delivery.Courier;
        [JsonProperty(PropertyName = "deposit")]
        public decimal Deposit { get; set; } = 0;
        [JsonProperty(PropertyName = "discount")]
        public decimal Discount { get; set; } = 0;
        [JsonProperty(PropertyName = "due")]
        public DateTime DueDate { get; set; } = DateTime.Now;
        [JsonProperty(PropertyName = "estimate_id")]
        public int? EstimateID { get; set; }
        [JsonProperty(PropertyName = "header_comment")]
        public string HeaderComment { get; set; }
        [JsonProperty(PropertyName = "internal_comment")]
        public string InternalComment{ get; set; }
        [JsonProperty(PropertyName = "invoice_currency")]
        public string InvoiceCurrency { get; set; } = Invoice.Currency.Euro;
        [JsonProperty(PropertyName = "idnvoice_no_formatted")]
        public string InvoiceNoFormatted { get; set; }
        [JsonProperty(PropertyName = "issued_by")]
        public string IssuedBy { get; set; }
        [JsonProperty(PropertyName = "issued_by_phone")]
        public string IssuedByPhone { get; set; }
        [JsonProperty(PropertyName = "issued_by_email")]
        public string IssuedByEmail { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "payment_type")]
        public string PaymentType { get; set; } = Payment.BankTransfer;
        [JsonProperty(PropertyName = "proforma_id")]
        public int? ProformaId { get; set; }
        [JsonProperty(PropertyName = "rounding")]
        public string RoundingType { get; set; } = Rounding.Item;
        [JsonProperty(PropertyName = "specific")]
        public string Specific { get; set; }
        [JsonProperty(PropertyName = "sequence_id")]
        public int? SequenceId { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string InvoiceType { get; set; } = Type.Regular;
        [JsonProperty(PropertyName = "variable")]
        public string Variable { get; set; }

    }
}
