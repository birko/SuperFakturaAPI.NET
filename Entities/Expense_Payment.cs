using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Expense 
    {
        public class Payment
        {
            public int ExpenseID { get; set; }
            // payment consts
            public string Type { get; set; } = Invoice.Payment.BankTransfer;
            public decimal Amount { get; set; } = 0;
            public string Currency { get; set; } = Invoice.Currency.Euro;
            public DateTime Date { get; set; }
        }
    }
}
