using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request
{
    public class Parameters
    {
        public virtual string ToParameters(bool listInfo = true)
        {
            string paramString = string.Empty;
            if (listInfo)
            {
                paramString += "/listinfo:1";
            }

            return paramString;
        }
    }

    public class SearchParameters : Parameters
    {
        [JsonProperty(PropertyName = "search")]
        public string Search { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "sku")]
        public string SKU { get; set; } = string.Empty;
        public override string ToParameters(bool listInfo = true)
        {
            string paramString = base.ToParameters(listInfo);
            if (!string.IsNullOrEmpty(Search))
            {
                paramString += "/search:" + Convert.ToBase64String(Encoding.UTF8.GetBytes(Search));
            }
            if (!string.IsNullOrEmpty(SKU))
            {
                paramString += "/sku:" + Convert.ToBase64String(Encoding.UTF8.GetBytes(SKU));
            }

            return paramString;
        }
    }

    public class PagedSearchParameters : SearchParameters
    {
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; } = 1;
        [JsonProperty(PropertyName = "per_page")]
        public int PerPage { get; set; } = 10;

        public override string ToParameters(bool listInfo = true)
        {
            string paramString = base.ToParameters(listInfo);
            if (Page > 0)
            {
                paramString += "/page:" + Page;
            }
            if (PerPage > 0)
            {
                paramString += "/per_page:" + PerPage;
            }

            return paramString;
        }
    }
}
