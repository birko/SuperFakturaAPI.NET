using Shouldly;
using System;
using System.Collections.Generic;
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
            this.apiClient = new Birko.SuperFaktura.SuperFakturaSandbox(superFakturaApiEmail, superFakturaApiKey);
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

        [Fact]
        public async Task TestInvoice()
        {
            var now = DateTime.Now.Date;
            var client = new Birko.SuperFaktura.Request.Client.Client()
            {
                BankAccount = string.Empty,
                Name = "Ing. František Bereò",
                ICO = "47883421",
                DIC = "2020202020",
                ICDPH = string.Empty,
                Email = string.Empty,
                Phone = string.Empty,
                Address = "Vasi¾ov 116",
                City = "Vasi¾ov",
                ZIP = "02951",
                Country = "Slovensko",
                CountryID = 191,
                CountryISOID = "sk",
            };
            var sfinvoice = new Birko.SuperFaktura.Request.Invoice.Invoice()
            {
                Constant = "308",
                Created = now,
                DueDate = now.AddDays(14),
                Comment = "SF unit test",
                Name = string.Empty,
                HeaderComment = "Za testovacie produkty",
                PaymentType = Birko.SuperFaktura.Request.Invoice.PaymentType.BankTransfer,
                InvoiceType = Birko.SuperFaktura.Request.Invoice.Type.Regular,
                IssuedBy = "SF tester",
                IssuedByEmail = "superfaktura@example.com",
                IssuedByWeb = "www.finstat.sk",
                IssuedByPhone = "0987654321",
            };
            var items = new List<Birko.SuperFaktura.Request.Invoice.Item>(new[] {
                new Birko.SuperFaktura.Request.Invoice.Item()
                    {
                        Name = "test",
                        Description = "test",
                        Quantity =  1,
                        Unit = "ks",
                        Tax = 20,
                        UnitPrice = 1,
                    }
            });
            var task = await this.apiClient.Invoices.Save(sfinvoice, client, items.ToArray());
            task.Error.ShouldBe(0);
        }
    }
}
