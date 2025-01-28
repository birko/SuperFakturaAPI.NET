using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.Export
{
    public class ExportData : Data
    {
        [JsonProperty(PropertyName = "Invoice", NullValueHandling = NullValueHandling.Ignore)]
        public Invoice Invoice { get; set; }

        [JsonProperty(PropertyName = "Export", NullValueHandling = NullValueHandling.Ignore)]
        public Export Export { get; set; }
    }

    public class Invoice
    {
        [JsonProperty(PropertyName = "ids", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<int> IDS { get; set; }
    }

    public class Export
    {
        [JsonProperty(PropertyName = "is_msel", NullValueHandling = NullValueHandling.Ignore)]
        public bool MultiSelect { get; set; } = true;

        [JsonProperty(PropertyName = "hide_pdf_payment_info", NullValueHandling = NullValueHandling.Ignore)]
        public bool HidePDFPpaymentInfo { get; set; }

        [JsonProperty(PropertyName = "hide_signature", NullValueHandling = NullValueHandling.Ignore)]
        public bool HideSignature { get; set; }

        [JsonProperty(PropertyName = "invoices_pdf", NullValueHandling = NullValueHandling.Ignore)]
        public bool InvoicesPDF { get; set; }

        [JsonProperty(PropertyName = "invoices_xls", NullValueHandling = NullValueHandling.Ignore)]
        public bool InvoicesXLS { get; set; }

        [JsonProperty(PropertyName = "merge_pdf", NullValueHandling = NullValueHandling.Ignore)]
        public bool MergePDF { get; set; }

        [JsonProperty(PropertyName = "pdf_lang_default", NullValueHandling = NullValueHandling.Ignore)]
        public bool PDFLangDefault { get; set; }

        [JsonProperty(PropertyName = "pdf_sort_client", NullValueHandling = NullValueHandling.Ignore)]
        public bool PDFSortClient { get; set; }

        [JsonProperty(PropertyName = "pdf_sort_date", NullValueHandling = NullValueHandling.Ignore)]
        public bool PDFSortDate { get; set; }

        [JsonProperty(PropertyName = "only_merge", NullValueHandling = NullValueHandling.Ignore)]
        public bool OnlyMerge { get; set; }
    }
}
