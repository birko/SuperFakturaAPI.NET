using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class InvoceTests: SuperFakturaTest
    {

        [Fact]
        public async Task TestList()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter());
            invoices.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestAdd()
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
                Address = "Vasilov 116",
                City = "Vasilov",
                ZIP = "02951",
                Country = "Slovensko",
                CountryID = 191,
            };
            var sfinvoice = new Birko.SuperFaktura.Request.Invoice.Invoice()
            {
                Constant = "308",
                Created = now,
                DueDate = now.AddDays(14),
                Comment = "SF unit test",
                Name = string.Empty,
                HeaderComment = "Za testovacie produkty",
                PaymentType = Birko.SuperFaktura.Request.ValueLists.PaymentType.BankTransfer,
                InvoiceType = Birko.SuperFaktura.Request.ValueLists.InvoiceType.Regular,
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
            var task = await apiClient.Invoices.Add(sfinvoice, client, items.ToArray());
            //task.Error.ShouldBe(0);
        }

    }
}
