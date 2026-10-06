using Birko.SuperFaktura.Request.ContactPersons;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.ContactPersons;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Birko.SuperFaktura
{
    public class ContactPersons
    {
        private readonly AbstractSuperFaktura superFaktura;

        public ContactPersons(AbstractSuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<IEnumerable<Response.ContactPersons.ContactPerson>> List(int id)
        {
            var result = await superFaktura.Get("contact_people/getContactPeople/" + id).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<IEnumerable<Item>>(result);
            return data.Select(x => x.ContactPerson);
        }

        public async Task<Response.ContactPersons.ContactPerson> Add(Request.ContactPersons.ContactPerson person)
        {
            var result = await superFaktura.Post("contact_people/add/api:1", new ContactPersonData { ContactPerson = person }).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<StateResponse<Item>>(result);
            return data.Data.ContactPerson;
        }

        public async Task<ErrorResponse> Delete(int id)
        {
            var result = await superFaktura.Get("contact_people/delete/" +  id).ConfigureAwait(false);
            return superFaktura.DeserializeResult<ErrorResponse>(result);
        }
    }
}
