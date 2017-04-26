using Birko.SuperFaktura.Request.Client;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
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

        public async Task<PagedResponse> Get(Filter filter, bool listInfo = true)
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

        public async Task<Response<ListItem>> Save(Request.Client.Client client)
        {
            var result = await superFaktura.Post("clients/create", new { Client = client }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ListItem>>(result);
        }

        public async Task<Response<Response.Client.ContactPerson>> AddContactPerson(Request.Client.ContactPerson person)
        {
            var result = await superFaktura.Post("/contact_people/add/api:1", new { ContactPerson = person }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<Response.Client.ContactPerson>>(result);
        }
    }
}
