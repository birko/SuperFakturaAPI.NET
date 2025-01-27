using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Other
{
    public class UserProfile : Response.UserProfile
    {

        [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Modified { get; set; }

        [JsonProperty(PropertyName = "phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "web", NullValueHandling = NullValueHandling.Ignore)]
        public string Web { get; set; }
    }

    public class AccountUserProfile : UserProfile
    {
        [JsonProperty(PropertyName = "affiliate_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? AffiliateID { get; set; }

        [JsonProperty(PropertyName = "bysquare", NullValueHandling = NullValueHandling.Ignore)]
        public bool BySquare { get; set; }

        [JsonProperty(PropertyName = "date_mask", NullValueHandling = NullValueHandling.Ignore)]
        public string DatetMask { get; set; }

        [JsonProperty(PropertyName = "default_constant", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultConstant { get; set; }

        [JsonProperty(PropertyName = "default_delivery", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultDelivery { get; set; }

        [JsonProperty(PropertyName = "default_payment_type", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultPaymentType { get; set; }

        [JsonProperty(PropertyName = "delete_status", NullValueHandling = NullValueHandling.Ignore)]
        public string DeleteStatus { get; set; }

        [JsonProperty(PropertyName = "disable_footer", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DisableFooter { get; set; }

        [JsonProperty(PropertyName = "due_warning_subject", NullValueHandling = NullValueHandling.Ignore)]
        public string DueWarningSubject { get; set; }

        [JsonProperty(PropertyName = "due_warning_template", NullValueHandling = NullValueHandling.Ignore)]
        public string DueWarningTemplate { get; set; }

        [JsonProperty(PropertyName = "expense_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ExpenseRate { get; set; }

        [JsonProperty(PropertyName = "header_logo", NullValueHandling = NullValueHandling.Ignore)]
        public bool HeaderLogo { get; set; }

        [JsonProperty(PropertyName = "help", NullValueHandling = NullValueHandling.Ignore)]
        public string Help { get; set; }

        [JsonProperty(PropertyName = "home_currency", NullValueHandling = NullValueHandling.Ignore)]
        public string HomeCurrency { get; set; }

        [JsonProperty(PropertyName = "ico_raw", NullValueHandling = NullValueHandling.Ignore)]
        public string CINRaw { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "invoice_comment", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceComment { get; set; }

        [JsonProperty(PropertyName = "invoice_email_subject", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceEmailSubject { get; set; }

        [JsonProperty(PropertyName = "invoice_email_template", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceEmailTemplate { get; set; }

        [JsonProperty(PropertyName = "invoice_header_comment", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceHeaderComment { get; set; }

        [JsonProperty(PropertyName = "invoice_item_limit", NullValueHandling = NullValueHandling.Ignore)]
        public int InvoiceItemLimit { get; set; }

        [JsonProperty(PropertyName = "invoice_items_name", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceItemsName { get; set; }

        [JsonProperty(PropertyName = "invoice_no_mask", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceNumberMask { get; set; }

        [JsonProperty(PropertyName = "invoice_sequence", NullValueHandling = NullValueHandling.Ignore)]
        public int InvoiceSequence { get; set; }

        [JsonProperty(PropertyName = "invoice_sequence_type", NullValueHandling = NullValueHandling.Ignore)]
        public string InvoiceSequenceType { get; set; }

        [JsonProperty(PropertyName = "items_per_page", NullValueHandling = NullValueHandling.Ignore)]
        public int ItemsPerPage { get; set; }

        [JsonProperty(PropertyName = "last_login", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime LastLogin { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "newsletter", NullValueHandling = NullValueHandling.Ignore)]
        public string Newsletter { get; set; }

        [JsonProperty(PropertyName = "online_payments", NullValueHandling = NullValueHandling.Ignore)]
        public bool OnlinePayments { get; set; }

        [JsonProperty(PropertyName = "payment_thankyou_subject", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentThankYouSubject { get; set; }

        [JsonProperty(PropertyName = "payment_thankyou_template", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentThankYouTemplate { get; set; }

        [JsonProperty(PropertyName = "rounding", NullValueHandling = NullValueHandling.Ignore)]
        public string Rounding { get; set; }

        [JsonProperty(PropertyName = "setup_finished", NullValueHandling = NullValueHandling.Ignore)]
        public bool SetupFinished { get; set; }

        [JsonProperty(PropertyName = "tax_base", NullValueHandling = NullValueHandling.Ignore)]
        public decimal TaxBase { get; set; }

        [JsonProperty(PropertyName = "update_taxdate", NullValueHandling = NullValueHandling.Ignore)]
        public bool UpdateTaxDate { get; set; }

        [JsonProperty(PropertyName = "vat_interval", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? VATInternal { get; set; }
    }
}
