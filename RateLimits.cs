using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura
{
    public class DetailLimit
    {
        public long? Limit { get; set; }
        public long? Remaining { get; set; }
        public DateTime? Reset{ get; set; }

        public override string ToString()
        {
            return string.Format("{0}/{1} - {2:dd.MM.yyyy}", Limit, Remaining, Reset);
        }
    }

    public class RateLimits
    {
        public DetailLimit Daily { get; set; }
        public DetailLimit Monthly { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return string.Format("Daily: {0}, Monthly: {1}, Message: {2}", Daily, Monthly, Message);
        }
    }
}
