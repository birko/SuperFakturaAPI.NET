using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.CashRegister
{
    public class CashRegisterItemData : Data {
        public CashRegisterItem CashRegisterItem { get; set; }
    }

    public class CashRegisterItemBase
    {
        [JsonProperty(PropertyName = "amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "cash_register_id", NullValueHandling = NullValueHandling.Ignore)]
        public int CashRegisterID { get; set; }

        [JsonProperty(PropertyName = "client_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ClientID { get; set; }

        [JsonProperty(PropertyName = "client_name", NullValueHandling = NullValueHandling.Ignore)]
        public string ClientName { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Created { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "expense_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ExpenseID { get; set; }

        [JsonProperty(PropertyName = "invoice_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? InvoiceID { get; set; }
    }

    public class CashRegisterItem: CashRegisterItemBase
    {

        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "is_eet", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? IsEET { get; set; }
    }
}
