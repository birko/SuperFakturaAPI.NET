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
        public async Task TestListAll()
        {
            bool end = false;
            int page = 1;
            while (!end)
            {
#if DEBUG
                Console.WriteLine($"Getting Page: {page}");
#endif
                var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter()
                {
                    Page = page,
                    PerPage = 200,
                    Type = String.Join("|", Birko.SuperFaktura.Request.ValueLists.InvoiceType.Types),
                });
#if DEBUG
                Console.WriteLine($"Response Page: {invoices.Page}/{invoices.PageCount}");
#endif
                page++;
                invoices.ShouldNotBe(null);
                end = invoices.Page == invoices.PageCount;
            }
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
                InvoiceType = Birko.SuperFaktura.Request.ValueLists.InvoiceType.ProForma,
                IssuedBy = "SF tester",
                IssuedByEmail = "superfaktura@example.com",
                IssuedByWeb = "www.finstat.sk",
                IssuedByPhone = "0987654321",
                InvoiceCurrency = "EUR"
                //BankAccounts = new[] {
                //    new  Birko.SuperFaktura.Request.BankAccounts.BankAccount()
                //    {
                //        IBAN ="SK0000000000000000",
                //        BankName = "New Bank1",
                //        SWIFT = "12345"
                //    },
                //    new  Birko.SuperFaktura.Request.BankAccounts.BankAccount()
                //    {
                //        IBAN ="SK0000000000000001",
                //        BankName = "New Bank2",
                //        SWIFT = "12346"
                //    }
                //}
            };
            var items = new List<Birko.SuperFaktura.Request.Invoice.Item>(new[] {
                new Birko.SuperFaktura.Request.Invoice.Item()
                    {
                        Name = "test",
                        Description = "test",
                        Quantity =  1,
                        Unit = "ks",
                        Tax = 23,
                        UnitPrice = 1200,
                    }
            });
            var settings = new Birko.SuperFaktura.Request.Invoice.InvoiceSettings()
            {
                //BySquare = true,
                //PayPal = true,
                //OnlinePayment = true,
                //CallbackPayment = "www.finstat.sk",
            };

            var extra = new Birko.SuperFaktura.Request.Invoice.Extra()
            {

            };
            var task = await apiClient.Invoices.Add(sfinvoice, client, items.ToArray(), null, settings, null);
            task.ShouldNotBeNull();
        }

        [Fact]
        public async Task TestAddCZ()
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
                Comment = "SF unit test cz",
                Name = string.Empty,
                HeaderComment = "Za testovacie produkty CZ",
                PaymentType = Birko.SuperFaktura.Request.ValueLists.PaymentType.BankTransfer,
                InvoiceType = Birko.SuperFaktura.Request.ValueLists.InvoiceType.Regular,
                IssuedBy = "SF tester",
                IssuedByEmail = "superfaktura@example.com",
                IssuedByWeb = "www.finstat.sk",
                IssuedByPhone = "0987654321",
                InvoiceCurrency = "CZK",
                //CountryExchangeRate = 26,
                //ExchangeRate = 26,
                //BankAccounts = new[] {
                //    new  Birko.SuperFaktura.Request.BankAccounts.BankAccount()
                //    {
                //        IBAN ="SK0000000000000000",
                //        BankName = "New Bank1",
                //        SWIFT = "12345"
                //    },
                //    new  Birko.SuperFaktura.Request.BankAccounts.BankAccount()
                //    {
                //        IBAN ="SK0000000000000001",
                //        BankName = "New Bank2",
                //        SWIFT = "12346"
                //    }
                //}
            };
            var items = new List<Birko.SuperFaktura.Request.Invoice.Item>(new[] {
                new Birko.SuperFaktura.Request.Invoice.Item()
                    {
                        Name = "test",
                        Description = "test",
                        Quantity =  1,
                        Unit = "ks",
                        Tax = 23,
                        UnitPrice = 1000,
                    }
            });
            var settings = new Birko.SuperFaktura.Request.Invoice.InvoiceSettings()
            {
                //BySquare = true,
                //PayPal = true,
                //OnlinePayment = true,
                //CallbackPayment = "www.finstat.sk",
            };

            var extra = new Birko.SuperFaktura.Request.Invoice.Extra()
            {

            };
            var task = await apiClient.Invoices.Add(sfinvoice, client, items.ToArray(), null, settings, null);
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
        public async Task TestEditDoesNotAppendItems()
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
                Comment = "SF unit test edit append",
                Name = string.Empty,
                HeaderComment = "Za testovacie produkty",
                PaymentType = Birko.SuperFaktura.Request.ValueLists.PaymentType.BankTransfer,
                InvoiceType = Birko.SuperFaktura.Request.ValueLists.InvoiceType.ProForma,
                IssuedBy = "SF tester",
                IssuedByEmail = "superfaktura@example.com",
                IssuedByWeb = "www.finstat.sk",
                IssuedByPhone = "0987654321",
                InvoiceCurrency = "EUR"
            };
            var items = new[] {
                new Birko.SuperFaktura.Request.Invoice.Item()
                {
                    Name = "edit item A",
                    Description = "edit item A",
                    Quantity = 1,
                    Unit = "ks",
                    Tax = 23,
                    UnitPrice = 100,
                },
                new Birko.SuperFaktura.Request.Invoice.Item()
                {
                    Name = "edit item B",
                    Description = "edit item B",
                    Quantity = 2,
                    Unit = "ks",
                    Tax = 23,
                    UnitPrice = 200,
                }
            };

            var created = await apiClient.Invoices.Add(sfinvoice, client, items);
            created.ShouldNotBeNull();
            var invoiceId = created.Invoice.ID.Value;

            try
            {
                // re-fetch so the items carry their server-assigned IDs
                var detail = await apiClient.Invoices.View(invoiceId);
                detail.InvoiceItems.ShouldNotBeNull();
                var originalCount = detail.InvoiceItems.Length;
                originalCount.ShouldBe(items.Length);

                // edit without changing the item set
                detail.Invoice.IssuedBy = "SF tester2";
                var edited = await apiClient.Invoices.Edit(detail.Invoice, client, detail.InvoiceItems);
                edited.ShouldNotBeNull();

                // editing must swap (replace) the items, not append them
                var after = await apiClient.Invoices.View(invoiceId);
                after.InvoiceItems.ShouldNotBeNull();
                after.InvoiceItems.Length.ShouldBe(originalCount);
            }
            finally
            {
                await apiClient.Invoices.Delete(invoiceId);
            }
        }

        [Fact]
        public async Task TestEditReplacesItems()
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
                Comment = "SF unit test edit replace",
                Name = string.Empty,
                HeaderComment = "Za testovacie produkty",
                PaymentType = Birko.SuperFaktura.Request.ValueLists.PaymentType.BankTransfer,
                InvoiceType = Birko.SuperFaktura.Request.ValueLists.InvoiceType.ProForma,
                IssuedBy = "SF tester",
                IssuedByEmail = "superfaktura@example.com",
                IssuedByWeb = "www.finstat.sk",
                IssuedByPhone = "0987654321",
                InvoiceCurrency = "EUR"
            };
            var items = new[] {
                new Birko.SuperFaktura.Request.Invoice.Item()
                {
                    Name = "edit item A",
                    Description = "edit item A",
                    Quantity = 1,
                    Unit = "ks",
                    Tax = 23,
                    UnitPrice = 100,
                },
                new Birko.SuperFaktura.Request.Invoice.Item()
                {
                    Name = "edit item B",
                    Description = "edit item B",
                    Quantity = 2,
                    Unit = "ks",
                    Tax = 23,
                    UnitPrice = 200,
                }
            };

            var created = await apiClient.Invoices.Add(sfinvoice, client, items);
            created.ShouldNotBeNull();
            var invoiceId = created.Invoice.ID.Value;

            try
            {
                // re-fetch so the items carry their server-assigned IDs
                var detail = await apiClient.Invoices.View(invoiceId);
                detail.InvoiceItems.ShouldNotBeNull();
                detail.InvoiceItems.Length.ShouldBe(items.Length);

                // keep A (with its ID), drop B, add a brand new C (no ID)
                var keepA = detail.InvoiceItems.Single(x => x.Name == "edit item A");
                var newC = new Birko.SuperFaktura.Request.Invoice.Item()
                {
                    Name = "replacement item C",
                    Description = "replacement item C",
                    Quantity = 3,
                    Unit = "ks",
                    Tax = 23,
                    UnitPrice = 300,
                };
                var replacement = new Birko.SuperFaktura.Request.Invoice.Item[] { keepA, newC };

                var edited = await apiClient.Invoices.Edit(detail.Invoice, client, replacement);
                edited.ShouldNotBeNull();

                // Known API behaviour (reported to SuperFaktura, unanswered): Edit updates items with
                // an ID and adds items without one, but keeps items that were left out (B stays).
                // If this assertion starts failing, the API began replacing items on Edit.
                var afterEdit = await apiClient.Invoices.View(invoiceId);
                afterEdit.InvoiceItems.Select(x => x.Name).OrderBy(x => x).ShouldBe(new[] { "edit item A", "edit item B", "replacement item C" });

                // Replacing the item set therefore needs an explicit delete of the left-out items.
                var keptIds = replacement.Where(x => x.ID.HasValue).Select(x => x.ID.Value).ToArray();
                var leftOut = afterEdit.InvoiceItems.Where(x => x.Name != newC.Name && !keptIds.Contains(x.ID)).Select(x => x.ID).ToArray();
                await apiClient.Invoices.DeleteItem(invoiceId, leftOut);

                // ends up with exactly the replacement set: A and C, no B
                var after = await apiClient.Invoices.View(invoiceId);
                after.InvoiceItems.ShouldNotBeNull();
                after.InvoiceItems.Length.ShouldBe(replacement.Length);
                var names = after.InvoiceItems.Select(x => x.Name).ToArray();
                names.ShouldContain("edit item A");
                names.ShouldContain("replacement item C");
                names.ShouldNotContain("edit item B");
            }
            finally
            {
                await apiClient.Invoices.Delete(invoiceId);
            }
        }

        [Fact]
        public async Task TestEditDoesNotDuplicateTags()
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
                Comment = "SF unit test edit tags duplicate",
                Name = string.Empty,
                HeaderComment = "Za testovacie produkty",
                PaymentType = Birko.SuperFaktura.Request.ValueLists.PaymentType.BankTransfer,
                InvoiceType = Birko.SuperFaktura.Request.ValueLists.InvoiceType.ProForma,
                IssuedBy = "SF tester",
                IssuedByEmail = "superfaktura@example.com",
                IssuedByWeb = "www.finstat.sk",
                IssuedByPhone = "0987654321",
                InvoiceCurrency = "EUR"
            };
            var items = new[] {
                new Birko.SuperFaktura.Request.Invoice.Item()
                {
                    Name = "tag test item",
                    Description = "tag test item",
                    Quantity = 1,
                    Unit = "ks",
                    Tax = 23,
                    UnitPrice = 100,
                }
            };

            var tagA = await apiClient.Tags.Add(new Birko.SuperFaktura.Request.Tags.Tag() { Name = "edit tag A" });
            var tagB = await apiClient.Tags.Add(new Birko.SuperFaktura.Request.Tags.Tag() { Name = "edit tag B" });
            var tagIds = new[] { tagA.ID, tagB.ID };

            int? invoiceId = null;
            try
            {
                var created = await apiClient.Invoices.Add(sfinvoice, client, items, tagIds);
                created.ShouldNotBeNull();
                invoiceId = created.Invoice.ID.Value;

                var detail = await apiClient.Invoices.View(invoiceId.Value);
                detail.Tag.ShouldNotBeNull();
                var originalCount = detail.Tag.Length;
                originalCount.ShouldBe(tagIds.Length);

                // edit without changing the tag set
                detail.Invoice.IssuedBy = "SF tester2";
                var edited = await apiClient.Invoices.Edit(detail.Invoice, client, detail.InvoiceItems, tagIds);
                edited.ShouldNotBeNull();

                // editing must swap (replace) the tags, not append them
                var after = await apiClient.Invoices.View(invoiceId.Value);
                after.Tag.ShouldNotBeNull();
                after.Tag.Length.ShouldBe(originalCount);
            }
            finally
            {
                if (invoiceId.HasValue)
                {
                    await apiClient.Invoices.Delete(invoiceId.Value);
                }
                await apiClient.Tags.Delete(tagA.ID);
                await apiClient.Tags.Delete(tagB.ID);
            }
        }

        [Fact]
        public async Task TestEditReplacesTags()
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
                Comment = "SF unit test edit tags replace",
                Name = string.Empty,
                HeaderComment = "Za testovacie produkty",
                PaymentType = Birko.SuperFaktura.Request.ValueLists.PaymentType.BankTransfer,
                InvoiceType = Birko.SuperFaktura.Request.ValueLists.InvoiceType.ProForma,
                IssuedBy = "SF tester",
                IssuedByEmail = "superfaktura@example.com",
                IssuedByWeb = "www.finstat.sk",
                IssuedByPhone = "0987654321",
                InvoiceCurrency = "EUR"
            };
            var items = new[] {
                new Birko.SuperFaktura.Request.Invoice.Item()
                {
                    Name = "tag test item",
                    Description = "tag test item",
                    Quantity = 1,
                    Unit = "ks",
                    Tax = 23,
                    UnitPrice = 100,
                }
            };

            var tagA = await apiClient.Tags.Add(new Birko.SuperFaktura.Request.Tags.Tag() { Name = "replace tag A" });
            var tagB = await apiClient.Tags.Add(new Birko.SuperFaktura.Request.Tags.Tag() { Name = "replace tag B" });
            var tagC = await apiClient.Tags.Add(new Birko.SuperFaktura.Request.Tags.Tag() { Name = "replace tag C" });

            int? invoiceId = null;
            try
            {
                var created = await apiClient.Invoices.Add(sfinvoice, client, items, new[] { tagA.ID, tagB.ID });
                created.ShouldNotBeNull();
                invoiceId = created.Invoice.ID.Value;

                var detail = await apiClient.Invoices.View(invoiceId.Value);
                detail.Tag.ShouldNotBeNull();
                detail.Tag.Length.ShouldBe(2);

                // keep A, drop B, add C
                var replacement = new[] { tagA.ID, tagC.ID };
                var edited = await apiClient.Invoices.Edit(detail.Invoice, client, detail.InvoiceItems, replacement);
                edited.ShouldNotBeNull();

                // editing must end up with exactly the replacement set: A and C, no B
                var after = await apiClient.Invoices.View(invoiceId.Value);
                after.Tag.ShouldNotBeNull();
                after.Tag.Length.ShouldBe(replacement.Length);
                var ids = after.Tag.Select(x => x.ID).ToArray();
                ids.ShouldContain(tagA.ID);
                ids.ShouldContain(tagC.ID);
                ids.ShouldNotContain(tagB.ID);
            }
            finally
            {
                if (invoiceId.HasValue)
                {
                    await apiClient.Invoices.Delete(invoiceId.Value);
                }
                await apiClient.Tags.Delete(tagA.ID);
                await apiClient.Tags.Delete(tagB.ID);
                await apiClient.Tags.Delete(tagC.ID);
            }
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
        public async Task TestMarkAsSent()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter() { PerPage = 200 });
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var detail = invoices?.Items?.First();
            var task = await apiClient.Invoices.MarkAsSent(detail.Invoice.ID.Value);
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
            // ensure there is a payment to delete so the delete response is actually exercised
            var added = await apiClient.Invoices.AddPayment(new Birko.SuperFaktura.Request.Invoice.Payment()
            {
                InvoiceID = detail.Invoice.ID.Value,
                Amount = 0.5m,
            });
            added.ShouldNotBeNull();
            added.PaymentID.ShouldNotBeNull();

            var task = await apiClient.Invoices.DeletePayment(added.PaymentID.Value);
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

        [Fact]
        public async Task TestDownload()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter() { PerPage = 200 });
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var detail = invoices?.Items?.First();
            var bytes = await apiClient.Invoices.Download(detail.Invoice.ID.Value, detail.Invoice.Token);
            bytes.ShouldNotBeEmpty();
            System.IO.File.WriteAllBytes("invoicenone.pdf", bytes);
            bytes = await apiClient.Invoices.Download(detail.Invoice.ID.Value, detail.Invoice.Token, Birko.SuperFaktura.Request.ValueLists.LanguageType.Slovak, true, false, false);
            bytes.ShouldNotBeEmpty();
            System.IO.File.WriteAllBytes("invoicesignature.pdf", bytes);
            bytes = await apiClient.Invoices.Download(detail.Invoice.ID.Value, detail.Invoice.Token, Birko.SuperFaktura.Request.ValueLists.LanguageType.Slovak, false, true, false);
            bytes.ShouldNotBeEmpty();
            System.IO.File.WriteAllBytes("invoicebySquare.pdf", bytes);
            bytes = await apiClient.Invoices.Download(detail.Invoice.ID.Value, detail.Invoice.Token, Birko.SuperFaktura.Request.ValueLists.LanguageType.Slovak, false, false, true);
            bytes.ShouldNotBeEmpty();
            System.IO.File.WriteAllBytes("invoicepaypal.pdf", bytes);
            bytes = await apiClient.Invoices.Download(detail.Invoice.ID.Value, detail.Invoice.Token, Birko.SuperFaktura.Request.ValueLists.LanguageType.Slovak, false, false, false);
            bytes.ShouldNotBeEmpty();
            System.IO.File.WriteAllBytes("invoiceblank.pdf", bytes);
            bytes = await apiClient.Invoices.Download(detail.Invoice.ID.Value, detail.Invoice.Token, Birko.SuperFaktura.Request.ValueLists.LanguageType.Slovak, true, true, true);
            bytes.ShouldNotBeEmpty();
            System.IO.File.WriteAllBytes("invoiceall.pdf", bytes);
        }

        [Fact]
        public async Task TestDownloadReceipt()
        {
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter() { PerPage = 200 });
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var detail = invoices?.Items?.First();
            var bytes = await apiClient.Invoices.DownloadReceipt(detail.Invoice.ID.Value);
            bytes.ShouldNotBeEmpty();
            System.IO.File.WriteAllBytes("invoicereceipt.pdf", bytes);
        }
    }
}
