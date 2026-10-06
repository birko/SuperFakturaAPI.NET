using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class ContactPersonsTest : SuperFakturaTest
    {
        [Fact]
        public async Task TestGet()
        {
            var persons = await apiClient.ContactPersons.List(7621);
            persons.ShouldNotBe(null);
            persons.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task TestAdd()
        {
            var client = await apiClient.ContactPersons.Add(new Birko.SuperFaktura.Request.ContactPersons.ContactPerson()
            {
                ClientID = 7621,
                Name = "Test",
                Email = "test@example.com"
            });
            client.ShouldNotBe(null);
        }


        [Fact]
        public async Task TestDelete()
        {
            var persons = await apiClient.ContactPersons.List(7621);
            if (!(persons?.Any() ?? false))
            {
                return;
            }
            var client = await apiClient.ContactPersons.Delete(persons.First().ID.Value);
            client.ShouldNotBe(null);
            client.Error.Equals(0);
        }
    }
}
