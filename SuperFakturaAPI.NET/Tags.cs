using Birko.SuperFaktura.Request.Tags;
using Birko.SuperFaktura.Response;
using System.Collections.Generic;
using System.Threading;
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

        public async Task<Dictionary<int, string>> List(CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get("tags/index.json", cancellationToken).ConfigureAwait(false);
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

        public async Task<Response.Tag.Tag> Add(Tag tag, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Post("tags/add", tag, cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response.Tag.Tag>(result);
        }

        public async Task<Response.Tag.Tag> Edit(int id, Tag tag, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Post($"tags/edit/{id}", tag, cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response.Tag.Tag>(result);
        }

        public async Task<StringMessageResponse> Delete(int id, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get($"tags/delete/{id}", cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<StringMessageResponse>(result);
        }
    }
}
