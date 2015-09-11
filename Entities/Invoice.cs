using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Invoice
    {
        public int? ID { get; set; }
        public bool AlreadyPaid { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Comment { get; set; }
        public string Constant { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryType { get; set; } = Delivery.Courier;
        public decimal Deposit { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public DateTime DueDate { get; set; } = DateTime.Now;
        public int? EstimateID { get; set; }
        public string HeaderComment { get; set; }
        public string InternalComment{ get; set; }
        public string InvoiceCurrency { get; set; } = Invoice.Currency.Euro;
        public string InvoiceNoFormatted { get; set; }
        public string IssuedBy { get; set; }
        public string IssuedByPhone { get; set; }
        public string IssuedByEmail { get; set; }
        public string Name { get; set; }
        public string PaymentType { get; set; } = Payment.BankTransfer;
        public int? ProformaId { get; set; }
        public string RoundingType { get; set; } = Rounding.Item;
        public string Specific { get; set; }
        public int? SequenceId { get; set; }
        public string InvoiceType { get; set; } = Type.Regular;
        public string Variable { get; set; }

    }
}
