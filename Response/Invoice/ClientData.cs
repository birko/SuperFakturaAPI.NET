using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class ClientData
    {
        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserID { get; set; }
        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserProfileID { get; set; }
        [JsonProperty(PropertyName = "country_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? CountryID { get; set; }
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "ico", NullValueHandling = NullValueHandling.Ignore)]
        public string ICO { get; set; }
        [JsonProperty(PropertyName = "dic", NullValueHandling = NullValueHandling.Ignore)]
        public string DIC { get; set; }
        [JsonProperty(PropertyName = "ic_dph", NullValueHandling = NullValueHandling.Ignore)]
        public string ICDPH { get; set; }
        [JsonProperty(PropertyName = "bank_account", NullValueHandling = NullValueHandling.Ignore)]
        public string BankAccount { get; set; }
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }
        [JsonProperty(PropertyName = "city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }
        [JsonProperty(PropertyName = "zip", NullValueHandling = NullValueHandling.Ignore)]
        public string ZIP { get; set; }
        [JsonProperty(PropertyName = "state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }
        [JsonProperty(PropertyName = "country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }
        [JsonProperty(PropertyName = "delivery_name", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryName { get; set; }
        [JsonProperty(PropertyName = "delivery_address", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryAddress { get; set; }
        [JsonProperty(PropertyName = "delivery_city", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryCity { get; set; }
        [JsonProperty(PropertyName = "delivery_zip", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryZIP { get; set; }
        [JsonProperty(PropertyName = "delivery_state", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryState { get; set; }
        [JsonProperty(PropertyName = "delivery_country", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryCountry { get; set; }
        [JsonProperty(PropertyName = "delivery_country_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? DeliveryCountryID { get; set; }
        [JsonProperty(PropertyName = "phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }
        [JsonProperty(PropertyName = "fax", NullValueHandling = NullValueHandling.Ignore)]
        public string Fax { get; set; }
        [JsonProperty(PropertyName = "due_date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DueDate { get; set; }
        [JsonProperty(PropertyName = "default_variable", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultVariable { get; set; }
        [JsonProperty(PropertyName = "discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Discount { get; set; }
        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }
        [JsonProperty(PropertyName = "comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }
        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)]
        public string Tags { get; set; }
        [JsonProperty(PropertyName = "demo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? Demo { get; set; } = null;
        [JsonProperty(PropertyName = "update_addressbook", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.LowerBooleanConverter))]
        public bool? UpdateAddresBook { get; set; } = null;

        [JsonProperty(PropertyName = "Country", NullValueHandling = NullValueHandling.Ignore)]
        public Country CountryFull { get; set; }
    }
}
