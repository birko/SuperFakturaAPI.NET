using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request.Client
{
    public class Filter: PagedSearchParameters
    {
        [JsonProperty(PropertyName = "price_from")]
        public decimal PriceFrom { get; set; } = 0;
        [JsonProperty(PropertyName = "price_to")]
        public decimal PriceTo { get; set; } = 0;

        public override string ToParameters(bool listInfo = true)
        {
            string paramString = base.ToParameters(listInfo);
            if (PriceFrom > 0)
            {
                paramString += "/price_from:" + PriceFrom;
            }
            if (PriceTo > 0)
            {
                paramString += "/price_to:" + PriceTo;
            }

            return paramString;
        }
    }
}
