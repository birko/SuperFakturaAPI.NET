using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.CashRegister
{
    public class EetReceiptBasic
    {
        [JsonProperty(PropertyName = "celk_trzba", NullValueHandling = NullValueHandling.Ignore)]
        public decimal TotalRevenue { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "fik", NullValueHandling = NullValueHandling.Ignore)]
        public string FIK { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "pkp", NullValueHandling = NullValueHandling.Ignore)]
        public string PKP { get; set; }
    }

    public class EetReceiptPaged : EetReceiptBasic
    {
        [JsonProperty(PropertyName = "invoice_payment_id", NullValueHandling = NullValueHandling.Ignore)]
        public int InvoicePaymentID { get; set; }
    }

    public class EetReceipt: EetReceiptBasic
    {
        [JsonProperty(PropertyName = "CashRegister", NullValueHandling = NullValueHandling.Ignore)]
        public CashRegister CashRegister { get; set; }

        [JsonProperty(PropertyName = "EetCertificate", NullValueHandling = NullValueHandling.Ignore)]
        public EetCertificate EetCertificate { get; set; }

        [JsonProperty(PropertyName = "amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "bkp", NullValueHandling = NullValueHandling.Ignore)]
        public string BKP { get; set; }

        [JsonProperty(PropertyName = "cash_register_id", NullValueHandling = NullValueHandling.Ignore)]
        public int CashRegisterID { get; set; }

        [JsonProperty(PropertyName = "cerp_zuct", NullValueHandling = NullValueHandling.Ignore)]
        public decimal CerpZuct { get; set; }

        [JsonProperty(PropertyName = "cest_sluz", NullValueHandling = NullValueHandling.Ignore)]
        public decimal CestSluz { get; set; }

        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "dan1", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Tax1 { get; set; }

        [JsonProperty(PropertyName = "dan2", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Tax2 { get; set; }

        [JsonProperty(PropertyName = "dan3", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Tax3 { get; set; }

        [JsonProperty(PropertyName = "dat_prij", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime IncomeDate { get; set; }

        [JsonProperty(PropertyName = "dat_trzby", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime RevenueDate { get; set; }

        [JsonProperty(PropertyName = "dat_trzby_internal", NullValueHandling = NullValueHandling.Ignore)]
        public string RevenueDateInternal { get; set; }

        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public string Data { get; set; }

        [JsonProperty(PropertyName = "dic_popl", NullValueHandling = NullValueHandling.Ignore)]
        public string DICPopl { get; set; }

        [JsonProperty(PropertyName = "dic_poverujiciho", NullValueHandling = NullValueHandling.Ignore)]
        public string DICPoverujuceho { get; set; }

        [JsonProperty(PropertyName = "exchange_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ExchangeRate { get; set; }

        [JsonProperty(PropertyName = "id_pokl", NullValueHandling = NullValueHandling.Ignore)]
        public string PokladnaID { get; set; }

        [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Modified { get; set; }

        [JsonProperty(PropertyName = "porad_cis", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderNumber { get; set; }

        [JsonProperty(PropertyName = "pouzit_zboz1", NullValueHandling = NullValueHandling.Ignore)]
        public decimal pouzitZboz1 { get; set; }

        [JsonProperty(PropertyName = "pouzit_zboz2", NullValueHandling = NullValueHandling.Ignore)]
        public decimal pouzitZboz2 { get; set; }

        [JsonProperty(PropertyName = "pouzit_zboz3", NullValueHandling = NullValueHandling.Ignore)]
        public decimal pouzitZboz3 { get; set; }

        [JsonProperty(PropertyName = "rezim", NullValueHandling = NullValueHandling.Ignore)]
        public int Rezim { get; set; }

        [JsonProperty(PropertyName = "test", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? Test { get; set; }
        [JsonProperty(PropertyName = "upid", NullValueHandling = NullValueHandling.Ignore)]
        public string UPID { get; set; }

        [JsonProperty(PropertyName = "urceno_cerp_zuct", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? UrcenoCerpZuct { get; set; }

        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int UserID { get; set; }

        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int UserProfileId { get; set; }

        [JsonProperty(PropertyName = "upid", NullValueHandling = NullValueHandling.Ignore)]
        public string uuid_zpravy { get; set; }

        [JsonProperty(PropertyName = "zakl_dan1", NullValueHandling = NullValueHandling.Ignore)]
        public decimal BaseTax1 { get; set; }

        [JsonProperty(PropertyName = "zakl_dan2", NullValueHandling = NullValueHandling.Ignore)]
        public decimal BaseTax2 { get; set; }

        [JsonProperty(PropertyName = "zakl_dan3", NullValueHandling = NullValueHandling.Ignore)]
        public decimal BaseTax3 { get; set; }

        [JsonProperty(PropertyName = "zakl_nepodl_dph", NullValueHandling = NullValueHandling.Ignore)]
        public decimal BaseNotSubjectToTax { get; set; }
    }

    public class EetCertificate {
        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "dic", NullValueHandling = NullValueHandling.Ignore)]
        public string DIC { get; set; }

        [JsonProperty(PropertyName = "hash", NullValueHandling = NullValueHandling.Ignore)]
        public string Hash { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Modified { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "private_key", NullValueHandling = NullValueHandling.Ignore)]
        public string PrivateKey { get; set; }

        [JsonProperty(PropertyName = "public_key", NullValueHandling = NullValueHandling.Ignore)]
        public string PublicKey { get; set; }

        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int UserProfileID { get; set; }

        [JsonProperty(PropertyName = "valid_from", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ValidFrom { get; set; }

        [JsonProperty(PropertyName = "valid_to", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ValidTo { get; set; }
    }
}
