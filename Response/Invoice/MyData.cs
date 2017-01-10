using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class MyData
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; internal  set; }
        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int UserID { get; set; }
        [JsonProperty(PropertyName = "country_id", NullValueHandling = NullValueHandling.Ignore)]
        public int CountryID { get; set; }
        [JsonProperty(PropertyName = "company_name", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyName { get; set; }
        [JsonProperty(PropertyName = "ico", NullValueHandling = NullValueHandling.Ignore)]
        public string ICO { get; set; }
        [JsonProperty(PropertyName = "dic", NullValueHandling = NullValueHandling.Ignore)]
        public string DIC { get; set; }
        [JsonProperty(PropertyName = "ic_dph", NullValueHandling = NullValueHandling.Ignore)]
        public string ICDPH { get; set; }
        [JsonProperty(PropertyName = "business_register", NullValueHandling = NullValueHandling.Ignore)]
        public string BusinessRegister { get; set; }
        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }
        [JsonProperty(PropertyName = "city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }
        [JsonProperty(PropertyName = "zip", NullValueHandling = NullValueHandling.Ignore)]
        public string ZIP{ get; set; }
        [JsonProperty(PropertyName = "tax_payer", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool TaxPayer { get; set; }
        [JsonProperty(PropertyName = "country", NullValueHandling = NullValueHandling.Ignore)]
        public Country Country { get; set; }
        [JsonProperty(PropertyName = "BankAccount", NullValueHandling = NullValueHandling.Ignore)]
        public BankAccount[] BankAccount { get; set; }
    }
}
