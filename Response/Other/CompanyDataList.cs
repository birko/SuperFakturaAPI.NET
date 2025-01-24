using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Other
{
    [JsonConverter(typeof(Converters.ItemListConverter<CompanyData>))]
    public class CompanyDataList : ItemList<CompanyData>
    {
    }
}
