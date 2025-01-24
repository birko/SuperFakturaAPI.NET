using Newtonsoft.Json;
using System.Globalization;

namespace Birko.SuperFaktura.Request.Client
{
    public class Client
    {

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "bank_account", NullValueHandling = NullValueHandling.Ignore)]
        public string BankAccount { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "bank_code", NullValueHandling = NullValueHandling.Ignore)]
        public string BankCode { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "country_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? CountryID { get; set; } = null;

        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; } = RegionInfo.CurrentRegion.ISOCurrencySymbol;

        [JsonProperty(PropertyName = "default_variable", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultVariable { get; set; }

        [JsonProperty(PropertyName = "delivery_address", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryAddress { get; set; }

        [JsonProperty(PropertyName = "delivery_city", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryCity { get; set; }

        [JsonProperty(PropertyName = "delivery_country", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryCountry { get; set; }

        [JsonProperty(PropertyName = "delivery_country_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? DeliveryCountryID { get; set; } = null;

        [JsonProperty(PropertyName = "delivery_name", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryName { get; set; }

        [JsonProperty(PropertyName = "delivery_phone", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryPhone { get; set; }

        [JsonProperty(PropertyName = "delivery_zip", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryZIP { get; set; }

        [JsonProperty(PropertyName = "discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Discount { get; set; }

        [JsonProperty(PropertyName = "dic", NullValueHandling = NullValueHandling.Ignore)]
        public string DIC { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "due_date", NullValueHandling = NullValueHandling.Ignore)]
        public int DueDate { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "fax", NullValueHandling = NullValueHandling.Ignore)]
        public string Fax { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "iban", NullValueHandling = NullValueHandling.Ignore)]
        public string IBAN { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "ic_dph", NullValueHandling = NullValueHandling.Ignore)]
        public string ICDPH { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "ico", NullValueHandling = NullValueHandling.Ignore)]
        public string ICO { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "match_address", NullValueHandling = NullValueHandling.Ignore)]
        public bool MatchAddress { get; set; } = true;

        [JsonProperty(PropertyName = "phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; } = string.Empty;


        [JsonProperty(PropertyName = "swift", NullValueHandling = NullValueHandling.Ignore)]
        public string SWIFT { get; set; }

        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)]
        public string Tags { get; set; }


        [JsonProperty(PropertyName = "uuid", NullValueHandling = NullValueHandling.Ignore)]
        public string UUID { get; set; }

        [JsonProperty(PropertyName = "zip", NullValueHandling = NullValueHandling.Ignore)]
        public string ZIP { get; set; } = string.Empty;


        [JsonProperty(PropertyName = "update", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool Update { get; set; }
    }
}
