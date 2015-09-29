using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    
    public partial class Invoice
    {
        public class Currency
        {
            public const string Euro = "EUR";
            public const string USDollar = "USD";
            public const string BritishPound = "GBP";
            public const string HungarianForint = "HUF";
            public const string CzechKoruna = "CZK";
            public const string PolishZloty = "PLN";
            public const string SwissFranc = "CHF";
            public const string RussianRuble = "RUB";
        }
        public class Rounding
        {
            public const string Document = "document";
            public const string Item = "item";
        }
        public class Date
        {
            public const int All = 0;
            public const int Today = 1;
            public const int Yesterday = 2;
            public const int ThisMonth = 4;
            public const int LastMonth = 5;
            public const int ThisQuarter = 8;
            public const int LastYear = 7;
            public const int ThisYear = 6;
            // from to values. must be used paramater CreatedSince and CreatedTo
            public const int Interval = 3;
        }
        public class Type
        {
            public const string Regular = "regular";
            public const string ProForma = "proforma";
            public const string Estimate = "estimate";
            public const string Cancel = "cancel";

            public static string[] Types = new[] { Regular, ProForma, Estimate, Cancel };
        }
        public class Delivery
        {
            public const string Mail = "mail";
            public const string Courier = "courier";
            public const string PersonalCollcection = "personal";
            public const string CargoTransport = "hailage";
        }
        public class Payment
        {
            public const string BankTransfer = "transfer";
            public const string Cash = "cash";
            public const string PayPal = "paypal";
            public const string TrustPay = "trustpay";
            public const string CreditCard = "credit";
            public const string DebitCarf = "debit";
            public const string CashOnDelivery = "cod";
            public const string MutalAccreditation = "accreditation";

            public int InvoiceID { get; set; }
            // payment consts
            public string Type { get; set; } = Payment.BankTransfer;
            public decimal Amount { get; set; } = 0;
            public string Currency { get; set; } = Invoice.Currency.Euro;
            public DateTime Date { get; set; }
        }

        public class Status
        {
            public const int All = 0;
            public const int WaitingForPayment = 1;
            public const int PartiallyPaid = 2;
            public const int Paid = 3;
            public const int Overdue = 99;
        }
    }
}
