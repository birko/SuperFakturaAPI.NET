using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class Item
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string ID { get; internal set; }
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Discount { get; set; } = 0;
        [JsonProperty(PropertyName = "discount_description", NullValueHandling = NullValueHandling.Ignore)]
        public string DiscountDescription { get; set; }
        [JsonProperty(PropertyName = "discount_no_vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal DiscountNoVat { get; set; }
        [JsonProperty(PropertyName = "discount_no_vat_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal DiscountNoVatTotal { get; set; }
        [JsonProperty(PropertyName = "discount_with_vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal DiscountWithVat { get; set; }
        [JsonProperty(PropertyName = "discount_with_vat_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal DiscountWithVatTotal { get; set; }
        public ExpandoObject HideInAutocomplete { get; set; }
        [JsonProperty(PropertyName = "invoice_id", NullValueHandling = NullValueHandling.Ignore)]
        public int InvoiceId { get; set; }
        [JsonProperty(PropertyName = "item_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ItemPrice { get; set; }
        [JsonProperty(PropertyName = "item_price_no_discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ItemPriceNoDiscount { get; set; }
        [JsonProperty(PropertyName = "item_price_vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ItemPriceVAT { get; set; }
        [JsonProperty(PropertyName = "item_price_vat_check", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ItemPriceVATCheck { get; set; }
        [JsonProperty(PropertyName = "item_price_vat_no_discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ItemPriceVATNoDiscount { get; set; }
        [JsonProperty(PropertyName = "ordernum", NullValueHandling = NullValueHandling.Ignore)]
        public int OrderNum { get; set; }
        [JsonProperty(PropertyName = "quantity", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Quantity { get; set; } = 1;
        [JsonProperty(PropertyName = "stock_item_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? StockItemID { get; set; } = null;
        [JsonProperty(PropertyName = "sku", NullValueHandling = NullValueHandling.Ignore)]
        public string SKU { get; set; }
        [JsonProperty(PropertyName = "tax", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Tax { get; set; } = 20;
        [JsonProperty(PropertyName = "tax_deposit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal TaxDeposit { get; set; } = 20;
        [JsonProperty(PropertyName = "unit", NullValueHandling = NullValueHandling.Ignore)]
        public string Unit { get; set; } = "ks";
        [JsonProperty(PropertyName = "unit_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal UnitPrice { get; set; }
        [JsonProperty(PropertyName = "unit_price_discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal UnitPriceDiscount { get; set; }
        [JsonProperty(PropertyName = "unit_price_vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal UnitPriceVAT { get; set; }
        [JsonProperty(PropertyName = "unit_price_vat_no_discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal UnitPriceVATNoDiscount { get; set; }
        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public string UserID { get; set; }
        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public string UserProfileID { get; set; }
    }
}
