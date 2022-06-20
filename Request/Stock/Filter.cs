using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request.Stock
{
    public class Filter: PagedSearchParameters
    {
        [JsonProperty(PropertyName = "price_from")]
        public decimal? PriceFrom { get; set; }
        [JsonProperty(PropertyName = "price_to")]
        public decimal? PriceTo { get; set; }

        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool Status { get; set; }

        public override string ToParameters(bool listInfo = true)
        {
            string paramString = base.ToParameters(listInfo);
            if (PriceFrom != null)
            {
                paramString += "/price_from:" + PriceFrom;
            }
            if (PriceTo > 0)
            {
                paramString += "/price_To:" + PriceTo;
            }
            if (Status)
            {
                paramString += "/status:1";
            }

            return paramString;
        }
    }
}
