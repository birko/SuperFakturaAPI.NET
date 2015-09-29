using Raven.Imports.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Client
    {
        [JsonProperty(PropertyName = "id")]
        public int? ID { get; set; }
        [JsonProperty(PropertyName = "user_id")]
        public int? UserID { get; set; }
        [JsonProperty(PropertyName = "user_profile_id")]
        public int? UserProfileID { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "ico")]
        public string ICO { get; set; }
        [JsonProperty(PropertyName = "dic")]
        public string DIC { get; set; }
        [JsonProperty(PropertyName = "ic_dph")]
        public string ICDPH { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
        [JsonProperty(PropertyName = "zip")]
        public string ZIP { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
        [JsonProperty(PropertyName = "fax")]
        public string Fax { get; set; }
        [JsonProperty(PropertyName = "bank_account")]
        public string BankAccount { get; set; }
        [JsonProperty(PropertyName = "country_id")]
        public int CountryID { get; set; }
        [JsonProperty(PropertyName = "country_iso_id")]
        public string CountryISOID { get; set; }
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
        [JsonProperty(PropertyName = "delivery_address")]
        public string DeliveryAddress { get; set; }
        [JsonProperty(PropertyName = "delivery_city")]
        public string DeliveryCity { get; set; }
        [JsonProperty(PropertyName = "delivery_zip")]
        public string DeliveryZIP { get; set; }
        [JsonProperty(PropertyName = "delivery_country_id")]
        public int DeliveryCountryID { get; set; }
        [JsonProperty(PropertyName = "delivery_iso_id")]
        public string DeliveryCountryISOID { get; set; }
        [JsonProperty(PropertyName = "delivery_country")]
        public string DeliveryCountry { get; set; }
        [JsonProperty(PropertyName = "delivery_name")]
        public string DeliveryName { get; set; }
        [JsonProperty(PropertyName = "match_address")]
        public bool MatchAddress { get; set; } = true;
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }
        [JsonProperty(PropertyName = "default_variable")]
        public string DefaultVariable { get; set; }
        [JsonProperty(PropertyName = "dicount")]
        public decimal Discount { get; set; }
        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; } = Invoice.Currency.Euro;
        [JsonProperty(PropertyName = "created")]
        public DateTime Created { get; set; }
        [JsonProperty(PropertyName = "modified")]
        public DateTime Modified { get; set; }
        [JsonProperty(PropertyName = "demo")]
        public bool Demo { get; set; } = false;
        [JsonProperty(PropertyName = "tags")]
        public dynamic[] Tags { get; set; }
    }
}
