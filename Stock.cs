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
            var result = await superFaktura.Get(string.Format("stock_items/edit/{0}.json", ID)).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Response<ExpandoObject>>(result);
        }

        public async Task<PagedResponse> Get(Filter filter, bool listInfo = true)
        {
            var result = await superFaktura.Get(string.Format("stock_items/index.json{0}", filter.ToParameters(listInfo))).ConfigureAwait(false);
            if (listInfo)
            {
                return JsonConvert.DeserializeObject<PagedResponse>(result);
            }
            else
            {
                return new PagedResponse { Items = JsonConvert.DeserializeObject<ItemList<ListItem>>(result) };
            }
        }

        public async Task<Response<Detail>> Save(Request.Stock.Item item)
        {
            var result = await superFaktura.Post("stock_items/add", item).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Response<Detail>>(result);
        }

        public async Task<Response<Detail>> Edit(Request.Stock.Item item)
        {
            var result = await superFaktura.Post("stock_items/edit", item).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Response<Detail>>(result);
        }

        public async Task<Response<Detail>> Delete(int ID)
        {
            var result = await superFaktura.Get(string.Format("stock_items/delete/{0}", ID)).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Response<Detail>>(result);
        }

        public async Task<Response<LogResponse>> AddStockMovement(IEnumerable<Request.Stock.Log> items)
        {
            if (items != null)
            {
                var result = await superFaktura.Post("stock_items/addstockmovement", new { StockLog = items.ToArray() }).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<Response<LogResponse>>(result);
            }
            return await Task.FromResult<Response<LogResponse>>(null).ConfigureAwait(false);
        }

        public async Task<Response<LogResponse>> AddStockMovement(Request.Stock.Log item)
        {
            return await AddStockMovement(new[] { item });
        }
    }
}
