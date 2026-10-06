using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request.Client
{
    // Fields left null are not sent, so Edit (PATCH) changes only what was set.
    public class Client
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "bank_account", NullValueHandling = NullValueHandling.Ignore)]
        public string BankAccount { get; set; }

        [JsonProperty(PropertyName = "bank_code", NullValueHandling = NullValueHandling.Ignore)]
        public string BankCode { get; set; }

        [JsonProperty(PropertyName = "city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty(PropertyName = "comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "country", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryName { get; set; }

        [JsonProperty(PropertyName = "country_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? CountryID { get; set; }

        // ISO-3166-1 alpha-2 code (e.g. "CZ"), alternative to CountryID when issuing an invoice.
        [JsonProperty(PropertyName = "country_iso_id", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryISOID { get; set; }

        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "default_variable", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultVariable { get; set; }

        [JsonProperty(PropertyName = "delivery_address", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryAddress { get; set; }

        [JsonProperty(PropertyName = "delivery_city", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryCity { get; set; }

        [JsonProperty(PropertyName = "delivery_country", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryCountry { get; set; }

        [JsonProperty(PropertyName = "delivery_country_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? DeliveryCountryID { get; set; }

        [JsonProperty(PropertyName = "delivery_country_iso_id", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryCountryISOID { get; set; }

        [JsonProperty(PropertyName = "delivery_name", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryName { get; set; }

        [JsonProperty(PropertyName = "delivery_phone", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryPhone { get; set; }

        [JsonProperty(PropertyName = "delivery_zip", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryZIP { get; set; }

        [JsonProperty(PropertyName = "discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Discount { get; set; }

        [JsonProperty(PropertyName = "dic", NullValueHandling = NullValueHandling.Ignore)]
        public string DIC { get; set; }

        [JsonProperty(PropertyName = "due_date", NullValueHandling = NullValueHandling.Ignore)]
        public int? DueDate { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "fax", NullValueHandling = NullValueHandling.Ignore)]
        public string Fax { get; set; }

        [JsonProperty(PropertyName = "iban", NullValueHandling = NullValueHandling.Ignore)]
        public string IBAN { get; set; }

        [JsonProperty(PropertyName = "ic_dph", NullValueHandling = NullValueHandling.Ignore)]
        public string ICDPH { get; set; }

        [JsonProperty(PropertyName = "ico", NullValueHandling = NullValueHandling.Ignore)]
        public string ICO { get; set; }

        [JsonProperty(PropertyName = "match_address", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? MatchAddress { get; set; } = true;

        [JsonProperty(PropertyName = "phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "swift", NullValueHandling = NullValueHandling.Ignore)]
        public string SWIFT { get; set; }

        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)]
        public string Tags { get; set; }

        [JsonProperty(PropertyName = "uuid", NullValueHandling = NullValueHandling.Ignore)]
        public string UUID { get; set; }

        [JsonProperty(PropertyName = "zip", NullValueHandling = NullValueHandling.Ignore)]
        public string ZIP { get; set; }

        // clients/create: also update client in addressbook.
        [JsonProperty(PropertyName = "update", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool Update { get; set; }

        // Client inside an invoice or expense: update addressbook client data when the document is issued.
        [JsonProperty(PropertyName = "update_addressbook", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? UpdateAddressBook { get; set; }

        [JsonProperty(PropertyName = "data_source", NullValueHandling = NullValueHandling.Ignore)]
        public string DataSource { get; set; } = ClientDataSource.Empty;
    }

    public static class ClientDataSource
    {
        public const string Empty = "empty";
        public const string Merge = "merge";
        public const string AddressBook = "addressbook";
    }
}
