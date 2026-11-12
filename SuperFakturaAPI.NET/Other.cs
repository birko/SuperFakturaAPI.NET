using Birko.SuperFaktura.Request.Client;
using Birko.SuperFaktura.Request.Tags;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.Client;
using Birko.SuperFaktura.Response.Other;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
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

        public async Task<Accounts> ListAccounts(CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get($"users/company_switcher", cancellationToken).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Accounts>(result);
            return data;
        }

        public async Task<IEnumerable<CompanyData>> ListUserCompanies(bool all = false, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get($"users/getUserCompaniesData/{ (all? 1: 0)}", cancellationToken).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<ListResponse<CompanyData>>(result);
            return data.Data;
        }

        public async Task<Request.Other.SMS> SendSMS(Request.Other.SMS sms, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Post($"sms/send", sms, cancellationToken).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<Request.Other.SMS>>(result);
            return data.Data;
        }

        public async Task<PagedResponse<BankAccountMovement>> ListBankAccountMovements(Request.Other.BankMovementFilter filter, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get($"accounts/index.json{filter.ToParameters()}", cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<PagedResponse<BankAccountMovement>>(result);
        }

        public async Task<IEnumerable<ActivityLogItem>> ListActivityLogs(string documentType, int documentID, int limit = 10, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get($"activity_logs/activity_list/{documentType}/{documentID}/{limit}", cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<IEnumerable<ActivityLogItem>>(result);
        }
    }
}
