using Birko.SuperFaktura.Response.BankAccounts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Birko.SuperFaktura
{
    public class BankAccounts
    {
        private readonly AbstractSuperFaktura superFaktura;

        public BankAccounts(AbstractSuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<Dictionary<int, BankAccount>> GetBankAccounts()
        {
            var result = await superFaktura.Get("bank_accounts/index").ConfigureAwait(false);
            try
            {
                return superFaktura.DeserializeResult<Dictionary<int, BankAccount>>(result);
            }
            catch (JsonSerializationException ex)
            {
                try
                {
                    var deserialized = superFaktura.DeserializeResult<BankAccount[]>(result);
                    return deserialized.ToDictionary(x => x.ID, x => x);
                }
                catch
                {
                    throw ex;
                }
            }
        }

        public async Task<BankAccount> AddBankAccount(Request.BankAccounts.BankAccount account)
        {
            var result = await superFaktura.Post("bank_accounts/add", account).ConfigureAwait(false);
            return superFaktura.DeserializeResult<BankAccount>(result);
        }

        public async Task<BankAccount> EditBankAccount(int id, Request.BankAccounts.BankAccount account)
        {
            var result = await superFaktura.Post(string.Format("bank_accounts/update/{0}", id), account).ConfigureAwait(false);
            return superFaktura.DeserializeResult<BankAccount>(result);
        }

        public async Task<string> DeleteBankAccount(int id)
        {
            var result = await superFaktura.Get(string.Format("bank_accounts/delete/{0}", id)).ConfigureAwait(false);
            return result;
        }
    }
}
