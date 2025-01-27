using Newtonsoft.Json;
using System.Dynamic;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class Item: Request.Invoice.Item
    {
        [JsonProperty(PropertyName = "discount_no_vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal DiscountNoVat { get; set; }

        [JsonProperty(PropertyName = "discount_no_vat_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal DiscountNoVatTotal { get; set; }

        [JsonProperty(PropertyName = "discount_with_vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal DiscountWithVat { get; set; }

        [JsonProperty(PropertyName = "discount_with_vat_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal DiscountWithVatTotal { get; set; }

        [JsonProperty(PropertyName = "hide_in_autocomplete", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HideInAutocomplete { get; set; }

        [JsonProperty(PropertyName = "ID", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }

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

        [JsonProperty(PropertyName = "tax_deposit", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.LowerBooleanConverter))]
        public bool? TaxDeposit { get; set; }

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
