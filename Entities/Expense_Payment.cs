using Raven.Imports.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Expense 
    {
        public class Payment
        {
            [JsonProperty(PropertyName = "expense_id")]
            public int ExpenseID { get; set; }
            // payment consts
            [JsonProperty(PropertyName = "type")]
            public string Type { get; set; } = Invoice.Payment.BankTransfer;
            [JsonProperty(PropertyName = "amount")]
            public decimal Amount { get; set; } = 0;
            [JsonProperty(PropertyName = "currency")]
            public string Currency { get; set; } = Invoice.Currency.Euro;
            [JsonProperty(PropertyName = "date")]
            public DateTime Date { get; set; }
        }
    }
}
