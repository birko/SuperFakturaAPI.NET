using Newtonsoft.Json;
using System;
using System.Globalization;

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
        public DateTime? DateSince { get; set; }

        [JsonProperty(PropertyName = "date_to")]
        public DateTime? DateTo { get; set; }

        [JsonProperty(PropertyName = "move_type")]
        public string MoveType { get; set; }

        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool Status { get; set; }

        public override string ToParameters(bool listInfo = true)
        {
            string paramString = base.ToParameters(listInfo);
            if (AmountFrom != null)
            {
                paramString += "/amount_from:" + AmountFrom.Value.ToString(CultureInfo.InvariantCulture);
            }
            if (AmountTo != null)
            {
                paramString += "/amount_to:" + AmountTo.Value.ToString(CultureInfo.InvariantCulture);
            }
            // date_since / date_to require date:3 (other.md), so it is added when only the range is given.
            var date = Date ?? (DateSince != null || DateTo != null ? ValueLists.TimeFilterConstants.SinceTo : (int?)null);
            if (date != null)
            {
                paramString += "/date:" + date;
            }
            if (DateSince != null)
            {
                paramString += "/date_since:" + DateSince.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            if (DateTo != null)
            {
                paramString += "/date_to:" + DateTo.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(MoveType))
            {
                paramString += "/move_type:" + MoveType;
            }
            if (Status)
            {
                paramString += "/status:1";
            }

            return paramString;
        }
    }
}
