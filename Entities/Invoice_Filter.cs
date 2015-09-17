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
                paramString += "/type:" + this.Type;
                paramString += "/delivery_type:" + this.DeliveryType;
                paramString += "/payment_type:" + this.PaymentType;
                paramString += "/status:" + this.Status;
                paramString += "/client_id:" + this.ClientId;
                paramString += "/amount_from:" + this.AmountFrom;
                paramString += "/amount_to:" + this.AmountTo;
                paramString += "/paid_since:" + this.PaidSince;
                paramString += "/paid_to:" + this.PaidTo;
                paramString += "/ignore:" + this.Ignore;

                return paramString;
            }

        }
    }
}
