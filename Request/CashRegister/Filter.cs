using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Birko.SuperFaktura.Request.CashRegister
{
    public class Filter: PagedSearchParameters
    {
        // Cash register ID; part of the URL path (cash_register_items/index/{ID}), not a named parameter.
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

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
            if (!string.IsNullOrEmpty(DateFilter))
            {
                paramString += "/datefilter:" + DateFilter;
            }
            if (DateFrom.HasValue)
            {
                paramString += "/date_from:" + DateFrom.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            if (DateTo.HasValue)
            {
                paramString += "/date_to:" + DateTo.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            if (SumFrom.HasValue)
            {
                paramString += "/sum_from:" + SumFrom.Value.ToString(CultureInfo.InvariantCulture);
            }
            if (SumTo.HasValue)
            {
                paramString += "/sum_to:" + SumTo.Value.ToString(CultureInfo.InvariantCulture);
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
