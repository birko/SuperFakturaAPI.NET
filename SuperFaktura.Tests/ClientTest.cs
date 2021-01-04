using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class ClientTest
    {
        private Birko.SuperFaktura.SuperFaktura apiClient = null;

        public ClientTest()
        {
            LaunchSettingsFixture.Load();
            var superFakturaApiEmail = System.Environment.GetEnvironmentVariable("SuperFakturaApiEmail");
            var superFakturaApiKey = System.Environment.GetEnvironmentVariable("SuperFakturaApiKey");
            this.apiClient = new Birko.SuperFaktura.SuperFaktura(superFakturaApiEmail, superFakturaApiKey);
        }
        [Fact]
        public async Task TestInvoices()
        {
            var invoices = await this.apiClient.Invoices.Get(new Birko.SuperFaktura.Request.Invoice.Filter());
            invoices.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestClients()
        {
            var clients = await this.apiClient.Clients.Get(new Birko.SuperFaktura.Request.Client.Filter());
            clients.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestCountries()
        {
            var countries = await this.apiClient.GetCountries();
            countries.Count.ShouldBe(251);
            countries[191].ShouldBe("Slovensko");
        }
    }
}
