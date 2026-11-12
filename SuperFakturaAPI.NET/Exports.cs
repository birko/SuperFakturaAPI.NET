using Birko.SuperFaktura.Request.Expense;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.Expense;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Birko.SuperFaktura
{
    public class Exports
    {
        private readonly AbstractSuperFaktura superFaktura;

        public Exports(AbstractSuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<Response.Export.Export> Export(Request.Export.ExportData export, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Post("exports", export, cancellationToken).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response.Export.ExporData> (result);
            return data.Export;
        }

        public async Task<Response.Export.Export> Status(int id, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get($"exports/getStatus/{id}", cancellationToken).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response.Export.ExporData>(result);
            return data.Export;
        }

        public async Task<byte[]> Download(int id, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.GetByte($"exports/download_export/{id}", cancellationToken).ConfigureAwait(false);
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
