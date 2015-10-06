using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura
{
    public class Params
    {
        public virtual string ToParams(bool listInfo = true)
        {
            string paramString = string.Empty;
            if (listInfo)
            {
                paramString += "/listinfo:1";
            }

            return paramString;
        }
    }

    public class SearchParams : Params
    {
        [JsonProperty(PropertyName = "search")]
        public string Search { get; set; } = string.Empty;
        public override string ToParams(bool listInfo = true)
        {
            string paramString = base.ToParams(listInfo);
            if (!string.IsNullOrEmpty(this.Search))
            {
                paramString += "/search:" + Convert.ToBase64String(Encoding.UTF8.GetBytes(this.Search));
            }

            return paramString;
        }
    }

    public class PagedSearchParams : SearchParams
    {
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; } = 1;
        [JsonProperty(PropertyName = "per_page")]
        public int PerPage { get; set; } = 10;

        public override string ToParams(bool listInfo = true)
        {
            string paramString = base.ToParams(listInfo);
            if (this.Page > 0)
            {
                paramString += "/page:" + this.Page;
            }
            if (this.PerPage > 0)
            {
                paramString += "/per_page:" + this.PerPage;
            }

            return paramString;
        }
    }
}
