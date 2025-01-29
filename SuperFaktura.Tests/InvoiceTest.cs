using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter(), false);
            invoices.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestListDetails()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter(), false);
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var details = await apiClient.Invoices.ListDetails(invoices.Items.Select(x => x.Invoice.ID.Value));
            details.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestAdd()
        {
            var now = DateTime.Now.Date;
            var client = new Birko.SuperFaktura.Request.Client.Client()
            {
                BankAccount = string.Empty,
                Name = "Ing. František Beren",
                ICO = "47883421",
                DIC = "2020202020",
                ICDPH = string.Empty,
                Email = string.Empty,
                Phone = string.Empty,
                Address = "Vasilov 116",
                City = "Vasilov",
                ZIP = "02951",
                CountryName = "Slovensko",
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
            var settings = new Birko.SuperFaktura.Request.Invoice.InvoiceSettings()
            {
                BySquare = true,
                PayPal = true,
                OnlinePayment = true,
                CallbackPayment = "www.finstat.sk",
            };

            var extra = new Birko.SuperFaktura.Request.Invoice.Extra()
            {

            };
            var task = await apiClient.Invoices.Add(sfinvoice, client, items.ToArray(), null, settings, extra);
            task.ShouldNotBeNull();
        }

        [Fact]
        public async Task TestEdit()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter() { PerPage = 200});
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var detail = invoices?.Items?.First();
            detail.Invoice.IssuedBy = "SF tester2";
            var task = await apiClient.Invoices.Edit(detail.Invoice, detail.Client, detail.InvoiceItems);
            task.ShouldNotBeNull();
        }

        [Fact]
        public async Task TestView()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter() { PerPage = 200 });
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var detail = invoices?.Items?.First();
            var task = await apiClient.Invoices.View(detail.Invoice.ID.Value);
            task.ShouldNotBeNull();
        }

        [Fact]
        public async Task TestSetInvoiceLanguage()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter() { PerPage = 200 });
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var detail = invoices?.Items?.First();
            var task = await apiClient.Invoices.SetInvoiceLanguage(detail.Invoice.ID.Value, Birko.SuperFaktura.Request.ValueLists.LanguageType.German);
            task.ShouldNotBeNull();
        }

        [Fact]
        public async Task TestWillNotBePaid()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter() { PerPage = 200 });
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var detail = invoices?.Items?.First();
            var task = await apiClient.Invoices.WillNotBePaid(detail.Invoice.ID.Value);
            task.ShouldNotBeNull();
        }

        [Fact]
        public async Task TestSendEmail()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter() { PerPage = 200 });
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var detail = invoices?.Items?.First();
            var task = await apiClient.Invoices.SendEmail(new Birko.SuperFaktura.Request.Invoice.Email()
            {
                InvoiceID = detail.Invoice.ID.Value,
                To = "recipient@example.com",
                Subject = "test",
            });
            task.ShouldNotBeNull();
        }

        [Fact]
        public async Task TestMarkAsSentViaMail()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter() { PerPage = 200 });
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var detail = invoices?.Items?.First();
            var task = await apiClient.Invoices.MarkAsSentViaMail(new Birko.SuperFaktura.Request.Invoice.MarkEmail() {
                InvoiceID  = detail.Invoice.ID.Value,
                EmailAddres = "marked@example.com",
                Message= "test",
                Subject = "test",
            });
            task.ShouldNotBeNull();
        }

        [Fact]
        public async Task TestSendPost()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter() { PerPage = 200 });
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var detail = invoices?.Items?.First();
            var task = await apiClient.Invoices.SendPost(new Birko.SuperFaktura.Request.Invoice.Post() {
                InvoiceID = detail.Invoice.ID.Value,
            });
            task.ShouldNotBeNull();
        }

        [Fact]
        public async Task TestMarkAsSend()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter() { PerPage = 200 });
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var detail = invoices?.Items?.First();
            var task = await apiClient.Invoices.MarkAsSend(detail.Invoice.ID.Value);
            task.ShouldNotBeNull();
        }

        [Fact]
        public async Task TestDeleteItem()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter() { PerPage = 200 });
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var detail = invoices?.Items?.First();
            if (!(detail.InvoiceItems?.Any() ?? false))
            {
                return;
            }
            var task = await apiClient.Invoices.DeleteItem(detail.Invoice.ID.Value, detail.InvoiceItems.FirstOrDefault().ID);
            task.ShouldNotBeNull();
        }

        [Fact]
        public async Task TestAddPayment()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter() { PerPage = 200 });
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var detail = invoices?.Items?.First();
            var task = await apiClient.Invoices.AddPayment(new Birko.SuperFaktura.Request.Invoice.Payment() {
                InvoiceID = detail.Invoice.ID.Value,
                Amount = 0.5m,

            });
            task.ShouldNotBeNull();
        }

        [Fact]
        public async Task TestDeletePayment()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter() { PerPage = 200 });
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var detail = invoices?.Items?.First();
            if (!(detail.InvoicePayment?.Any() ?? false))
            {
                return;
            }
            var task = await apiClient.Invoices.DeletePayment(detail.InvoicePayment.First().ID);
            task.ShouldNotBeNull();
        }

        [Fact]
        public async Task TestAddRelatedItem()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter() { PerPage = 200 });
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var invoice = invoices?.Items?.First();
            var expenses = await apiClient.Expenses.List(new Birko.SuperFaktura.Request.Expense.Filter() { });
            if (!(expenses.Items?.Any() ?? false))
            {
                return;
            }
            var task = await apiClient.Invoices.AddRelatedItem(new Birko.SuperFaktura.Request.RelatedItem() {
                ParentID =  invoice.Invoice.ID.Value,
                ParentType = "invoice",
                ChildID = expenses.Items.FirstOrDefault().Expense.ID.Value,
                ChildType = "expense"
            });
            task.ShouldNotBeNull();
        }

        [Fact]
        public async Task TestDeleteRelatedItem()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter() { PerPage = 200 });
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var invoice = invoices?.Items?.First();
            if (!(invoice.RelatedItems?.Any() ?? false))
            {
                return;
            }
            var task = await apiClient.Invoices.DeleteRelatedItem(invoice.RelatedItems.First().RelationID);
            task.ShouldNotBeNull();
        }

        [Fact]
        public async Task TestDelete()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter() { PerPage = 200 });
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var detail = invoices?.Items?.First();
            var task = await apiClient.Invoices.Delete(detail.Invoice.ID.Value);
            task.ShouldNotBeNull();
        }
    }
}
