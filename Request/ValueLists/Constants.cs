using System;
using System.Text.RegularExpressions;

namespace Birko.SuperFaktura.Request.ValueLists
{
    public static class AccountingDetailType
    {
        public const string Item = "item";
        public const string Service = "service";

        public static string[] Types = new[] { Item, Service };
    }

    public static class DeliveryType
    {
        public const string Courier = "courier";
        public const string Haulage = "haulage";
        public const string Mail = "mail";
        public const string Personal = "personal";
        public const string PickupPoint = "pickup_point";

        public static string[] Types = new[] { Courier, Haulage, Mail, Personal, PickupPoint };
    }

    public static class DocumenType
    {
        public const string Invoice = "invocie";
        public const string Expense = "expense";
    }

    public static class ExpenseStatus
    {
        public const int New = 1;
        public const int PartiallyPaid = 2;
        public const int Paid = 3;
        public const int Overdue = 99;

        public static int[] Statuses = new[] { New, PartiallyPaid, Paid, Overdue };
    }

    public static class ExpenseVersion
    {
        public const string Basic = "basic";
        public const string Items = "items";

        public static string[] ExpenseVersions = new[] { Basic, Items };
    }

    public static class ExpenseType
    {
        public const string Bill = "bill";
        public const string Contribution = "contribution";
        public const string Internal = "internal";
        public const string Invoice = "invoice";
        public const string NonDeductible = "nondeductible";
        public const string RecievedCreditNote = "recieved_credit_note";

        public static string[] Types = new[] { Bill, Contribution, Internal, Invoice, NonDeductible, RecievedCreditNote };
    }

    public static class ExportStatus
    {
        public const int Failed = 0;
        public const int Completed = 1;
        public const int InProgress = 2;
        public const int Scheduled = 3;

        public static int[] Statuses = new[] { Failed, Completed, InProgress, Scheduled };
    }

    public static class InvoiceType
    {
        public const string Cancel = "cancel";
        public const string Delivery = "delivery";
        public const string Draft = "draft";
        public const string Estimate = "estimate";
        public const string Order = "order";
        public const string ProForma = "proforma";
        public const string Regular = "regular";
        public const string ReverseOrder = "reverse_order";

        public static string[] Types = new[] { Cancel, Delivery, Draft, Estimate, Order, ProForma, Regular, ReverseOrder };
    }

    public static class InvoceStatus
    {
        public const int Issued = 1;
        public const int PartiallyPaid = 2;
        public const int Paid = 3;
        public const int Overdue = 99;

        public static int[] Statuses = new[] { Issued, PartiallyPaid, Paid, Overdue };
    }

    public static class LanguageType
    {
        public const string Czech = "cze";
        public const string German = "deu";
        public const string English = "eng";
        public const string Croatian = "hrv";
        public const string Hungarian = "hun";
        public const string Italian = "ita";
        public const string Dutch = "nld";
        public const string Polish = "pol";
        public const string Romanian = "rom";
        public const string Russian = "rus";
        public const string Slovak = "slo";
        public const string Slovenian = "slv";
        public const string Spanish = "spa";
        public const string Ukrainian = "ukr";

        public static string[] Languages
        {
            get
            {
                return new[] { Czech, German, English, Croatian, Hungarian, Italian, Dutch, Polish, Romanian, Russian, Slovak, Slovenian, Spanish, Ukrainian };
            }
        }
    }

    public static class PaymentType
    {
        public const string MutalAccreditation = "accreditation";
        public const string Barion = "barion";
        public const string Besteron = "besteron";
        public const string Cash = "cash";
        public const string Card = "card";
        public const string CashOnDelivery = "cod";
        public const string CreditCard = "credit";
        public const string DebitCard = "debit";
        public const string GoPay = "gopay";
        public const string Other = "other";
        public const string PayPal = "paypal";
        public const string BankTransfer = "transfer";
        public const string TrustPay = "trustpay";
        public const string Viamo = "viamo";

        public static string[] Types
        {
            get
            {
                return new[] { MutalAccreditation, Barion, Besteron, Cash, Card, CashOnDelivery, CreditCard, DebitCard, GoPay, Other, PayPal, BankTransfer, TrustPay, Viamo };
            }
        }
    }

    public static class PeriodTypes
    {
        public const string Daily = "daily";
        public const string Monthly = "monthly";
        public const string Yearly = "yearly";

        public static string[] Types
        {
            get
            {
                return new[] { Daily, Monthly, Yearly };
            }
        }
    }

    public static class RoundingType
    {
        public const string Document = "document";
        public const string Item = "item";
        public const string Retail = "item_ext";

        public static string[] Types
        {
            get
            {
                return new[] { Document, Item, Retail };
            }
        }
    }


    public static class TimeFilterConstants
    {
        public const int All = 0;
        public const int Today = 1;
        public const int Yesterday = 2;
        public const int SinceTo = 3;
        public const int ThisMonth = 4;
        public const int LastMonth = 5;
        public const int ThisYear = 6;
        public const int LastYear = 7;
        public const int ThisQuarter = 8;
        public const int ThisWeek = 9;
        public const int LastQuarter = 10;
        public const int LastHour = 11;
        public const int ThisHour = 12;

        public static int[] Filters
        {
            get
            {
                return new[] { All, Today, Yesterday, SinceTo, ThisMonth, LastMonth, ThisYear, LastYear, ThisQuarter, ThisWeek, LastQuarter, LastHour, ThisHour };
            }
        }
    }

    public static class TimeFilter
    {

        public const string Today = "today";
        public const string Yesterday = "yesterday";
        public const string ThisMonth = "thismonth";
        public const string ThisYear = "thisyear";
        public const string PrevMonth = "prevmonth";
        public const string PrevYear = "prevyear";

        public static string[] Filters
        {
            get
            {
                return new[] { Today, Yesterday, ThisMonth, ThisYear, PrevMonth, PrevYear };
            }
        }
    }

    [Obsolete("Not found in API documentation")]
    public static class EstimateStatus
    {
        public const int NotApproved = 1;
        public const int Approved = 2;
        public const int Rejected = 3;
    }
}
