using Birko.SuperFaktura.Response.BankAccounts;
using Birko.SuperFaktura.Response.Logs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Birko.SuperFaktura
{
    public class Logs
    {
        private readonly AbstractSuperFaktura superFaktura;

        public Logs(AbstractSuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<IEnumerable<ActivityLogItem>> GetActivityLogs(Request.Logs.ActivityLog logData)
        {
            var result = await superFaktura.Get($"/activity_logs/activity_list/{logData.DocumentType}/{logData.DocumentID}/{logData.Limit}").ConfigureAwait(false);
            try
            {
                return superFaktura.DeserializeResult<IEnumerable<ActivityLogItem>>(result);
            }
            catch (JsonSerializationException ex)
            {
                try
                {
                    var deserialized = superFaktura.DeserializeResult<IEnumerable<ActivityLogItem>>(result);
                    return deserialized;
                }
                catch
                {
                    throw ex;
                }
            }
        }
    }
}
