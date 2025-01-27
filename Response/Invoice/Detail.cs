using Birko.SuperFaktura.Converters;
using Birko.SuperFaktura.Request.Invoice;
using Birko.SuperFaktura.Response.ValueLists;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class DetailBasic
    {
        [JsonProperty(PropertyName = "Invoice", NullValueHandling = NullValueHandling.Ignore)]
        public Invoice Invoice { get; set; } = null;

        [JsonProperty(PropertyName = "Tag", NullValueHandling = NullValueHandling.Ignore)]
        public Tag[] Tag { get; set; } = null;
    }

    public class DetailInvoice : DetailBasic
    {
        [JsonProperty(PropertyName = "InvoiceItem", NullValueHandling = NullValueHandling.Ignore)]
        public Item[] InvoiceItems { get; set; } = null;

        [Obsolete("Not found in API documentation")]
        [JsonProperty(PropertyName = "ExpenseItem", NullValueHandling = NullValueHandling.Ignore)]
        public Response.Expense.ExpenseItem[] ExpenseItems { get; set; } = null;
    }

    public class DetailData : DetailInvoice
    {
        [JsonProperty(PropertyName = "ClientData", NullValueHandling = NullValueHandling.Ignore)]
        public ClientData ClientData { get; set; } = null;

        [JsonProperty(PropertyName = "MyData", NullValueHandling = NullValueHandling.Ignore)]
        public MyData MyData { get; set; } = null;
    }

    public class Detail: DetailData
    {
        [JsonProperty(PropertyName = "Client", NullValueHandling = NullValueHandling.Ignore)]
        public Client.Client Client { get; set; } = null;

        [JsonProperty(PropertyName = "InvoiceEmail", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Email> InvoiceEmail { get; set; } = null;

        [JsonProperty(PropertyName = "InvoiceExtra", NullValueHandling = NullValueHandling.Ignore)]
        public Request.Invoice.Extra InvoiceExtra { get; set; } = null;

        [JsonProperty(PropertyName = "InvoicePayment", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Payment> InvoicePayment { get; set; } = null;

        [JsonProperty(PropertyName = "InvoiceSetting", NullValueHandling = NullValueHandling.Ignore)]
        public InvoiceSettings InvoiceSetting { get; set; } = null;

        [JsonProperty(PropertyName = "Logo", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Logo> Logo { get; set; } = null;

        [JsonProperty(PropertyName = "PaymentLink", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentLink { get; set; }

        [JsonProperty(PropertyName = "Paypal", NullValueHandling = NullValueHandling.Ignore)]
        public bool PayPal { get; set; }

        [JsonProperty(PropertyName = "PostStamp", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<PostStamp> PostStamp { get; set; } = null;

        [JsonProperty(PropertyName = "RelatedItems", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<RelatedItem> RelatedItems { get; set; } = null;

        [JsonProperty(PropertyName = "qr", NullValueHandling = NullValueHandling.Ignore)]
        public QR QR { get; set; } = null;

        [JsonProperty(PropertyName = "Signature", NullValueHandling = NullValueHandling.Ignore)]
        public Signature SignatureRaw { get; set; } = null;

        [JsonProperty(PropertyName = "Summary", NullValueHandling = NullValueHandling.Ignore)]
        public Summary Summary { get; set; } = null;

        [JsonProperty(PropertyName = "SummaryInvoice", NullValueHandling = NullValueHandling.Ignore)]
        public SummaryInvoice SummaryInvoice { get; set; } = null;

        [JsonProperty(PropertyName = "UnitCount", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(DictionaryConverter<string, decimal?>))]
        public Dictionary<string, decimal?> UnitCount { get; set; } = null;
    }
}
