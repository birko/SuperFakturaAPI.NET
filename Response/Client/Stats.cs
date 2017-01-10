using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Birko.SuperFaktura.Response.Client
{
    public class Stats
    {
        [JsonProperty(PropertyName = "ExpenseCount", NullValueHandling = NullValueHandling.Ignore)]
        public int ExpenseCount { get; set; }
        [JsonProperty(PropertyName = "ExpenseCountOverdue", NullValueHandling = NullValueHandling.Ignore)]
        public int ExpenseCountOverdue{ get; set; }
        [JsonProperty(PropertyName = "ExpenseOwesTotal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ExpenseOwesTotal { get; set; }
        [JsonProperty(PropertyName = "ExpensesPaid", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ExpensesPaid { get; set; }
        [JsonProperty(PropertyName = "ExpensesTotal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ExpensesTotal { get; set; }
        [JsonProperty(PropertyName = "InvoiceCount", NullValueHandling = NullValueHandling.Ignore)]
        public int InvoiceCount { get; set; }
        [JsonProperty(PropertyName = "InvoiceCountOverdue", NullValueHandling = NullValueHandling.Ignore)]
        public int InvoiceCountOverdue { get; set; }
        [JsonProperty(PropertyName = "InvoicedTotal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal InvoicedTotal { get; set; }
        [JsonProperty(PropertyName = "OwesTotal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal OwesTotal { get; set; }
        [JsonProperty(PropertyName = "PaidTotal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal PaidTotal { get; set; }
        [JsonProperty(PropertyName = "PayTime", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PayTime { get; set; }
    }
}
