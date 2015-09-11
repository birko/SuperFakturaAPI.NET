using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Client
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string ICO { get; set; }
        public string DIC { get; set; }
        public string ICDPH { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string BankAccount { get; set; }
        public int CountryID { get; set; }
        public string CountryISOID { get; set; }
        public string Country { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryZIP { get; set; }
        public int DeliveryCountryID { get; set; }
        public string DeliveryCountryISOID { get; set; }
        public string DeliveryCountry { get; set; }
        public string DeliveryName { get; set; }
        public bool MatchAddress { get; set; } = true;
        public string Comment { get; set; }
    }
}
