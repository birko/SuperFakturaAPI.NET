using Birko.SuperFaktura.Request.Client;
using Birko.SuperFaktura.Request.Tags;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Birko.SuperFaktura
{
    public class Tags
    {
        private readonly AbstractSuperFaktura superFaktura;

        public Tags(AbstractSuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<Dictionary<int, string>> List()
        {
            var result = await superFaktura.Get("tags/index.json").ConfigureAwait(false);
            try
            {
                return superFaktura.DeserializeResult<Dictionary<int, string>>(result);
            }
            catch (Exceptions.ParseException ex)
            {
                try
                {
                    var deserialized = superFaktura.DeserializeResult<string[]>(result);
                    var data = new Dictionary<int, string>();
                    int i = 1;
                    foreach (var tag in deserialized)
                    {
                        data.Add(i, tag);
                        i++;
                    }
                    return data;
                }
                catch
                {
                    throw ex;
                }
            }
        }

        public async Task<Response.Tag.Tag> Add(Tag tag)
        {
            var result = await superFaktura.Post("tags/add", tag).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response.Tag.Tag>(result);
        }

        public async Task<Response.Tag.Tag> Edit(int id, Tag tag)
        {
            var result = await superFaktura.Post(string.Format("tags/edit/{0}", id), tag).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response.Tag.Tag>(result);
        }

        public async Task<ErrorMessageResponse> Delete(int id)
        {
            var result = await superFaktura.Get(string.Format("tags/delete/{0}", id)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<ErrorMessageResponse>(result);
        }
    }
}
