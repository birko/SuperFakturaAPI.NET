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
    }

    [JsonConverter(typeof(ResponseListItemConverter))]
    public class InvoiceItem: ClientItem
    {
        [JsonProperty(PropertyName = "Invoice", NullValueHandling = NullValueHandling.Ignore)]
        public Entities.Invoice Invoice { get; set; } = null;
        [JsonProperty(PropertyName = "InvoicePayment", NullValueHandling = NullValueHandling.Ignore)]
        public ExpandoObject InvoicePayment { get; set; } = null;
        [JsonProperty(PropertyName = "InvoiceItem", NullValueHandling = NullValueHandling.Ignore)]
        public List<ExpandoObject> InvoiceItems { get; set; } = null;
        [JsonProperty(PropertyName = "InvoiceEmail", NullValueHandling = NullValueHandling.Ignore)]
        public ExpandoObject InvoiceEmail { get; set; } = null;
        [JsonProperty(PropertyName = "PostStamp", NullValueHandling = NullValueHandling.Ignore)]
        public ExpandoObject PostStamp { get; set; } = null;
        [JsonProperty(PropertyName = "_InvoiceSettings", NullValueHandling = NullValueHandling.Ignore)]
        public ExpandoObject InvoiceSettings { get; set; } = null;
    }
}
