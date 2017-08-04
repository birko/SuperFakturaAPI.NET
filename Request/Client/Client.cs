using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.Client
{
    public class Client
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; internal set; }
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "ico", NullValueHandling = NullValueHandling.Ignore)]
        public string ICO { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "dic", NullValueHandling = NullValueHandling.Ignore)]
        public string DIC { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "ic_dph", NullValueHandling = NullValueHandling.Ignore)]
        public string ICDPH { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "zip", NullValueHandling = NullValueHandling.Ignore)]
        public string ZIP { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "fax", NullValueHandling = NullValueHandling.Ignore)]
        public string Fax { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "bank_account", NullValueHandling = NullValueHandling.Ignore)]
        public string BankAccount { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "bank_account_id", NullValueHandling = NullValueHandling.Ignore)]
        public string BankAccountID { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "bank_account_prefix", NullValueHandling = NullValueHandling.Ignore)]
        public string BankAccountPrefix { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "iban", NullValueHandling = NullValueHandling.Ignore)]
        public string IBAN { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "country_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? CountryID { get; set; } = null;
        [JsonProperty(PropertyName = "country_iso_id", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryISOID { get; set; }
        [JsonProperty(PropertyName = "country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "delivery_address", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryAddress { get; set; }
        [JsonProperty(PropertyName = "delivery_city", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryCity { get; set; }
        [JsonProperty(PropertyName = "delivery_zip", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryZIP { get; set; }
        [JsonProperty(PropertyName = "delivery_country_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? DeliveryCountryID { get; set; } = null;
        [JsonProperty(PropertyName = "delivery_country_iso_id", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryCountryISOID { get; set; }
        [JsonProperty(PropertyName = "delivery_country", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryCountry { get; set; }
        [JsonProperty(PropertyName = "delivery_name", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryName { get; set; }
        [JsonProperty(PropertyName = "match_address", NullValueHandling = NullValueHandling.Ignore)]
        public bool MatchAddress { get; set; } = true;
        [JsonProperty(PropertyName = "update_addressbook", NullValueHandling = NullValueHandling.Ignore)]
        public bool UpdateAddressBook { get; set; } = false;
        [JsonProperty(PropertyName = "comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "default_variable", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultVariable { get; set; }
        [JsonProperty(PropertyName = "discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Discount { get; set; }
        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; } = Invoice.CurrencyType.Euro;
        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)]
        public string Tags { get; set; }
    }
}
