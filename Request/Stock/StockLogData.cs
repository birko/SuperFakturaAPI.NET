using System.Collections.Generic;

namespace Birko.SuperFaktura.Request.Stock
{
    public class StockLogData: Data
    {
        public IEnumerable<Log> StockLog { get; set; }
    }
}
