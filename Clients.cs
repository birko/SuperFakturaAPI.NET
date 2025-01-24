using Birko.SuperFaktura.Request.Client;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Birko.SuperFaktura
{
    public class Clients
    {
        private readonly AbstractSuperFaktura superFaktura;

        public Clients(AbstractSuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<PagedResponse> List(Filter filter, bool listInfo = true)
        {
            var result = await superFaktura.Get(string.Format("clients/index.json{0}", filter.ToParameters(listInfo))).ConfigureAwait(false);
            if (listInfo)
            {
                return superFaktura.DeserializeResult<PagedResponse>(result);
            }
            else
            {
                return new PagedResponse { Items = superFaktura.DeserializeResult<ItemList<ListItem>>(result) };
            }
        }

        public async Task<Response.Client.Client> Add(Request.Client.Client client, IEnumerable<int> tags = null)
        {
            var result = await superFaktura.Post("clients/create", new ClientData { Client = client, Tag = tags }).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<ResponseClient>>(result);
            return data.Data.Client;
        }

        public async Task<StringMessageResponse> Edit(int id, Request.Client.Client client, IEnumerable<int> tags = null)
        {
            var result = await superFaktura.Patch($"clients/edit/{id}", new ClientData { Client = client, Tag = tags }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ResponseClient>>(result);
        }

        public async Task<DetailClient> View(int id)
        {
            var result = await superFaktura.Get($"/clients/view/{id}").ConfigureAwait(false);
            return superFaktura.DeserializeResult<DetailClient>(result);
        }

        public async Task<RedirectResponse> Delete(int id)
        {
            var result = await superFaktura.Delete($"/clients/delete/{id}").ConfigureAwait(false);
            return superFaktura.DeserializeResult<RedirectResponse>(result);
        }
    }
}
