using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Expense
{
    public class ExpensePaymentData
    {
        [JsonProperty(PropertyName = "ExpensePayment", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ArrayObjectConverter))]
        public IEnumerable<Payment> ExpensePayment { get; set; } = null;
    }

    public class PaymentResponse: Response<ExpensePaymentData>
    {

        [JsonProperty(PropertyName = "expense", NullValueHandling = NullValueHandling.Ignore)]
        public Expense Expense { get; set; }
    }
}
