using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Stock
    {
        public class Movement
        {
            public int? StockItemID { get; set; } = null;
            public decimal Quantity { get; set; } = 1;
            public string Note { get; set; }
            public DateTime Created { get; set; }
        }
    }
}
