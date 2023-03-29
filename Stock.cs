﻿using Birko.SuperFaktura.Request;
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

        public async Task<Response<ExpandoObject>> Get(int ID)
        {
            var result = await superFaktura.Get(string.Format("stock_items/edit/{0}.json", ID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }

        public async Task<PagedResponse> Get(Filter filter, bool listInfo = true)
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

        public async Task<Response<Detail>> Save(Request.Stock.Item item)
        {
            var result = await superFaktura.Post("stock_items/add", new StockItemData () { StockItem = item }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<Detail>>(result);
        }

        public async Task<Response<Detail>> Edit(Request.Stock.Item item)
        {
            var result = await superFaktura.Post("stock_items/edit", new StockItemData() { StockItem = item }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<Detail>>(result);
        }

        public async Task<Response<Detail>> Delete(int ID)
        {
            var result = await superFaktura.Get(string.Format("stock_items/delete/{0}", ID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<Detail>>(result);
        }

        public async Task<Response<LogResponse>> AddStockMovement(IEnumerable<Request.Stock.Log> items)
        {
            if (items != null)
            {
                var result = await superFaktura.Post("stock_items/addstockmovement", new StockLogData { StockLog = items.ToArray() }).ConfigureAwait(false);
                return superFaktura.DeserializeResult<Response<LogResponse>>(result);
            }
            return await Task.FromResult<Response<LogResponse>>(null).ConfigureAwait(false);
        }

        public async Task<Response<LogResponse>> AddStockMovement(Request.Stock.Log item)
        {
            return await AddStockMovement(new[] { item }).ConfigureAwait(false);
        }
    }
}
