using Birko.SuperFaktura.Request.Stock;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Birko.SuperFaktura.Response.Other
{
    public class PagedBankAccountMovement : PagedResponse<BankAccountMovement>
    {
        [JsonProperty(PropertyName = "items", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.ItemListConverter<BankAccountMovement>))]
        public override ItemList<BankAccountMovement> Items { get; set; }
    }
}
