using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Birko.SuperFaktura.Request
{
    public class Payment
    {
        [JsonProperty(PropertyName = "amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Amount { get; set; } = 0;

        [JsonProperty(PropertyName = "cash_register_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? CashRegisterID { get; set; } = null;

        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; } = RegionInfo.CurrentRegion.ISOCurrencySymbol;

        [JsonProperty(PropertyName = "payment_type", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentType { get; set; } = ValueLists.PaymentType.BankTransfer;
    }
}
