using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Client
{
    public class Stat
    {
        [JsonProperty(PropertyName = "cancel_count", NullValueHandling = NullValueHandling.Ignore)]
        public int CancelCount { get; set; }

        [JsonProperty(PropertyName = "cancel_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal CancelTotal { get; set; }

        [JsonProperty(PropertyName = "client_id", NullValueHandling = NullValueHandling.Ignore)]
        public int ClientID { get; set; }

        [JsonProperty(PropertyName = "date_founded", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DateFounded { get; set; }

        [JsonProperty(PropertyName = "delivery_count", NullValueHandling = NullValueHandling.Ignore)]
        public int DeliveryCount { get; set; }

        [JsonProperty(PropertyName = "delivery_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal DeliveryTotal { get; set; }

        [JsonProperty(PropertyName = "estimate_count", NullValueHandling = NullValueHandling.Ignore)]
        public int EstimateCount { get; set; }

        [JsonProperty(PropertyName = "estimate_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal EstimateTotal { get; set; }

        [JsonProperty(PropertyName = "expense_count", NullValueHandling = NullValueHandling.Ignore)]
        public int ExpenseCount { get; set; }

        [JsonProperty(PropertyName = "expense_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ExpenseTotal { get; set; }

        [JsonProperty(PropertyName = "expense_unpaid_count", NullValueHandling = NullValueHandling.Ignore)]
        public int ExpenseUnpaidCount { get; set; }

        [JsonProperty(PropertyName = "expense_unpaid_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ExpenseUnpaidTotal { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "order_count", NullValueHandling = NullValueHandling.Ignore)]
        public int OrderCount { get; set; }

        [JsonProperty(PropertyName = "order_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal OrderTotal { get; set; }

        [JsonProperty(PropertyName = "pay_time", NullValueHandling = NullValueHandling.Ignore)]
        public decimal PayTime { get; set; }

        [JsonProperty(PropertyName = "proforma_count", NullValueHandling = NullValueHandling.Ignore)]
        public int ProformaCount { get; set; }

        [JsonProperty(PropertyName = "proforma_overdue_count", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ProformaOverdueCount { get; set; }

        [JsonProperty(PropertyName = "proforma_overdue_Total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ProformaOverdueTotal { get; set; }

        [JsonProperty(PropertyName = "proforma_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ProformaTotal { get; set; }

        [JsonProperty(PropertyName = "regular_count", NullValueHandling = NullValueHandling.Ignore)]
        public int RegularCount { get; set; }

        [JsonProperty(PropertyName = "regular_overdue_count", NullValueHandling = NullValueHandling.Ignore)]
        public decimal RegularOverdueCount { get; set; }

        [JsonProperty(PropertyName = "regular_overdue_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal RegularOverdueTotal { get; set; }

        [JsonProperty(PropertyName = "regular_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal RegularTotal { get; set; }

        [JsonProperty(PropertyName = "reverse_order_count", NullValueHandling = NullValueHandling.Ignore)]
        public int ReverseOrderCount { get; set; }

        [JsonProperty(PropertyName = "reverse_order_total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ReverseOrderTotal { get; set; }

        [JsonProperty(PropertyName = "risk", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Risk { get; set; }
    }
}
