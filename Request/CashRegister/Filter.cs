using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Request.CashRegister
{
    public class Filter: PagedSearchParameters
    {
        [JsonProperty(PropertyName = "datefilter")]
        public string DateFilter { get; set; } = null;
        [JsonProperty(PropertyName = "date_from")]
        public DateTime? DateFrom { get; set; } = null;
        [JsonProperty(PropertyName = "date_to")]
        public DateTime? DateTo { get; set; } = null;
        [JsonProperty(PropertyName = "sum_from")]
        public decimal? SumFrom { get; set; } = null;
        [JsonProperty(PropertyName = "sum_to")]
        public decimal? SumTo { get; set; } = null;
        [JsonProperty(PropertyName = "term")]
        public string Term { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        public override string ToParameters(bool listInfo = true)
        {
            string paramString = base.ToParameters(listInfo);
            paramString += "/datefilter:" + DateFilter;
            if (DateFrom.HasValue)
            {
                paramString += "/date_from:" + DateFrom;
            }
            if (DateTo.HasValue)
            {
                paramString += "/date_to:" + DateTo;
            }
            if (SumFrom.HasValue)
            {
                paramString += "/sum_from:" + SumFrom;
            }
            if (SumTo.HasValue)
            {
                paramString += "/sum_to:" + SumTo;
            }
            if (!string.IsNullOrEmpty(Term))
            {
                paramString += "/term:" + Term;
            }
            if (!string.IsNullOrEmpty(Type))
            {
                paramString += "/type:" + Type;
            }

            return paramString;
        }
    }
}
