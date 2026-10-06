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
        private static Birko.SuperFaktura.Request.ContactPersons.ContactPerson Person(int clientId)
        {
            return new Birko.SuperFaktura.Request.ContactPersons.ContactPerson()
            {
                ClientID = clientId,
                Name = "Test",
                Email = "test@example.com"
            };
        }

        [Fact]
        public async Task TestGet()
        {
            var clientId = await CreateTestClient();
            try
            {
                await apiClient.ContactPersons.Add(Person(clientId));
                var persons = await apiClient.ContactPersons.List(clientId);
                persons.ShouldNotBe(null);
                persons.ShouldNotBeEmpty();
            }
            finally
            {
                await apiClient.Clients.Delete(clientId);
            }
        }

        [Fact]
        public async Task TestAdd()
        {
            var clientId = await CreateTestClient();
            try
            {
                var person = await apiClient.ContactPersons.Add(Person(clientId));
                person.ShouldNotBe(null);
                person.ClientID.ShouldBe(clientId);
                person.Email.ShouldBe("test@example.com");
            }
            finally
            {
                await apiClient.Clients.Delete(clientId);
            }
        }

        [Fact]
        public async Task TestDelete()
        {
            var clientId = await CreateTestClient();
            try
            {
                var person = await apiClient.ContactPersons.Add(Person(clientId));
                var result = await apiClient.ContactPersons.Delete(person.ID.Value);
                result.ShouldNotBe(null);
                result.Error.ShouldBe(0);
                (await apiClient.ContactPersons.List(clientId)).ShouldNotContain(p => p.ID == person.ID);
            }
            finally
            {
                await apiClient.Clients.Delete(clientId);
            }
        }
    }
}
