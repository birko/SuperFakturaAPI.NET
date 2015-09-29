using Raven.Imports.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura
{
    public class PageResponse<T>
    {
        [JsonProperty(PropertyName = "itemCount")]
        public int ItemCount { get; set; } = 0;
        [JsonProperty(PropertyName = "pageCount")]
        public int PageCount { get; set; } = 0;
        [JsonProperty(PropertyName = "perPage")]
        public int PerPage { get; set; } = 0;
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; } = 0;
        [JsonProperty(PropertyName = "Items")]
        public T[] Items { get; set; }
    }

    public class ClientItem
    {
        [JsonProperty(PropertyName = "Client")]
        public Entities.Client Client { get; set; } = null;
    }

    public class InvoiceItem : ClientItem
    {
        [JsonProperty(PropertyName = "Invoice")]
        public dynamic Invoice { get; set; } = null;
        [JsonProperty(PropertyName = "InvoicePayment")]
        public dynamic InvoicePayment { get; set; } = null;
        [JsonProperty(PropertyName = "InvoiceEmail")]
        public dynamic InvoiceEmail { get; set; } = null;
        [JsonProperty(PropertyName = "PostStamp")]
        public dynamic PostStamp { get; set; } = null;
    }
}
