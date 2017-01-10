using Birko.SuperFaktura.Request.Stock;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.Stock;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birko.SuperFaktura
{
    public class Stock
    {
        private readonly SuperFaktura superFaktura;

        public Stock(SuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<Response<ExpandoObject>> Get(int ID)
        {
            return JsonConvert.DeserializeObject<Response<ExpandoObject>>(await superFaktura.Get(string.Format("stock_items/edit/{0}.json", ID)));
        }

        public async Task<PagedResponse> Get(Filter filter, bool listInfo = true)
        {
            var url = string.Format("stock_items/index.json{0}", filter.ToParameters(listInfo));
            if (listInfo)
            {
                return JsonConvert.DeserializeObject<PagedResponse>(await superFaktura.Get(url));
            }
            else
            {
                return new PagedResponse { Items = JsonConvert.DeserializeObject<ItemList<ListItem>>(await superFaktura.Get(url)) };
            }
        }

        public async Task<Response<Detail>> Save(Request.Stock.Item item)
        {
            return JsonConvert.DeserializeObject<Response<Detail>>(await superFaktura.Post("stock_items/add", item));
        }

        public async Task<Response<Detail>> Edit(Request.Stock.Item item)
        {
            return JsonConvert.DeserializeObject<Response<Detail>>(await superFaktura.Post("stock_items/edit", item));
        }

        public async Task<Response<Detail>> Delete(int ID)
        {
            return JsonConvert.DeserializeObject<Response<Detail>>(await superFaktura.Get(string.Format("stock_items/delete/{0}", ID)));
        }

        public async Task<Response<LogResponse>> AddStockMovement(IEnumerable<Request.Stock.Log> items)
        {
            if (items != null)
            {
                return JsonConvert.DeserializeObject<Response<LogResponse>>(await superFaktura.Post("stock_items/addstockmovement", new { StockLog = items.ToArray() }));
            }
            return null;
        }

        public async Task<Response<LogResponse>> AddStockMovement(Request.Stock.Log item)
        {
            return await AddStockMovement(new[] { item });
        }
    }
}
