using Birko.SuperFaktura.Request;
using Birko.SuperFaktura.Request.Stock;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.Stock;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Birko.SuperFaktura
{
    public class Stock
    {
        private readonly AbstractSuperFaktura superFaktura;

        public Stock(AbstractSuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<PagedResponse> List(Filter filter, bool listInfo = true)
        {
            var result = await superFaktura.Get(string.Format("stock_items/index.json{0}", filter.ToParameters(listInfo))).ConfigureAwait(false);
            if (listInfo)
            {
                return superFaktura.DeserializeResult<PagedResponse>(result);
            }
            else
            {
                return new PagedResponse { Items = superFaktura.DeserializeResult<ItemList<ListItem>>(result) };
            }
        }

        public async Task<Response.Stock.Item> View(int ID)
        {
            var result = await superFaktura.Get($"/stock_items/view/{ID}").ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<Detail>>(result);
            return data.Data.StockItem;
        }

        public async Task<Detail> Add(Request.Stock.Item item)
        {
            var result = await superFaktura.Post("stock_items/add", new StockItemData () { StockItem = item }).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<Detail>>(result);
            return data.Data;
        }

        public async Task<Detail> Edit(int id, Request.Stock.Item item)
        {
            var result = await superFaktura.Patch($"stock_items/edit/{id}", new StockItemData() { StockItem = item }).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<Detail>>(result);
            return data.Data;
        }

        public async Task<Detail> Delete(int ID)
        {
            var result = await superFaktura.Delete(string.Format("stock_items/delete/{0}", ID)).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<Detail>>(result);
            return data.Data;
        }

        public async Task<IEnumerable<Response.Stock.Log>> AddStockMovement(IEnumerable<Request.Stock.Log> items)
        {
            if (!(items?.Any() ?? false))
            {
                return null;
            }
            var result = await superFaktura.Post("stock_items/addstockmovement", new StockLogsData { StockLog = items.ToArray() }).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<LogResponse>>(result);
            return data.Data.StockLog;
        }

        public async Task<IEnumerable<Response.Stock.Log>> AddStockMovement(Request.Stock.Log item)
        {
            return await AddStockMovement(new[] { item }).ConfigureAwait(false);
        }

        public async Task<PagedLogsResponse> ListStockMovements(int id, PagedParameters filter, bool listInfo = true)
        {
            var result = await superFaktura.Get(string.Format($"stock_items/movements/{id}", filter.ToParameters(listInfo))).ConfigureAwait(false);
            if (listInfo)
            {
                return superFaktura.DeserializeResult<PagedLogsResponse>(result);
            }
            else
            {
                return new PagedLogsResponse { Items = superFaktura.DeserializeResult<ItemList<LogData>>(result) };
            }
        }
    }
}
