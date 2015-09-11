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
            public int Created { get; set; } = Invoice.Date.All;
            //Invoce.Date const
            public int Delivery { get; set; } = Invoice.Date.All;
            //Invoce.Typeconst
            public string Type { get; set; } = Invoice.Type.Regular;
            //Invoce.Delivery const
            public string DeliveryType { get; set; } = Invoice.Delivery.Mail;
            //Invoce.Paymenty const
            public string PaymentType { get; set; } = Invoice.Payment.BankTransfer;
            //Invoce.status const
            public int Status { get; set; } = Invoice.Status.All;
            public int? ClientId { get; set; } = 1;
            public decimal AmountFrom { get; set; } = 0;
            public decimal AmountTo { get; set; } = 0;
            public DateTime? PaidSince { get; set; } = null;
            public DateTime? PaidTo { get; set; } = null;
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
