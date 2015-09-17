using Raven.Imports.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Expense
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; internal set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }
        [JsonProperty(PropertyName = "vat")]
        public decimal VAT { get; set; } = 20;
        [JsonProperty(PropertyName = "already_paid")]
        public bool AlreadyPaid { get; set; } = false;
        [JsonProperty(PropertyName = "created")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }
        [JsonProperty(PropertyName = "constant")]
        public string Constant { get; set; }
        [JsonProperty(PropertyName = "delivery")]
        public DateTime DeliveryDate { get; set; } = DateTime.Now;
        [JsonProperty(PropertyName = "due")]
        public DateTime DueDate { get; set; } = DateTime.Now;
        [JsonProperty(PropertyName = "Currency")]
        public string Currency { get; set; } = Invoice.Currency.Euro;
        [JsonProperty(PropertyName = "payment_type")]
        public string PaymentType { get; set; } = Invoice.Payment.BankTransfer;
        [JsonProperty(PropertyName = "specific")]
        public string Specific { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string ExpenseType { get; set; } = Type.Invoice;
        [JsonProperty(PropertyName = "variable")]
        public string Variable { get; set; }
    }
}
