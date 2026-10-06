using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class ClientsTest: SuperFakturaTest
    {
        [Fact]
        public async Task TestList()
        {
            var clients = await apiClient.Clients.List(new Birko.SuperFaktura.Request.Client.Filter());
            clients.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestAdd()
        {
            var client = await apiClient.Clients.Add(new Birko.SuperFaktura.Request.Client.Client() {
                Name = "ClienTest Client",
            });
            client.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestView()
        {
            var clientId = await CreateTestClient("ClientTest view");
            try
            {
                var client = await apiClient.Clients.View(clientId);
                client.ShouldNotBe(null);
                client.Client.ID.ShouldBe(clientId);
                client.Client.Name.ShouldStartWith("ClientTest view");
            }
            finally
            {
                await apiClient.Clients.Delete(clientId);
            }
        }

        [Fact]
        public async Task TestEdit()
        {
            var clientId = await CreateTestClient("ClientTest edit");
            try
            {
                var client = await apiClient.Clients.Edit(clientId, new Birko.SuperFaktura.Request.Client.Client()
                {
                    Name = "ClienTest Client Edit",
                });
                client.ShouldNotBe(null);
                client.Error.ShouldBe(0);
                (await apiClient.Clients.View(clientId)).Client.Name.ShouldBe("ClienTest Client Edit");
            }
            finally
            {
                await apiClient.Clients.Delete(clientId);
            }
        }

        [Fact]
        public async Task TestDelete()
        {
            // a client of its own: an existing client may have invoices and cannot be deleted
            var clientId = await CreateTestClient("ClientTest delete");
            var client = await apiClient.Clients.Delete(clientId);
            client.ShouldNotBe(null);
            client.Error.ShouldBe(0);
            client.RedirectURL.ShouldBe("/clients");
        }
    }
}
