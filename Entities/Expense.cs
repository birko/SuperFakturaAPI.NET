using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Expense
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal VAT { get; set; } = 20;
        public bool AlreadyPaid { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Comment { get; set; }
        public string Constant { get; set; }
        public DateTime DeliveryDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now;
        public string Currency { get; set; } = Invoice.Currency.Euro;
        public string PaymentType { get; set; } = Invoice.Payment.BankTransfer;
        public string Specific { get; set; }
        public string ExpenseType { get; set; } = Type.Invoice;
        public string Variable { get; set; }
    }
}
