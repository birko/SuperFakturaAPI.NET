using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Stock
    {
        public class Item
        {
            public int? ID { get; set; } = null;
            public string Name { get; set; }
            public string Description { get; set; }
            public string Unit { get; set; } = "ks";
            public decimal UnitPrice { get; set; }
            public decimal VAT { get; set; } = 20;
            public decimal Stock { get; set; } = 0;
            public string SKU { get; set; }
        }
    }
}
