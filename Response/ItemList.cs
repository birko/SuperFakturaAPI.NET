using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;

namespace Birko.SuperFaktura.Response
{
    [JsonArray]
    public class ItemList<T>: List<T>
    {
    }
}
