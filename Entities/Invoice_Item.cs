using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Invoice
    {
        public class Item
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Quantity { get; set; } = 1;
            public string Unit { get; set; } = "ks";
            public decimal UnitPrice { get; set; }
            public decimal Tax { get; set; } = 20;
            public int? StockItemID { get; set; } = null;
            public string SKU { get; set; }
            public decimal Discount { get; set; } = 0;
            public bool LoadDataFromStock { get; set; } = true;
        }
    }
}
