using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Birko.SuperFaktura.Response
{

    public abstract class ItemListResponse<T>
    {
        public abstract ItemList<T> Items { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Items: { \n");
            if (Items != null)
            {
                foreach (var item in Items)
                {
                    builder.AppendFormat("\t {0} \n", item.ToString());
                }
            }
            else
            {
                builder.AppendFormat("\t {0} \n", "NULL");
            }
            builder.Append("},");
            return builder.ToString();
        }
    }
    public abstract class PagedResponse<T>: ItemListResponse<T>
    {
        [JsonProperty(PropertyName = "itemCount", NullValueHandling = NullValueHandling.Ignore)]
        public int ItemCount { get; set; } = 0;

        [JsonProperty(PropertyName = "pageCount", NullValueHandling = NullValueHandling.Ignore)]
        public int PageCount { get; set; } = 0;

        [JsonProperty(PropertyName = "perPage", NullValueHandling = NullValueHandling.Ignore)]
        public int PerPage { get; set; } = 0;

        [JsonProperty(PropertyName = "page", NullValueHandling = NullValueHandling.Ignore)]
        public int Page { get; set; } = 0;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{\n");
            builder.AppendFormat(" ItemCount: {0}, \n PageCount: {1}, \n PerPage: {2}, \n Page: {3}, \n", ItemCount, PageCount, PerPage, Page);
            builder.AppendFormat(base.ToString());
            builder.Append("}");

            return builder.ToString();
        }
    }

    public class DynamicResponse : PagedResponse<ExpandoObject>
    {
        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }

        [JsonProperty(PropertyName = "items", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ItemListConverter<ExpandoObject>))]
        public override ItemList<ExpandoObject> Items { get; set; }
    }
}
