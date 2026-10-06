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

        [JsonProperty(PropertyName = "0", NullValueHandling = NullValueHandling.Ignore)]
        public ItemFlags Flags { get; set; }
    }

    public class ItemFlags
    {
        // The item was cancelled by a reverse item.
        [JsonProperty(PropertyName = "has_storno", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? HasStorno { get; set; }
    }

    // cash_register_items/index/{ID}: paged items plus the cash register they belong to.
    public class ItemList : PagedResponse<Item>
    {
        [JsonProperty(PropertyName = "CashRegister", NullValueHandling = NullValueHandling.Ignore)]
        public CashRegister CashRegister { get; set; }
    }
}
