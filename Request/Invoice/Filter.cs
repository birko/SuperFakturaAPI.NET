using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class Filter : PagedSearchParameters
    {
        [JsonProperty(PropertyName = "amount_from")]
        public decimal? AmountFrom { get; set; } = null;

        [JsonProperty(PropertyName = "amount_to")]
        public decimal? AmountTo { get; set; } = null;

        [JsonProperty(PropertyName = "client_id")]
        public int? ClientId { get; set; } = null;

        [JsonProperty(PropertyName = "created_since")]
        public DateTime? CreatedSince { get; set; } = null;

        [JsonProperty(PropertyName = "created_to")]
        public DateTime? CreatedTo { get; set; } = null;

        //TimeFilterConstants const
        [JsonProperty(PropertyName = "created")]
        public int Created { get; set; } = ValueLists.TimeFilterConstants.All;

        [JsonProperty(PropertyName = "delivery")]
        public int Delivery { get; set; } = ValueLists.TimeFilterConstants.All;

        [JsonProperty(PropertyName = "delivery_since")]
        public DateTime? DeliverySince { get; set; } = null;

        [JsonProperty(PropertyName = "delivery_to")]
        public DateTime? DeliveryTo { get; set; } = null;

        //Delivery const
        [JsonProperty(PropertyName = "delivery_type")]
        public string DeliveryType { get; set; }

        [JsonProperty(PropertyName = "ignore")]
        public string Ignore { get; set; }

        [JsonProperty(PropertyName = "invoice_no_formatted")]
        public string InvoiceNumberFormatted { get; set; }

        [JsonProperty(PropertyName = "modified")]
        public int Modified { get; set; } = ValueLists.TimeFilterConstants.All;

        [JsonProperty(PropertyName = "modified_since")]
        public DateTime? ModifiedSince { get; set; } = null;

        [JsonProperty(PropertyName = "modified_to")]
        public DateTime? ModifiedTo { get; set; } = null;

        [JsonProperty(PropertyName = "order_no")]
        public string OrderNumber { get; set; }

        [JsonProperty(PropertyName = "paydate")]
        public int PayDate { get; set; } = ValueLists.TimeFilterConstants.All;

        [JsonProperty(PropertyName = "paydate_since")]
        public DateTime? PayDateSince { get; set; } = null;

        [JsonProperty(PropertyName = "paydate_to")]
        public DateTime? PayDateTo { get; set; } = null;

        //Invoce.Paymenty const
        [JsonProperty(PropertyName = "payment_type")]
        public string PaymentType { get; set; }

        //Invoce.status const
        [JsonProperty(PropertyName = "status")]
        public int? Status { get; set; } = null;

        [JsonProperty(PropertyName = "tag")]
        public int? Tag { get; set; } = null;

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "variable")]
        public string Variable { get; set; }

        public override string ToParameters(bool listInfo = true)
        {
            string paramString = base.ToParameters(listInfo);
            paramString += "/created:" + Created;
            paramString += "/modified:" + Modified;
            paramString += "/delivery:" + Delivery;
            paramString += "/paydate:" + PayDate;
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
            if (Status.HasValue)
            {
                paramString += "/status:" + Status;
            }
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
            if (PayDateSince.HasValue)
            {
                paramString += "/paydate_since:" + PayDateSince.Value.ToString("yyyy-MM-dd");
            }
            if (PayDateTo.HasValue)
            {
                paramString += "/paydate_to:" + PayDateTo.Value.ToString("yyyy-MM-dd");
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
            if (!string.IsNullOrEmpty(InvoiceNumberFormatted))
            {
                paramString += "/invoice_no_formatted:" + InvoiceNumberFormatted;
            }

            return paramString;
        }
    }
}
