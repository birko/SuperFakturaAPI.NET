using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class Payment
    {
        [JsonProperty(PropertyName = "invoice_id", NullValueHandling = NullValueHandling.Ignore)]
        public int InvoiceID { get; set; }

        [JsonProperty(PropertyName = "amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Amount { get; set; } = 0;

        [JsonProperty(PropertyName = "cash_register_id ", NullValueHandling = NullValueHandling.Ignore)]
        public int? CashRegisterID { get; set; } = null;

        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; } = RegionInfo.CurrentRegion.ISOCurrencySymbol;

        [JsonProperty(PropertyName = "date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Date { get; set; } = DateTime.Now;

        [JsonProperty(PropertyName = "document_no", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentNumber { get; set; } = ValueLists.PaymentType.BankTransfer;

        // payment consts
        [JsonProperty(PropertyName = "payment_type", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentType { get; set; } = ValueLists.PaymentType.BankTransfer;


    }
}
