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
        private readonly SuperFaktura superFaktura;

        public Clients(SuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<PagedResponse> Get(Filter filter, bool listInfo = true)
        {
            var result = await superFaktura.Get(string.Format("clients/index.json{0}", filter.ToParameters(listInfo))).ConfigureAwait(false);
            if (listInfo)
            {
                return JsonConvert.DeserializeObject<PagedResponse>(result);
            }
            else
            {
                return new PagedResponse { Items = JsonConvert.DeserializeObject<ItemList<ListItem>>(result) };
            }
        }

        public async Task<Response<ListItem>> Save(Request.Client.Client client)
        {
            var result = await superFaktura.Post("clients/create", new { Client = client }).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Response<ListItem>>(result);
        }

        public async Task<Response<Response.Client.ContactPerson>> AddContactPerson(Request.Client.ContactPerson person)
        {
            var result = await superFaktura.Post("/contact_people/add/api:1", new { ContactPerson = person }).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Response<Response.Client.ContactPerson>>(result);
        }
    }
}
