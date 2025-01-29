using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class InvoiceSettings
    {
        [JsonProperty(PropertyName = "bysquare", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool BySquare { get; set; } = true;

        [JsonProperty(PropertyName = "callback_payment", NullValueHandling = NullValueHandling.Ignore)]
        public string CallbackPayment { get; set; } = null;

        [JsonProperty(PropertyName = "language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; } = ValueLists.LanguageType.Slovak;

        [JsonProperty(PropertyName = "online_payment", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool OnlinePayment { get; set; } = true;

        [JsonProperty(PropertyName = "payment_info", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool PaymentInfo { get; set; } = true;

        [JsonProperty(PropertyName = "paypal", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool PayPal { get; set; } = true;

        [JsonProperty(PropertyName = "show_prices", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool ShowPrices { get; set; } = false;

        [JsonProperty(PropertyName = "signature", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool Signature { get; set; } = true;

        [JsonProperty(PropertyName = "summary_bg_color", NullValueHandling = NullValueHandling.Ignore)]
        public string SummaryBackgroundColor { get; set; }

        [JsonProperty(PropertyName = "create_from_multiple_deliveries", NullValueHandling = NullValueHandling.Ignore)]
        public string CreateFromMultipleDeliveries { get; set; }
    }
}
