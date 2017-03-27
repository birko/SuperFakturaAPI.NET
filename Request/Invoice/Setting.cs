using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class Setting
    {
        [JsonProperty(PropertyName = "language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; } = LanguageType.Slovak;
        [JsonProperty(PropertyName = "signature", NullValueHandling = NullValueHandling.Ignore)]
        public bool Signature { get; set; } = true;
        [JsonProperty(PropertyName = "payment_info", NullValueHandling = NullValueHandling.Ignore)]
        public bool PaymentInfo { get; set; } = true;
        [JsonProperty(PropertyName = "online_payment", NullValueHandling = NullValueHandling.Ignore)]
        public bool OnlinePayment { get; set; } = true;
        [JsonProperty(PropertyName = "bysquare", NullValueHandling = NullValueHandling.Ignore)]
        public bool BySquare { get; set; } = true;
        [JsonProperty(PropertyName = "paypal", NullValueHandling = NullValueHandling.Ignore)]
        public bool PayPal { get; set; } = true;
    }
}
