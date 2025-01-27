using Birko.SuperFaktura.Request.Client;
using Birko.SuperFaktura.Request.Tags;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.Client;
using Birko.SuperFaktura.Response.Other;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace Birko.SuperFaktura
{
    public class Other
    {
        private readonly AbstractSuperFaktura superFaktura;

        public Other(AbstractSuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<Accounts> ListAccounts()
        {
            var result = await superFaktura.Get($"users/company_switcher").ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Accounts>(result);
            return data;
        }

        public async Task<IEnumerable<CompanyData>> ListUserCompanies(bool all = false)
        {
            var result = await superFaktura.Get($"users/getUserCompaniesData/{ (all? 1: 0)}]").ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<CompanyDataList>>(result);
            return data.Data;
        }

        public async Task<Request.Other.SMS> SendSMS(Request.Other.SMS sms)
        {
            var result = await superFaktura.Post($"sms/send", sms).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<Request.Other.SMS>>(result);
            return data.Data;
        }

        public async Task<PagedBankAccountMovement> ListBankAccountMovements(Request.Other.BankMovementFilter filter)
        {
            var result = await superFaktura.Get($"accounts/index.json{filter.ToParameters()}").ConfigureAwait(false);
            return superFaktura.DeserializeResult<PagedBankAccountMovement>(result);
        }

        public async Task<IEnumerable<ActivityLogItem>> ListActivityLogs(string documentType, int documentID, int limit = 10)
        {
            var result = await superFaktura.Get($"activity_logs/activity_list/{documentType}/{documentID}/{limit}").ConfigureAwait(false);
            return superFaktura.DeserializeResult<IEnumerable<ActivityLogItem>>(result);
        }
    }
}
