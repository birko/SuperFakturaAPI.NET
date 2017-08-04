using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class Payment
    {
        [JsonProperty(PropertyName = "invoice_id", NullValueHandling = NullValueHandling.Ignore)]
        public int InvoiceID { get; set; }
        [JsonProperty(PropertyName = "amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Amount { get; set; } = 0;
        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; } = CurrencyType.Euro;
        [JsonProperty(PropertyName = "date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Date { get; set; } = DateTime.Now;
        // payment consts
        [JsonProperty(PropertyName = "payment_type", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentType { get; set; } = Request.Invoice.PaymentType.BankTransfer;
        [JsonProperty(PropertyName = "cash_register_id ", NullValueHandling = NullValueHandling.Ignore)]
        public int? CashRegisterID { get; set; } = null;
    }
}
