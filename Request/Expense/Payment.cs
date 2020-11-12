using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Request.Expense
{
    public class Payment
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; internal set; } = null;
        [JsonProperty(PropertyName = "expense_id", NullValueHandling = NullValueHandling.Ignore)]
        public int ExpenseID { get; set; }
        [JsonProperty(PropertyName = "amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Amount { get; set; } = 0;
        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; } = Invoice.CurrencyType.Euro;
        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Date { get; set; } = DateTime.Now;
        [JsonProperty(PropertyName = "payment_type", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentType { get; set; } = Invoice.PaymentType.BankTransfer;
        [JsonProperty(PropertyName = "cash_register_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? CashRegisterID { get; set; } = null;
    }
}
