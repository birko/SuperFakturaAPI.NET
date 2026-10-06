using Birko.SuperFaktura.Request.Invoice;
using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Request.Client
{
    public class Filter: PagedSearchParameters
    {
        [JsonProperty(PropertyName = "created")]
        public int Created { get; set; } = ValueLists.TimeFilterConstants.All;
        [JsonProperty(PropertyName = "modified")]
        public int Modified { get; set; } = ValueLists.TimeFilterConstants.All;

        [JsonProperty(PropertyName = "created_since")]
        public DateTime? CreatedSince { get; set; } = null;
        [JsonProperty(PropertyName = "created_to")]
        public DateTime? CreatedTo { get; set; } = null;
        [JsonProperty(PropertyName = "modified_since")]
        public DateTime? ModifiedSince { get; set; } = null;
        [JsonProperty(PropertyName = "modified_to")]
        public DateTime? ModifiedTo { get; set; } = null;

        [JsonProperty(PropertyName = "price_from")]
        public decimal PriceFrom { get; set; } = 0;
        [JsonProperty(PropertyName = "price_to")]
        public decimal PriceTo { get; set; } = 0;

        [JsonProperty(PropertyName = "search_uuid")]
        public string SearchUUID { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "tag")]
        public int? Tag { get; set; }

        // First letter of client name; non-letter characters are under "#".
        [JsonProperty(PropertyName = "char_filter")]
        public string CharFilter { get; set; }

        public override string ToParameters(bool listInfo = true)
        {
            string paramString = base.ToParameters(listInfo);
            // *_since / *_to require created:3 / modified:3 (clients.md).
            var created = Created == ValueLists.TimeFilterConstants.All && (CreatedSince.HasValue || CreatedTo.HasValue) ? ValueLists.TimeFilterConstants.SinceTo : Created;
            var modified = Modified == ValueLists.TimeFilterConstants.All && (ModifiedSince.HasValue || ModifiedTo.HasValue) ? ValueLists.TimeFilterConstants.SinceTo : Modified;
            paramString += "/created:" + created;
            paramString += "/modified:" + modified;

            if (PriceFrom > 0)
            {
                paramString += "/price_from:" + PriceFrom;
            }
            if (PriceTo > 0)
            {
                paramString += "/price_to:" + PriceTo;
            }
            if (CreatedSince.HasValue)
            {
                paramString += "/created_since:" + CreatedSince.Value.ToString("yyyy-MM-dd");
            }
            if (CreatedTo.HasValue)
            {
                paramString += "/created_to:" + CreatedTo.Value.ToString("yyyy-MM-dd");
            }
            if (ModifiedSince.HasValue)
            {
                paramString += "/modified_since:" + ModifiedSince.Value.ToString("yyyy-MM-dd");
            }
            if (ModifiedTo.HasValue)
            {
                paramString += "/modified_to:" + ModifiedTo.Value.ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrEmpty(SearchUUID))
            {
                paramString += "/search_uuid:" + SearchUUID;
            }
            if (Tag.HasValue)
            {
                paramString += "/tag:" + Tag;
            }
            if (!string.IsNullOrEmpty(CharFilter))
            {
                paramString += "/char_filter:" + CharFilter;
            }

            return paramString;
        }
    }
}
