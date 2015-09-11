using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Invoice
    {
        public class MarkEmail
        {
            public int InvoiceId { get; set; }
            public string EmailAddres { get; set; }
            public string Subject { get; set; } = string.Empty;
            public string Message { get; set; } = string.Empty;
        }

        public class Email
        {
            public int InvoiceId { get; set; }
            public string To { get; set; }
            public IEnumerable<string> CC { get; set; }
            public IEnumerable<string> BCC { get; set; }
            public string Subject { get; set; } = string.Empty;
            public string Body { get; set; } = string.Empty;
        }
    }
}
