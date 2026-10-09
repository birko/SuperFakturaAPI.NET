using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Birko.SuperFaktura.Request.Expense
{
    public class Filter: PagedSearchParameters
    {
        [JsonProperty(PropertyName = "modified")]
        public int? Modified { get; set; } = null;
        [JsonProperty(PropertyName = "modified_since")]
        public DateTime? ModifiedSince { get; set; } = null;
        [JsonProperty(PropertyName = "modified_to")]
        public DateTime? ModifiedTo { get; set; } = null;

        [JsonProperty(PropertyName = "amount_from")]
        public decimal? AmountFrom { get; set; } = null;
        [JsonProperty(PropertyName = "amount_to")]
        public decimal? AmountTo { get; set; } = null;
        [JsonProperty(PropertyName = "category")]
        public int? Category { get; set; } = null;
        [JsonProperty(PropertyName = "client_id")]
        public int? ClientId { get; set; } = null;
        [JsonProperty(PropertyName = "created")]
        public int? Created { get; set; } = null;
        [JsonProperty(PropertyName = "created_since")]
        public DateTime? CreatedSince { get; set; } = null;
        [JsonProperty(PropertyName = "created_to")]
        public DateTime? CreatedTo { get; set; } = null;
        [JsonProperty(PropertyName = "delivery")]
        public int? Delivery { get; set; } = null;
        [JsonProperty(PropertyName = "delivery_since")]
        public DateTime? DeliverySince { get; set; } = null;
        [JsonProperty(PropertyName = "delivery_to")]
        public DateTime? DeliveryTo { get; set; } = null;
        // Exact due date. Sent as due:3/due_since/due_to (inclusive) - "due:<date>" from the docs is ignored by the API.
        [JsonProperty(PropertyName = "due")]
        public DateTime? Due { get; set; } = null;
        // Time filter constant for the due date (as in the PHP client); sent as "due" in ToParameters.
        public int? DueFilter { get; set; } = null;
        [JsonProperty(PropertyName = "due_since")]
        public DateTime? DueSince { get; set; } = null;
        [JsonProperty(PropertyName = "due_to")]
        public DateTime? DueTo { get; set; } = null;
        [JsonProperty(PropertyName = "payment_type")]
        public string PaymentType { get; set; }
        // ValueLists.ExpenseStatus
        [JsonProperty(PropertyName = "status")]
        public int[] Status { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        public override string ToParameters(bool listInfo = true)
        {
            string paramString = base.ToParameters(listInfo);
            if (AmountFrom.HasValue)
            {
                paramString += "/amount_from:" + AmountFrom.Value.ToString(CultureInfo.InvariantCulture);
            }
            if (AmountTo.HasValue)
            {
                paramString += "/amount_to:" + AmountTo.Value.ToString(CultureInfo.InvariantCulture);
            }
            if (Category.HasValue)
            {
                paramString += "/category:" + Category;
            }            
            if (ClientId.HasValue)
            {
                paramString += "/client_id:" + ClientId;
            }
            // *_since / *_to require created:3, modified:3, delivery:3 (expenses.md).
            var created = Created ?? (CreatedSince.HasValue || CreatedTo.HasValue ? ValueLists.TimeFilterConstants.SinceTo : (int?)null);
            if (created.HasValue)
            {
                paramString += "/created:" + created;
            }
            var modified = Modified ?? (ModifiedSince.HasValue || ModifiedTo.HasValue ? ValueLists.TimeFilterConstants.SinceTo : (int?)null);
            if (modified.HasValue)
            {
                paramString += "/modified:" + modified;
            }
            if (ModifiedSince.HasValue)
            {
                paramString += "/modified_since:" + ModifiedSince.Value.ToString("yyyy-MM-dd");
            }
            if (ModifiedTo.HasValue)
            {
                paramString += "/modified_to:" + ModifiedTo.Value.ToString("yyyy-MM-dd");
            }
            if (CreatedSince.HasValue)
            {
                paramString += "/created_since:" + CreatedSince.Value.ToString("yyyy-MM-dd");
            }
            if (CreatedTo.HasValue)
            {
                paramString += "/created_to:" + CreatedTo.Value.ToString("yyyy-MM-dd");
            }
            var delivery = Delivery ?? (DeliverySince.HasValue || DeliveryTo.HasValue ? ValueLists.TimeFilterConstants.SinceTo : (int?)null);
            if (delivery.HasValue)
            {
                paramString += "/delivery:" + delivery;
            }
            if (DeliverySince.HasValue)
            {
                paramString += "/delivery_since:" + DeliverySince.Value.ToString("yyyy-MM-dd");
            }
            if (DeliveryTo.HasValue)
            {
                paramString += "/delivery_to:" + DeliveryTo.Value.ToString("yyyy-MM-dd");
            }
            var dueSince = DueSince ?? Due;
            var dueTo = DueTo ?? Due;
            var due = DueFilter ?? (dueSince.HasValue || dueTo.HasValue ? ValueLists.TimeFilterConstants.SinceTo : (int?)null);
            if (due.HasValue)
            {
                paramString += "/due:" + due;
            }
            if (dueSince.HasValue)
            {
                paramString += "/due_since:" + dueSince.Value.ToString("yyyy-MM-dd");
            }
            if (dueTo.HasValue)
            {
                paramString += "/due_to:" + dueTo.Value.ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrEmpty(PaymentType))
            {
                paramString += "/payment_type:" + PaymentType;
            }
            var status = JoinValues(Status);
            if (status != null)
            {
                paramString += "/status:" + status;
            }
            if (!string.IsNullOrEmpty(Type))
            {
                paramString += "/type:" + Type;
            }

            return paramString;
        }
    }
}
