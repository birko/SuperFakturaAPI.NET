using Birko.SuperFaktura.Request;
using Birko.SuperFaktura.Request.CashRegister;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.CashRegister;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public async Task<IEnumerable<CashRegister>> List(CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get("cash_registers/getDetails", cancellationToken).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<IEnumerable<CashRegisterData>>(result);
            return data.Select(x => x.CashRegister);
        }

        public async Task<CashRegister> View(int id, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get($"cash_registers/view/{id}", cancellationToken).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<CashRegisterData>(result);
            return data.CashRegister;
        }

        public async Task<ItemList> ListItems(Filter filter, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get(ItemsUrl(filter), cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<ItemList>(result);
        }

        internal static string ItemsUrl(Filter filter)
        {
            return $"cash_register_items/index/{filter.ID}{filter.ToParameters()}";
        }

        /// <param name="checksum">Your own unique identifier of this request (e.g. receipt number, max 32 chars).
        /// If no response arrives, call ResponseByChecksum with it to find out whether the item was created.</param>
        public async Task<CashRegisterItemResponse> AddItem(Request.CashRegister.CashRegisterItem item, string checksum = null, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Post("cash_register_items/add", new CashRegisterItemData { CashRegisterItem = item, CheckSum = checksum }, cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<CashRegisterItemResponse>(result);
        }


        public async Task<CashRegisterSummaryResponse> DeleteItem(int id, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get("cash_register_items/delete/" + id, cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<CashRegisterSummaryResponse>(result);
        }

        public async Task<CashRegisterSummaryResponse> DeleteItems(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            if (!(ids?.Any() ?? false))
            {
                throw new ArgumentException("Parameter ids is empty");
            }
            var result = await superFaktura.Post("cash_register_items/delete", new { ids = string.Join(",", ids) }, cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<CashRegisterSummaryResponse>(result);
        }

        public async Task<byte[]> Download(int id, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.GetByte($"cash_register_items/receipt/{id}", cancellationToken).ConfigureAwait(false);
            try
            {
                string testResult = Encoding.UTF8.GetString(result);
                superFaktura.DeserializeResult<ErrorMessageResponse>(testResult);
            }
            catch (Exceptions.ParseException) when (result != null && result.Length != 0)
            {
                //test deserialization failed. it is a pdf file
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
