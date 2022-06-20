using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class Export
    {
        [JsonProperty(PropertyName = "hide_pdf_payment_info")]
        public bool HidePdfPaymentInfo { get; set; }
        [JsonProperty(PropertyName = "hide_signature")]
        public bool HideSignature { get; set; }
        [JsonProperty(PropertyName = "invoices_pdf")]
        public bool InvoicesPDF { get; set; }
        [JsonProperty(PropertyName = "invoices_xls")]
        public bool InvoicesXLS { get; set; }
        [JsonProperty(PropertyName = "merge_pdf")]
        public bool MergePDF { get; set; }
        [JsonProperty(PropertyName = "pdf_lang_default")]
        public bool PDFLangDefault { get; set; }
        [JsonProperty(PropertyName = "pdf_sort_client")]
        public bool PDFSortClient { get; set; }
        [JsonProperty(PropertyName = "pdf_sort_date")]
        public bool PDFSortDate { get; set; }
        [JsonProperty(PropertyName = "is_msel")]
        public bool IsMsel { get; set; } = true;
    }
}
