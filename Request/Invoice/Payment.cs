using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class Payment : Request.Payment
    {
        [JsonProperty(PropertyName = "invoice_id", NullValueHandling = NullValueHandling.Ignore)]
        public int InvoiceID { get; set; }

        [JsonProperty(PropertyName = "date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Date { get; set; } = DateTime.Now;

        [JsonProperty(PropertyName = "document_no", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentNumber { get; set; } = ValueLists.PaymentType.BankTransfer;
    }
}
