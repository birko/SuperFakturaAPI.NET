?﻿using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class DetailBasic
    {
        [JsonProperty(PropertyName = "Invoice", NullValueHandling = NullValueHandling.Ignore)]
        public Invoice Invoice { get; set; } = null;
        [JsonProperty(PropertyName = "Tag", NullValueHandling = NullValueHandling.Ignore)]
        public Tag[] Tag { get; set; } = null;
    }

    public class DetailData : DetailBasic
    {
        [JsonProperty(PropertyName = "ClientData", NullValueHandling = NullValueHandling.Ignore)]
        public ClientData ClientData { get; set; } = null;
        [JsonProperty(PropertyName = "InvoiceItem", NullValueHandling = NullValueHandling.Ignore)]
        public Item[] InvoiceItems { get; set; } = null;
        [JsonProperty(PropertyName = "MyData", NullValueHandling = NullValueHandling.Ignore)]
        public MyData MyData { get; set; } = null;
    }

    public class Detail: DetailData
    {
        [JsonProperty(PropertyName = "Client", NullValueHandling = NullValueHandling.Ignore)]
        public Client.Client Client { get; set; } = null;
        [JsonProperty(PropertyName = "InvoicePayment", NullValueHandling = NullValueHandling.Ignore)]
        public Payment[] InvoicePayment { get; set; } = null;
        [JsonProperty(PropertyName = "InvoiceEmail", NullValueHandling = NullValueHandling.Ignore)]
        public Email[] InvoiceEmail { get; set; } = null;
        [JsonProperty(PropertyName = "PostStamp", NullValueHandling = NullValueHandling.Ignore)]
        public List<PostStamp> PostStamp { get; set; } = null;
        [JsonProperty(PropertyName = "Summary", NullValueHandling = NullValueHandling.Ignore)]
        public Summary Summary { get; set; } = null;
        [JsonProperty(PropertyName = "SummaryInvoice", NullValueHandling = NullValueHandling.Ignore)]
        public SummaryInvoice SummaryInvoice { get; set; } = null;
        [JsonProperty(PropertyName = "UnitCount", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(DictionaryConverter<string, decimal?>))]
        public Dictionary<string, decimal?> UnitCount { get; set; } = null;
        [JsonProperty(PropertyName = "PaymentLink", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentLink { get; set; }
        [JsonProperty(PropertyName = "Paypal", NullValueHandling = NullValueHandling.Ignore)]
        public string PayPal { get; set; }
        [JsonProperty(PropertyName = "Logo", NullValueHandling = NullValueHandling.Ignore)]
        public Logo[] Logo { get; set; } = null;
        [JsonProperty(PropertyName = "Signature", NullValueHandling = NullValueHandling.Ignore)]
        public Signature Signature { get; set; } = null;
    }
}
