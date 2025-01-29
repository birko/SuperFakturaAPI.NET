using Newtonsoft.Json;
using System;
using System.Text;

namespace Birko.SuperFaktura.Request
{
    public class Parameters
    {
        public const string Direction_ASC = "ASC";
        public const string Direction_DESC = "DESC";

        [JsonProperty(PropertyName = "direction")]
        public string Direction { get; set; } = Direction_DESC;

        [JsonProperty(PropertyName = "sort")]
        public string Sort { get; set; }

        public virtual string ToParameters(bool listInfo = true)
        {
            string paramString = string.Empty;
            if (listInfo)
            {
                paramString += "/listinfo:1";
            }
            if (!string.IsNullOrEmpty(Direction))
            {
                paramString += "/direction:" + Direction;
            }
            if (!string.IsNullOrEmpty(Sort))
            {
                paramString += "/sort:" + Sort;
            }
            return paramString;
        }
    }

    public class SearchParameters : Parameters
    {
        [JsonProperty(PropertyName = "search")]
        public string Search { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "sku")]
        //nepotrebuje clients
        public string SKU { get; set; } = string.Empty;

        public override string ToParameters(bool listInfo = true)
        {
            string paramString = base.ToParameters(listInfo);
            if (!string.IsNullOrEmpty(Search))
            {
                paramString += "/search:" + Convert.ToBase64String(Encoding.UTF8.GetBytes(Search))?.Replace("+", "-")?.Replace("/", "_")?.Replace("=", ",");
            }

            if (!string.IsNullOrEmpty(SKU))
            {
                paramString += "/sku:" + Convert.ToBase64String(Encoding.UTF8.GetBytes(SKU))?.Replace("+", "-")?.Replace("/", "_")?.Replace("=", ",");
            }

            return paramString;
        }
    }

    public class PagedParameters : Parameters
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
                //Limit
                if (PerPage > 200)
                {
                    PerPage = 200;
                }
                paramString += "/per_page:" + PerPage;
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
                //Limit
                if (PerPage > 200)
                {
                    PerPage = 200;
                }
                paramString += "/per_page:" + PerPage;
            }

            return paramString;
        }
    }
}
