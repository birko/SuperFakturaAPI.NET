using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Response.Stock
{
    public class LogResponse
    {
        [JsonProperty(PropertyName = "StockLog", NullValueHandling = NullValueHandling.Ignore)]
        public Log[] StockLog { get; internal set; }
    }

    public class Log : Request.Stock.Log
    {
        [JsonProperty(PropertyName = "log_data", NullValueHandling = NullValueHandling.Ignore)]
        public string LogData { get; internal set; }

        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserID { get; set; }

        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserProfileID { get; set; }

        [JsonProperty(PropertyName = "invoice_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? InvoiceID { get; set; }

        [JsonProperty(PropertyName = "expense_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ExpenseID { get; set; }

        [JsonProperty(PropertyName = "document_item_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? DocumentItemID { get; set; }

        // Type of the document that moved the stock (e.g. invoice), null for manual movements.
        [JsonProperty(PropertyName = "document", NullValueHandling = NullValueHandling.Ignore)]
        public string Document { get; set; }

        [JsonProperty(PropertyName = "document_subtype", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentSubtype { get; set; }

        [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Modified { get; set; }
    }

    // One row of stock_items/movements/{ID}.
    public class LogItem
    {
        [JsonProperty(PropertyName = "StockLog", NullValueHandling = NullValueHandling.Ignore)]
        public Log StockLog { get; set; }
    }
}
