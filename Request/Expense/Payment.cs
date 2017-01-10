using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.Expense
{
    public class Payment
    {

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; internal set; }
        [JsonProperty(PropertyName = "expense_id", NullValueHandling = NullValueHandling.Ignore)]
        public int ExpenseID { get; set; }
        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; } = Invoice.PaymentType.BankTransfer;
        [JsonProperty(PropertyName = "amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Amount { get; set; } = 0;
        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; } = Invoice.CurrencyType.Euro;
        [JsonProperty(PropertyName = "date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
