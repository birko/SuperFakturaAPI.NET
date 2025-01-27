﻿using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class Payment: Request.Invoice.Payment
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; internal set; }

        [JsonProperty(PropertyName = "import_payment_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ImportPaymentID { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "document_no", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentNo { get; set; }

        [JsonProperty(PropertyName = "exchange_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ExchangeRate { get; set; }

        [JsonProperty(PropertyName = "home_currency", NullValueHandling = NullValueHandling.Ignore)]
        public string HomeCurrency { get; set; }

        [JsonProperty(PropertyName = "payment_currency", NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentCurrency { get; set; }

        [JsonProperty(PropertyName = "vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal VAT { get; set; }

        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public string UserID { get; set; }

        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public string UserProfileID { get; set; }
    }
}
