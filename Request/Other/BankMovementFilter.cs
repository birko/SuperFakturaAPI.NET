using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request.Other
{
    public class BankMovementFilter: PagedSearchParameters
    {
        [JsonProperty(PropertyName = "amount_from")]
        public decimal? AmountFrom { get; set; }

        [JsonProperty(PropertyName = "amount_to")]
        public decimal? AmountTo { get; set; }

        [JsonProperty(PropertyName = "date")]
        public int? Date { get; set; }

        [JsonProperty(PropertyName = "date_since")]
        public decimal? DateSince { get; set; }

        [JsonProperty(PropertyName = "date_to")]
        public decimal? DateTo { get; set; }

        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool Status { get; set; }

        public override string ToParameters(bool listInfo = true)
        {
            string paramString = base.ToParameters(listInfo);
            if (AmountFrom != null)
            {
                paramString += "/amount_from:" + AmountFrom;
            }
            if (AmountTo > 0)
            {
                paramString += "/amount_to:" + AmountTo;
            }
            if (Date != null)
            {
                paramString += "/date:" + Date;
            }
            if (DateSince != null)
            {
                paramString += "/dateSinc:" + DateSince;
            }
            if (DateTo > 0)
            {
                paramString += "/date_to:" + DateTo;
            }
            if (Status)
            {
                paramString += "/status:1";
            }

            return paramString;
        }
    }
}
