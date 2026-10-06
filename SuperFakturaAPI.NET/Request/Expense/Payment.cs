using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Birko.SuperFaktura.Request.Expense
{
    public class Payment : Request.Payment
    {
        [JsonProperty(PropertyName = "expense_id", NullValueHandling = NullValueHandling.Ignore)]
        public int ExpenseID { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Date { get; set; } = DateTime.Now;

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; internal set; } = null;
    }
}
