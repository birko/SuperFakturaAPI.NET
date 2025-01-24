using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class Filter : PagedSearchParameters
    {
        //Invoce.Date const
        [JsonProperty(PropertyName = "created")]
        public int Created { get; set; } = ValueLists.TimeFilterConstants.All;
        [JsonProperty(PropertyName = "modified")]
        public int Modified { get; set; } = ValueLists.TimeFilterConstants.All;
        //Invoce.Date const
        [JsonProperty(PropertyName = "delivery")]
        public int Delivery { get; set; } = ValueLists.TimeFilterConstants.All;
        [JsonProperty(PropertyName = "paid")]
        public int Paid { get; set; } = ValueLists.TimeFilterConstants.All;
        //Invoce.Type const
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        //Invoce.Delivery const
        [JsonProperty(PropertyName = "delivery_type")]
        public string DeliveryType { get; set; }
        //Invoce.Paymenty const
        [JsonProperty(PropertyName = "payment_type")]
        public string PaymentType { get; set; }
        //Invoce.status const
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; } = ValueLists.StatusType.All;
        [JsonProperty(PropertyName = "client_id")]
        public int? ClientId { get; set; } = null;
        [JsonProperty(PropertyName = "amount_from")]
        public decimal? AmountFrom { get; set; } = null;
        [JsonProperty(PropertyName = "amount_to")]
        public decimal? AmountTo { get; set; } = null;
        [JsonProperty(PropertyName = "paid_since")]
        public DateTime? PaidSince { get; set; } = null;
        [JsonProperty(PropertyName = "paid_to")]
        public DateTime? PaidTo { get; set; } = null;
        [JsonProperty(PropertyName = "ignore")]
        public string Ignore { get; set; }
        [JsonProperty(PropertyName = "order_no")]
        public string OrderNumber{ get; set; }
        [JsonProperty(PropertyName = "created_since")]
        public DateTime? CreatedSince { get; set; } = null;
        [JsonProperty(PropertyName = "created_to")]
        public DateTime? CreatedTo { get; set; } = null;
        [JsonProperty(PropertyName = "modified_since")]
        public DateTime? ModifiedSince { get; set; } = null;
        [JsonProperty(PropertyName = "modified_to")]
        public DateTime? ModifiedTo { get; set; } = null;

        [JsonProperty(PropertyName = "delivery_since")]
        public DateTime? DeliverySince { get; set; } = null;
        [JsonProperty(PropertyName = "delivery_to")]
        public DateTime? DeliveryTo { get; set; } = null;
        [JsonProperty(PropertyName = "tag")]
        public int? Tag { get; set; } = null;
        [JsonProperty(PropertyName = "variable")]
        public string Variable { get; set; }

        public override string ToParameters(bool listInfo = true)
        {
            string paramString = base.ToParameters(listInfo);
            paramString += "/created:" + Created;
            paramString += "/modified:" + Modified;
            paramString += "/delivery:" + Delivery;
            paramString += "/paid:" + Paid;
            if (!string.IsNullOrEmpty(Type))
            {
                paramString += "/type:" + Type;
            }
            if (!string.IsNullOrEmpty(DeliveryType))
            {
                paramString += "/delivery_type:" + DeliveryType;
            }
            if (!string.IsNullOrEmpty(PaymentType))
            {
                paramString += "/payment_type:" + PaymentType;
            }
            paramString += "/status:" + Status;
            if (ClientId.HasValue)
            {
                paramString += "/client_id:" + ClientId;
            }
            if (AmountFrom.HasValue)
            {
                paramString += "/amount_from:" + AmountFrom;
            }
            if (AmountTo.HasValue)
            {
                paramString += "/amount_to:" + AmountTo;
            }
            if (!string.IsNullOrEmpty(Ignore))
            {
                paramString += "/ignore:" + Ignore;
            }
            if (!string.IsNullOrEmpty(OrderNumber))
            {
                paramString += "/order_no:" + OrderNumber;
            }
            if (PaidSince.HasValue)
            {
                paramString += "/paid_since:" + PaidSince.Value.ToString("yyyy-MM-dd");
            }
            if (PaidTo.HasValue)
            {
                paramString += "/paid_to:" + PaidTo.Value.ToString("yyyy-MM-dd");
            }
            if (CreatedSince.HasValue)
            {
                paramString += "/created_since:" + CreatedSince.Value.ToString("yyyy-MM-dd");
            }
            if (CreatedTo.HasValue)
            {
                paramString += "/created_to:" + CreatedTo.Value.ToString("yyyy-MM-dd");
            }
            if(ModifiedSince.HasValue)
            {
                paramString += "/modified_since:" + ModifiedSince.Value.ToString("yyyy-MM-dd");
            }
            if (ModifiedTo.HasValue)
            {
                paramString += "/modified_to:" + ModifiedTo.Value.ToString("yyyy-MM-dd");
            }

            if (DeliverySince.HasValue)
            {
                paramString += "/delivery_since:" + DeliverySince.Value.ToString("yyyy-MM-dd");
            }
            if (DeliveryTo.HasValue)
            {
                paramString += "/delivery_to:" + DeliveryTo.Value.ToString("yyyy-MM-dd");
            }
            if (Tag.HasValue)
            {
                paramString += "/tag:" + Tag;
            }
            if (!string.IsNullOrEmpty(Variable))
            {
                paramString += "/variable:" + Variable;
            }

            return paramString;
        }
    }
}
