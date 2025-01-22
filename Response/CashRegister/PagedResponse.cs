using Newtonsoft.Json;
using System.Dynamic;

namespace Birko.SuperFaktura.Response.CashRegister
{
    public class PagedResponse : PagedResponse<Item>
    {
        [JsonProperty(PropertyName = "CashRegister", NullValueHandling = NullValueHandling.Ignore)]
        public CashRegister CashRegister { get; set; }

        [JsonProperty(PropertyName = "items", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.ItemListConverter<Item>))]
        public override ItemList<Item> Items { get; set; }
    }

    public class Item
    {
        [JsonProperty(PropertyName = "Invoice", NullValueHandling = NullValueHandling.Ignore)]
        public Request.Invoice.InvoiceBasic Invoice { get; set; }

        [JsonProperty(PropertyName = "Expense", NullValueHandling = NullValueHandling.Ignore)]
        public Request.Expense.ExpenseBasic Expense { get; set; }

        [JsonProperty(PropertyName = "CashRegisterItem", NullValueHandling = NullValueHandling.Ignore)]
        public CashRegisterItemPaged CashRegisterItem  { get; set; }

        [JsonProperty(PropertyName = "EetReceipt", NullValueHandling = NullValueHandling.Ignore)]
        public EetReceiptPaged EetReceipt { get; set; }

    }
}
