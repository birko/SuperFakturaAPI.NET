using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.Invoice
{
    public static class LanguageType
    {
        public const string Slovak = "slo";
        public const string Czech = "cze";
        public const string English = "eng";
        public const string German = "deu";
        public const string Russian = "rus";
        public const string Ukrainian = "ukr";
        public const string Hungarian = "hun";
        public const string Polish  = "pol";
        public const string Romanian = "rom";
        public const string Croatian = "hrv";
        public const string Slovenian = "slv";

        public static string[] Languages
        {
            get
            {
                return new[] { Slovak, Czech, English, German, Russian, Ukrainian, Hungarian, Polish, Romanian, Croatian, Slovenian };
            }
        }
    }

    public static class CurrencyType
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

    public static class RoundingType
    {
        public const string Document = "document";
        public const string Item = "item";
        public const string Retail = "item_ext";
    }

    public static class DateType
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

    public static class Type
    {
        public const string Regular = "regular";
        public const string ProForma = "proforma";
        public const string Estimate = "estimate";
        public const string Cancel = "cancel";
        public const string Order = "order";
        public const string Delivery = "delivery";

        public static string[] Types = new[] { Regular, ProForma, Estimate, Cancel, Order, Delivery };
    }

    public static class DeliveryType
    {
        public const string Mail = "mail";
        public const string Courier = "courier";
        public const string Personal = "personal";
        public const string Haulage = "haulage";
        public const string PickupPoint = "pickup_point";
    }

    public static class PaymentType
    {
        public const string BankTransfer = "transfer";
        public const string Cash = "cash";
        public const string PayPal = "paypal";
        public const string TrustPay = "trustpay";
        public const string CreditCard = "credit";
        public const string DebitCard = "debit";
        public const string CashOnDelivery = "cod";
        public const string MutalAccreditation = "accreditation";
    }

    public static class StatusType
    {
        public const int All = 0;
        public const int WaitingForPayment = 1;
        public const int PartiallyPaid = 2;
        public const int Paid = 3;
        public const int Overdue = 99;
    }

    public static class AccountingDetailType
    {
        public const string Item = "item";
        public const string Service = "service";
    }

    public static class EstimateStatus
    {
        public const int NotApproved = 1;
        public const int Approved = 2;
        public const int Rejected = 3;
    }
}
