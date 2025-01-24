using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System;
using System.Dynamic;

namespace Birko.SuperFaktura.Response.Expense
{
    public class ExpenseItem : Request.Expense.ExpenseItem
    {
        [JsonProperty(PropertyName = "discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Discount { get; set; }

        [JsonProperty(PropertyName = "discount_description", NullValueHandling = NullValueHandling.Ignore)]
        public string DiscountDescription { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "ordernum", NullValueHandling = NullValueHandling.Ignore)]
        public int OrderNumber { get; set; }

        [JsonProperty(PropertyName = "quantity", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Quantity { get; set; }

        [JsonProperty(PropertyName = "stock_item_id", NullValueHandling = NullValueHandling.Ignore)]
        public int StockItemID { get; set; }

        [JsonProperty(PropertyName = "total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Total { get; set; }

        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "unit", NullValueHandling = NullValueHandling.Ignore)]
        public string Unit { get; set; }

        [JsonProperty(PropertyName = "unit_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal UnitTotal { get; set; }
    }
}
