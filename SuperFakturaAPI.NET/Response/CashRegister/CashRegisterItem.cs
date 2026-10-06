using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.CashRegister
{

    public class CashRegisterItemBase : Request.CashRegister.CashRegisterItemBase
    {
        [JsonProperty(PropertyName = "cash_item_no_formatted", NullValueHandling = NullValueHandling.Ignore)]
        public string CashItemNumberFormatted { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }
    }

    public class CashRegisterItemPaged : CashRegisterItemBase
    {
        [JsonProperty(PropertyName = "eet_receipt_id", NullValueHandling = NullValueHandling.Ignore)]
        public int EetReceiptID { get; set; }

        [JsonProperty(PropertyName = "invoice_payment_id", NullValueHandling = NullValueHandling.Ignore)]
        public int InvoicePaymentID { get; set; }
    }

    public class CashRegisterItem: CashRegisterItemBase
    {
        [JsonProperty(PropertyName = "amount_formatted", NullValueHandling = NullValueHandling.Ignore)]
        public string AmountFormatted { get; set; }

        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "cash_item_no", NullValueHandling = NullValueHandling.Ignore)]
        public string CashItemNumber { get; set; }
        [JsonProperty(PropertyName = "created_formatted", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedFormatted { get; set; }

        [JsonProperty(PropertyName = "exchange_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ExchangeRate { get; set; }

        [JsonProperty(PropertyName = "sequence_cnt", NullValueHandling = NullValueHandling.Ignore)]
        public int SequenceCount { get; set; }

        [JsonProperty(PropertyName = "sequence_id", NullValueHandling = NullValueHandling.Ignore)]
        public int SequenceID { get; set; }

        [JsonProperty(PropertyName = "sequencein_id", NullValueHandling = NullValueHandling.Ignore)]
        public int SequenceInID { get; set; }

        [JsonProperty(PropertyName = "sequenceout_id", NullValueHandling = NullValueHandling.Ignore)]
        public int SequenceOutID { get; set; }

        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public int Type { get; set; }

    }

}
