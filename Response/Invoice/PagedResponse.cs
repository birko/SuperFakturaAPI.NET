using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class PagedResponse : PagedResponse<Detail>
    {
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public bool Status { get; set; }
    }
}
