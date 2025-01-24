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
            var clients = await apiClient.Clients.List(new Birko.SuperFaktura.Request.Client.Filter()
            {
                PerPage = 50
            });
            if (!(clients.Items?.Any() ?? false))
            {
                return;
            }
            var client = await apiClient.Clients.View(clients.Items.Last().Client.ID.Value);
            client.ShouldNotBe(null);
            client.Client.Name.Equals("ClienTest Client");
        }

        [Fact]
        public async Task TestEdit()
        {
            var clients = await apiClient.Clients.List(new Birko.SuperFaktura.Request.Client.Filter() {
                PerPage = 50
            });
            if (!(clients.Items?.Any() ?? false))
            {
                return;
            }
            var client = await apiClient.Clients.Edit(clients.Items.Last().Client.ID.Value, new Birko.SuperFaktura.Request.Client.Client()
            {
                Name = "ClienTest Client Edit",
            });
            client.ShouldNotBe(null);
            client.Error.Equals(0);
        }

        [Fact]
        public async Task TestDelete()
        {
            var clients = await apiClient.Clients.List(new Birko.SuperFaktura.Request.Client.Filter()
            {
                PerPage = 50
            });
            if (!(clients.Items?.Any() ?? false))
            {
                return;
            }
            var client = await apiClient.Clients.Delete(clients.Items.Last().Client.ID.Value);
            client.ShouldNotBe(null);
            client.Error.Equals(0);
            client.RedirectURL.Equals("/clients");
        }
    }
}
