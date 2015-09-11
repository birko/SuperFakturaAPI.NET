using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Expense
    {
        public class Type
        {
            public const string Invoice = "invoice";
            public const string Bill = "bill";
            public const string Internal = "internal";
            public const string Contribution = "contribution";
        }
    }
}
