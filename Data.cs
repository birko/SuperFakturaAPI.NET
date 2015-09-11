using Birko.SuperFaktura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura
{
    public class Data
    {
        public IList<Invoice.Item> InvoiceItem { get; internal set; } = new List<Invoice.Item>();
        public IList<int> Tag { get; internal set; } = new List<int>();
        public Expense Expense { get; set; }
        public Invoice Invoice { get; set; }

        public IList<Stock.Movement> StockLog { get; internal set; } = new List<Stock.Movement>();
        public Entities.Client Client { get; internal set; }
    }
}
