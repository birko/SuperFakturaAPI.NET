using Newtonsoft.Json;
using System.Dynamic;

namespace Birko.SuperFaktura.Response.CashRegister
{
    public class PagedResponse : PagedResponse<ExpandoObject>
    {
        [JsonProperty(PropertyName = "CashRegister", NullValueHandling = NullValueHandling.Ignore)]
        public CashRegister CashRegister { get; set; }

        [JsonProperty(PropertyName = "items", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.ItemListConverter<ExpandoObject>))]
        public override ItemList<ExpandoObject> Items { get; set; }
    }
}
