using Birko.SuperFaktura.Request.ContactPersons;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.ContactPersons;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
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

        public async Task<Response<Response.ContactPersons.ContactPerson>> Add(Request.ContactPersons.ContactPerson person)
        {
            var result = await superFaktura.Post("/contact_people/add/api:1", new ContactPersonData { ContactPerson = person }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<Response.ContactPersons.ContactPerson>>(result);
        }

        public async Task<Response<Response.ContactPersons.ContactPerson[]>> List(int id)
        {
            var result = await superFaktura.Get("/contact_people/getContactPeople/" + id).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<Response.ContactPersons.ContactPerson[]>>(result);
        }

        public async Task<Response<ExpandoObject>> Delete(int id)
        {
            var result = await superFaktura.Get("/contact_people/delete/" +  id).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }
    }
}
