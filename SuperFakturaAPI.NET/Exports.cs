using Birko.SuperFaktura.Request.Expense;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.Expense;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
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

        public async Task<Response.Export.Export> Export(Request.Export.ExportData export)
        {
            var result = await superFaktura.Post("exports", export).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response.Export.ExporData> (result);
            return data.Export;
        }

        public async Task<Response.Export.Export> Status(int id)
        {
            var result = await superFaktura.Get($"exports/getStatus/{id}").ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response.Export.ExporData>(result);
            return data.Export;
        }

        public async Task<byte[]> Download(int id)
        {
            var result = await superFaktura.GetByte($"exports/download_export/{id}").ConfigureAwait(false);
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
