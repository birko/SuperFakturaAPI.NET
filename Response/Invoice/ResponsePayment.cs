using Birko.SuperFaktura.Request.Invoice;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class DeletePayment : ErrorResponse
    {
        [JsonProperty(PropertyName = "invoice_id", NullValueHandling = NullValueHandling.Ignore)]
        public int InvoiceID { get; set; }

        [JsonProperty(PropertyName = "paid", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Paid { get; set; }

        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "to_pay", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ToPay { get; set; }
    }

    public class ResponsePayment
    {
        [JsonProperty(PropertyName = "country_exchange_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal CountryExchangeRate { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; set; } = DateTime.Now;

        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; } = RegionInfo.CurrentRegion.ISOCurrencySymbol;

        [JsonProperty(PropertyName = "flash_message", NullValueHandling = NullValueHandling.Ignore)]
        public FlashMessage FlashMessage { get; set; }

        [JsonProperty(PropertyName = "home_currency", NullValueHandling = NullValueHandling.Ignore)]
        public string HommeCurrency { get; set; } = RegionInfo.CurrentRegion.ISOCurrencySymbol;

        [JsonProperty(PropertyName = "invoice_currency", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceCurrency { get; set; } = RegionInfo.CurrentRegion.ISOCurrencySymbol;

        [JsonProperty(PropertyName = "invoice_type", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceType { get; set; }

        [JsonProperty(PropertyName = "overdue", NullValueHandling = NullValueHandling.Ignore)]
        public bool Overdue { get; set; }

        [JsonProperty(PropertyName = "parent_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ParentID { get; set; }

        [JsonProperty(PropertyName = "payment_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? PaymentID { get;  set; }

        [JsonProperty(PropertyName = "to_pay_home_cur", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ToPayHomeCurrency { get; set; }
    }
}
