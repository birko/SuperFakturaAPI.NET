using Raven.Imports.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Invoice
    {
        public class Filter: PagedSearchParams
        {
            //Invoce.Date const
            [JsonProperty(PropertyName = "created")]
            public int Created { get; set; } = Invoice.Date.All;
            //Invoce.Date const
            [JsonProperty(PropertyName = "delivery")]
            public int Delivery { get; set; } = Invoice.Date.All;
            //Invoce.Typeconst
            [JsonProperty(PropertyName = "type")]
            public string Type { get; set; } = Invoice.Type.Regular;
            //Invoce.Delivery const
            [JsonProperty(PropertyName = "delivery_type")]
            public string DeliveryType { get; set; } = Invoice.Delivery.Mail;
            //Invoce.Paymenty const
            [JsonProperty(PropertyName = "payment_type")]
            public string PaymentType { get; set; } = Invoice.Payment.BankTransfer;
            //Invoce.status const
            [JsonProperty(PropertyName = "status")]
            public int Status { get; set; } = Invoice.Status.All;
            [JsonProperty(PropertyName = "client_id")]
            public int? ClientId { get; set; } = 1;
            [JsonProperty(PropertyName = "amount_from")]
            public decimal AmountFrom { get; set; } = 0;
            [JsonProperty(PropertyName = "amount_since")]
            public decimal AmountTo { get; set; } = 0;
            [JsonProperty(PropertyName = "paid_since")]
            public DateTime? PaidSince { get; set; } = null;
            [JsonProperty(PropertyName = "paid_to")]
            public DateTime? PaidTo { get; set; } = null;
            [JsonProperty(PropertyName = "ignore")]
            public string Ignore { get; set; }

            public override string ToParams(bool listInfo = true)
            {
                string paramString = base.ToParams(listInfo);
                paramString += "/created:" + this.Created;
                paramString += "/delivery:" + this.Delivery;
                if (!string.IsNullOrEmpty(this.Type))
                {
                    paramString += "/type:" + this.Type;
                }
                if (!string.IsNullOrEmpty(this.DeliveryType))
                {
                    paramString += "/delivery_type:" + this.DeliveryType;
                }
                if (!string.IsNullOrEmpty(this.PaymentType))
                {
                    paramString += "/payment_type:" + this.PaymentType;
                }
                paramString += "/status:" + this.Status;
                if (this.ClientId.HasValue)
                {
                    paramString += "/client_id:" + this.ClientId;
                }
                if (this.AmountFrom > 0)
                {
                    paramString += "/amount_from:" + this.AmountFrom;
                }
                if (this.AmountTo > 0)
                {
                    paramString += "/amount_to:" + this.AmountTo;
                }
                if (this.PaidSince.HasValue)
                {
                    paramString += "/paid_since:" + this.PaidSince;
                }
                if (this.PaidTo.HasValue)
                {
                    paramString += "/paid_to:" + this.PaidTo;
                }
                if (!string.IsNullOrEmpty(this.Ignore))
                {
                    paramString += "/ignore:" + this.Ignore;
                }

                return paramString;
            }

        }
    }
}
