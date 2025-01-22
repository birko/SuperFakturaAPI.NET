using Birko.SuperFaktura.Request;
using Birko.SuperFaktura.Response;
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

        public async Task<IEnumerable<BankAccount>> List()
        {
            var result = await superFaktura.Get("bank_accounts/index").ConfigureAwait(false);
            var deserialized = superFaktura.DeserializeResult<Response.BankAccounts.BankAccounts>(result);
            return deserialized.Accounts.Select(x => x.BankAccount);
        }

        public async Task<BankAccount> Add(Request.BankAccounts.BankAccount account)
        {
            var result = await superFaktura.Post("bank_accounts/add", account).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<BankAccountData>(result);
            return data.BankAccount;
        }

        public async Task<BankAccount> Edit(int id, Request.BankAccounts.BankAccount account)
        {
            var result = await superFaktura.Post(string.Format("bank_accounts/update/{0}", id), account).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<EditBankAccountResponse>(result);
            return data.Message?.BankAccount;
        }

        public async Task<StringMessageResponse> Delete(int id)
        {
            var result = await superFaktura.Get(string.Format("bank_accounts/delete/{0}", id)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<StringMessageResponse>(result);
        }
    }
}
