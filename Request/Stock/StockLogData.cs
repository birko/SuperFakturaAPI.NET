using System.Collections.Generic;

namespace Birko.SuperFaktura.Request.Stock
{
    public class StockLogsData: Data
    {
        public IEnumerable<Log> StockLog { get; set; }
    }


    public class LogData : Data
    {
        public Log StockLog { get; set; }
    }
}
