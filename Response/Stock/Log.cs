using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Stock
{
    public class LogResponse
    {
        [JsonProperty(PropertyName = "StockLog", NullValueHandling = NullValueHandling.Ignore)]
        public Log[] StockLog { get; internal set; }
    }

    public class Log : Request.Stock.Log
    {
    }
}
