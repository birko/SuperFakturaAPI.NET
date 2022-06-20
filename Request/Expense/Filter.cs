using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Request.Expense
{
    public class Filter: SearchParameters
    {
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
        [JsonProperty(PropertyName = "due")]
        public DateTime? Due { get; set; } = null;
        [JsonProperty(PropertyName = "payment_type")]
        public string PaymentType { get; set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        public override string ToParameters(bool listInfo = true)
        {
            string paramString = base.ToParameters(listInfo);
            if (AmountFrom.HasValue)
            {
                paramString += "/amount_from:" + AmountFrom;
            }
            if (AmountTo.HasValue)
            {
                paramString += "/amount_to:" + AmountTo;
            }
            if (Category.HasValue)
            {
                paramString += "/category:" + Category;
            }            
            if (ClientId.HasValue)
            {
                paramString += "/client_id:" + ClientId;
            }
            if (Created.HasValue)
            {
                paramString += "/created:" + ClientId;
            }
            if (CreatedSince.HasValue)
            {
                paramString += "/created_since:" + CreatedSince.Value.ToString("yyyy-MM-dd");
            }
            if (CreatedTo.HasValue)
            {
                paramString += "/created_to:" + CreatedTo.Value.ToString("yyyy-MM-dd");
            }
            if (Delivery.HasValue)
            {
                paramString += "/delivery:" + Delivery;
            }
            if (DeliverySince.HasValue)
            {
                paramString += "/delivery_since:" + DeliverySince.Value.ToString("yyyy-MM-dd");
            }
            if (DeliveryTo.HasValue)
            {
                paramString += "/delivery_to:" + DeliveryTo.Value.ToString("yyyy-MM-dd");
            }
            if (Due.HasValue)
            {
                paramString += "/due:" + Due.Value.ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrEmpty(PaymentType))
            {
                paramString += "/payment_type:" + PaymentType;
            }
            if (!string.IsNullOrEmpty(Status))
            {
                paramString += "/status:" + Status;
            }
            if (!string.IsNullOrEmpty(Type))
            {
                paramString += "/type:" + Type;
            }

            return paramString;
        }
    }
}
