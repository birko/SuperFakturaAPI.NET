using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.CashRegister
{
    public class Item
    {
        [JsonProperty(PropertyName = "Invoice", NullValueHandling = NullValueHandling.Ignore)]
        public Request.Invoice.InvoiceBasic Invoice { get; set; }

        [JsonProperty(PropertyName = "Expense", NullValueHandling = NullValueHandling.Ignore)]
        public Request.Expense.ExpenseBasic Expense { get; set; }

        [JsonProperty(PropertyName = "CashRegisterItem", NullValueHandling = NullValueHandling.Ignore)]
        public CashRegisterItemPaged CashRegisterItem { get; set; }

        [JsonProperty(PropertyName = "EetReceipt", NullValueHandling = NullValueHandling.Ignore)]
        public EetReceiptPaged EetReceipt { get; set; }
    }
}
