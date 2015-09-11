using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Invoice
    {
        public class Post
        {
            public int InvoiceID { get; set; }
            public string DeliveryAddress { get; set; }
            public string DeliveryCity { get; set; }
            public string DeliveryState{ get; set; }
        }
    }
}
