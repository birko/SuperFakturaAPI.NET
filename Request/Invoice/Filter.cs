using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class Filter : PagedSearchParameters
    {
        //Invoce.Date const
        [JsonProperty(PropertyName = "created")]
        public int Created { get; set; } = DateType.All;
        //Invoce.Date const
        [JsonProperty(PropertyName = "delivery")]
        public int Delivery { get; set; } = DateType.All;
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
        public int Status { get; set; } = StatusType.All;
        [JsonProperty(PropertyName = "client_id")]
        public int? ClientId { get; set; } = null;
        [JsonProperty(PropertyName = "amount_from")]
        public decimal? AmountFrom { get; set; } = null;
        [JsonProperty(PropertyName = "amount_since")]
        public decimal? AmountTo { get; set; } = null;
        [JsonProperty(PropertyName = "paid_since")]
        public DateTime? PaidSince { get; set; } = null;
        [JsonProperty(PropertyName = "paid_to")]
        public DateTime? PaidTo { get; set; } = null;
        [JsonProperty(PropertyName = "ignore")]
        public string Ignore { get; set; }
        [JsonProperty(PropertyName = "order_no")]
        public string OrderNumber{ get; set; }

        public override string ToParameters(bool listInfo = true)
        {
            string paramString = base.ToParameters(listInfo);
            paramString += "/created:" + Created;
            paramString += "/delivery:" + Delivery;
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
            if (PaidSince.HasValue)
            {
                paramString += "/paid_since:" + PaidSince;
            }
            if (PaidTo.HasValue)
            {
                paramString += "/paid_to:" + PaidTo;
            }
            if (!string.IsNullOrEmpty(Ignore))
            {
                paramString += "/ignore:" + Ignore;
            }
            if (!string.IsNullOrEmpty(OrderNumber))
            {
                paramString += "/order_no:" + OrderNumber;
            }

            return paramString;
        }

    }
}
