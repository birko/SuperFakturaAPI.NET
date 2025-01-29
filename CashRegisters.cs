using Birko.SuperFaktura.Request;
using Birko.SuperFaktura.Request.CashRegister;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.CashRegister;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Birko.SuperFaktura
{
    public class CashRegisters
    {
        private readonly AbstractSuperFaktura superFaktura;

        public CashRegisters(AbstractSuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<IEnumerable<CashRegister>> List()
        {
            var result = await superFaktura.Get("cash_registers/getDetails").ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<IEnumerable<CashRegisterData>>(result);
            return data.Select(x => x.CashRegister);
        }

        public async Task<CashRegister> View(int id)
        {
            var result = await superFaktura.Get($"cash_registers/view/{id}").ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<CashRegisterData>(result);
            return data.CashRegister;
        }

        public async Task<PagedResponse<Item>> ListItems(Filter filter)
        {
            var result = await superFaktura.Get($"cash_register_items/index/{filter.ToParameters()}").ConfigureAwait(false);
            return superFaktura.DeserializeResult<PagedResponse<Item>>(result);
        }

        public async Task<CashRegisterItemResponse> AddItem(Request.CashRegister.CashRegisterItem item)
        {
            var result = await superFaktura.Post("cash_register_items/add", new CashRegisterItemData { CashRegisterItem = item}).ConfigureAwait(false);
            return superFaktura.DeserializeResult<CashRegisterItemResponse>(result);
        }


        public async Task<CashRegisterSummaryResponse> DeleteItem(int id)
        {
            var result = await superFaktura.Get("cash_register_items/delete/" + id).ConfigureAwait(false);
            return superFaktura.DeserializeResult<CashRegisterSummaryResponse>(result);
        }

        public async Task<CashRegisterSummaryResponse> DeleteItems(IEnumerable<int> ids)
        {
            if (!(ids?.Any() ?? false))
            {
                throw new ArgumentException("Parameter ids is empty");
            }
            var result = await superFaktura.Post("/cash_register_items/delete", new { ids = string.Join(",", ids) }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<CashRegisterSummaryResponse>(result);
        }

        public async Task<byte[]> Download(int id)
        {
            return await superFaktura.GetByte($"cash_register_items/receipt/{id}").ConfigureAwait(false);
        }
    }
}
