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
            var url = string.Format("clients/index.json{0}", filter.ToParameters(listInfo));
            if (listInfo)
            {
                return JsonConvert.DeserializeObject<PagedResponse>(await superFaktura.Get(url));
            }
            else
            {
                return new PagedResponse { Items = JsonConvert.DeserializeObject<ItemList<ListItem>>(await superFaktura.Get(url)) };
            }
        }

        public async Task<Response<ListItem>> Save(Request.Client.Client client)
        {
            return JsonConvert.DeserializeObject<Response<ListItem>>(await superFaktura.Post("clients/create", new { Client = client }));
        }

        public async Task<Response<Response.Client.ContactPerson>> AddContactPerson(Request.Client.ContactPerson person)
        {
            return JsonConvert.DeserializeObject<Response<Response.Client.ContactPerson>>(await superFaktura.Post("/contact_people/add/api:1", new { ContactPerson = person }));
        }
    }
}
