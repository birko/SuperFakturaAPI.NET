using Birko.SuperFaktura.Request;
using Birko.SuperFaktura.Request;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.BankAccounts;
using Birko.SuperFaktura.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Birko.SuperFaktura
{
    public class CashRegister
    {
        private readonly AbstractSuperFaktura superFaktura;

        public CashRegister(AbstractSuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<Response.CashRegister.PagedResponse> Get(int id, Request.CashRegister.Filter filter, bool listInfo = true)
        {
            var result = await superFaktura.Get(string.Format("/cash_register_items/index/{0}{1}", id, filter.ToParameters(listInfo))).ConfigureAwait(false);
            if (listInfo)
            {
                return superFaktura.DeserializeResult<Response.CashRegister.PagedResponse>(result);
            }
            else
            {
                return new Response.CashRegister.PagedResponse { Items = superFaktura.DeserializeResult<ItemList<ExpandoObject>>(result) };
            }
        }

        public async Task<Response.CashRegister.CashRegisterItem[]> GetDetails(int id)
        {
            var result = await superFaktura.Get("cash_registers/getDetails").ConfigureAwait(false);
            try
            {
                return superFaktura.DeserializeResult<Response.CashRegister.CashRegisterItem[]>(result);
            }
            catch (JsonSerializationException ex)
            {
                throw ex;
            }
        }

        public async Task<Response<ExpandoObject>> Delete(int id)
        {
            var result = await superFaktura.Get("/cash_register_items/delete/" + id).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }

        public async Task<Response<ExpandoObject>> Delete(int[] ids)
        {
            var result = await superFaktura.Post("/cash_register_items/delete", new DataData { Data = new { ids = string.Join(",", ids) } }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }
    }
}
