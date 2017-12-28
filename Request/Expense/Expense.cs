using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.Expense
{
    public class Expense
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; internal set; }
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "already_paid", NullValueHandling = NullValueHandling.Ignore)]
        public bool AlreadyPaid { get; set; } = false;
        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; set; } = DateTime.Now;
        [JsonProperty(PropertyName = "amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Amount { get; set; }
        [JsonProperty(PropertyName = "expense_category_id", NullValueHandling = NullValueHandling.Ignore)]
        public int ExpenseCategoryID { get; internal set; }
        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string ExpenseType { get; set; } = Birko.SuperFaktura.Request.Expense.Type.Invoice;
        [JsonProperty(PropertyName = "specific", NullValueHandling = NullValueHandling.Ignore)]
        public string Specific { get; set; }
        [JsonProperty(PropertyName = "variable", NullValueHandling = NullValueHandling.Ignore)]
        public string Variable { get; set; }
        [JsonProperty(PropertyName = "comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }
        [JsonProperty(PropertyName = "constant", NullValueHandling = NullValueHandling.Ignore)]
        public string Constant { get; set; }
        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; } = Birko.SuperFaktura.Request.Invoice.CurrencyType.Euro;
        [JsonProperty(PropertyName = "delivery", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Delivery { get; set; } = DateTime.Now;
        [JsonProperty(PropertyName = "due", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Due { get; set; } = DateTime.Now;
        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)]
        public string Tags { get; set; }
        [JsonProperty(PropertyName = "client_id", NullValueHandling = NullValueHandling.Ignore)]
        public int ClientID { get; internal set; }
        [JsonProperty(PropertyName = "document_number", NullValueHandling = NullValueHandling.Ignore)]
        public int DocumentNumber { get; internal set; }
    }
}
