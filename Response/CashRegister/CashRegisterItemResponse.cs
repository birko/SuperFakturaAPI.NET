using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.CashRegister
{
    public class CashRegisterSummaryResponse
    {
        [JsonProperty(PropertyName = "Summary", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.ZeroObjectConverter))]
        public Summary Summary { get; set; }
    }

    public class CashRegisterItemResponse : CashRegisterSummaryResponse
    {
        [JsonProperty(PropertyName = "CashRegister", NullValueHandling = NullValueHandling.Ignore)]
        public CashRegister CashRegister { get; set; }

        [JsonProperty(PropertyName = "CashRegisterItem", NullValueHandling = NullValueHandling.Ignore)]
        public CashRegisterItem CashRegisterItem { get; set; }

        [JsonProperty(PropertyName = "Invoice", NullValueHandling = NullValueHandling.Ignore)]
        public Invoice.Invoice Invoice { get; set; }

        [JsonProperty(PropertyName = "Expense", NullValueHandling = NullValueHandling.Ignore)]
        public Expense.Expense Expense { get; set; }

        [JsonProperty(PropertyName = "EetReceipt", NullValueHandling = NullValueHandling.Ignore)]
        public EetReceipt EetReceipt { get; set; }

        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public int Status { get;  set; }
    }
}
