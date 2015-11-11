using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Collections;
using Birko.SuperFaktura.Entities;

namespace Birko.SuperFaktura
{
    public class PageResponse<T>
    {
        [JsonProperty(PropertyName = "itemCount", NullValueHandling = NullValueHandling.Ignore)]
        public int ItemCount { get; set; } = 0;
        [JsonProperty(PropertyName = "pageCount", NullValueHandling = NullValueHandling.Ignore)]
        public int PageCount { get; set; } = 0;
        [JsonProperty(PropertyName = "perPage", NullValueHandling = NullValueHandling.Ignore)]
        public int PerPage { get; set; } = 0;
        [JsonProperty(PropertyName = "page", NullValueHandling = NullValueHandling.Ignore)]
        public int Page { get; set; } = 0;
        [JsonProperty(PropertyName = "items", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ResponseItemListConverter))]
        public ItemList<T> Items { get; set; }
    }

    [JsonArray]
    public class ItemList<T>: List<T>
    {
        [JsonProperty(PropertyName = "_InvoiceSettings", NullValueHandling = NullValueHandling.Ignore)]
        public ExpandoObject InvoiceSettings { get; set; } = null;
    }

    [JsonConverter(typeof(ResponseListItemConverter))]
    public class EmptyItem : List<ExpandoObject>
    {
    }
    [JsonConverter(typeof(ResponseListItemConverter))]
    public class ClientItem: EmptyItem
    {
        [JsonProperty(PropertyName = "Client", NullValueHandling = NullValueHandling.Ignore)]
        public Entities.Client Client { get; set; } = null;

        [JsonProperty(PropertyName = "ClientData", NullValueHandling = NullValueHandling.Ignore)]
        public ExpandoObject ClientData { get; set; } = null;
    }

    [JsonConverter(typeof(ResponseListItemConverter))]
    public class InvoiceItem: ClientItem
    {
        [JsonProperty(PropertyName = "Invoice", NullValueHandling = NullValueHandling.Ignore)]
        public Entities.Invoice Invoice { get; set; } = null;
        [JsonProperty(PropertyName = "InvoicePayment", NullValueHandling = NullValueHandling.Ignore)]
        public List<ExpandoObject> InvoicePayment { get; set; } = null;
        [JsonProperty(PropertyName = "InvoiceItem", NullValueHandling = NullValueHandling.Ignore)]
        public List<ExpandoObject> InvoiceItems { get; set; } = null;
        [JsonProperty(PropertyName = "InvoiceEmail", NullValueHandling = NullValueHandling.Ignore)]
        public List<ExpandoObject> InvoiceEmail { get; set; } = null;
        [JsonProperty(PropertyName = "PostStamp", NullValueHandling = NullValueHandling.Ignore)]
        public List<ExpandoObject> PostStamp { get; set; } = null;
        [JsonProperty(PropertyName = "_InvoiceSettings", NullValueHandling = NullValueHandling.Ignore)]
        public ExpandoObject InvoiceSettings { get; set; } = null;
        [JsonProperty(PropertyName = "MyData", NullValueHandling = NullValueHandling.Ignore)]
        public ExpandoObject MyData { get; set; } = null;
        [JsonProperty(PropertyName = "Summary", NullValueHandling = NullValueHandling.Ignore)]
        public ExpandoObject Summary { get; set; } = null;
        [JsonProperty(PropertyName = "SummaryInvoice", NullValueHandling = NullValueHandling.Ignore)]
        public ExpandoObject SummaryInvoice { get; set; } = null;
        [JsonProperty(PropertyName = "UnitCount", NullValueHandling = NullValueHandling.Ignore)]
        public ExpandoObject UnitCount { get; set; } = null;
        [JsonProperty(PropertyName = "PaymentLink", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentLink { get; set; }
        [JsonProperty(PropertyName = "Paypal", NullValueHandling = NullValueHandling.Ignore)]
        public string PayPal { get; set; }
        [JsonProperty(PropertyName = "Tag", NullValueHandling = NullValueHandling.Ignore)]
        public List<ExpandoObject> Tag { get; set; } = null;

    }
}
