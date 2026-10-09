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
                    Type = Birko.SuperFaktura.Request.ValueLists.InvoiceType.Types,
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
        public async Task TestListFiltersByMultipleValues()
        {
            // two invoices of one new client differing in type, payment type and delivery type;
            // client_id keeps the results to this test's data
            var first = await CreateTestInvoice("sf-test-multi");
            var clientId = first.Invoice.ClientID.Value;
            var second = await apiClient.Invoices.Add(
                new Birko.SuperFaktura.Request.Invoice.Invoice
                {
                    Name = UniqueName("sf-test-multi"),
                    InvoiceType = Birko.SuperFaktura.Request.ValueLists.InvoiceType.ProForma,
                    PaymentType = Birko.SuperFaktura.Request.ValueLists.PaymentType.Cash,
                    DeliveryType = Birko.SuperFaktura.Request.ValueLists.DeliveryType.Courier,
                },
                new Birko.SuperFaktura.Request.Client.Client { ID = clientId },
                new[] { new Birko.SuperFaktura.Request.Invoice.Item { Name = "test item", Quantity = 1, UnitPrice = 10, Tax = 20 } });
            try
            {
                await apiClient.Invoices.Edit(new Birko.SuperFaktura.Request.Invoice.Invoice
                {
                    ID = first.Invoice.ID,
                    PaymentType = Birko.SuperFaktura.Request.ValueLists.PaymentType.BankTransfer,
                    DeliveryType = Birko.SuperFaktura.Request.ValueLists.DeliveryType.Mail,
                });
                var firstId = first.Invoice.ID.Value;
                var secondId = second.Invoice.ID.Value;
                var bothTypes = new[] { Birko.SuperFaktura.Request.ValueLists.InvoiceType.Regular, Birko.SuperFaktura.Request.ValueLists.InvoiceType.ProForma };

                async Task<int[]> ListIds(Birko.SuperFaktura.Request.Invoice.Filter filter)
                {
                    filter.ClientId = clientId;
                    filter.PerPage = 200;
                    var list = await apiClient.Invoices.List(filter);
                    return (list?.Items ?? Enumerable.Empty<Birko.SuperFaktura.Response.Invoice.Detail>())
                        .Select(x => x.Invoice.ID.Value).OrderBy(x => x).ToArray();
                }

                (await ListIds(new Birko.SuperFaktura.Request.Invoice.Filter { Type = bothTypes }))
                    .ShouldBe(new[] { firstId, secondId });
                (await ListIds(new Birko.SuperFaktura.Request.Invoice.Filter { Type = new[] { Birko.SuperFaktura.Request.ValueLists.InvoiceType.ProForma } }))
                    .ShouldBe(new[] { secondId });

                (await ListIds(new Birko.SuperFaktura.Request.Invoice.Filter
                {
                    Type = bothTypes,
                    PaymentType = new[] { Birko.SuperFaktura.Request.ValueLists.PaymentType.BankTransfer, Birko.SuperFaktura.Request.ValueLists.PaymentType.Cash },
                })).ShouldBe(new[] { firstId, secondId });
                (await ListIds(new Birko.SuperFaktura.Request.Invoice.Filter
                {
                    Type = bothTypes,
                    PaymentType = new[] { Birko.SuperFaktura.Request.ValueLists.PaymentType.Cash },
                })).ShouldBe(new[] { secondId });

                // delivery_type takes a single value only (see Filter.DeliveryType)
                (await ListIds(new Birko.SuperFaktura.Request.Invoice.Filter
                {
                    Type = bothTypes,
                    DeliveryType = Birko.SuperFaktura.Request.ValueLists.DeliveryType.Mail,
                })).ShouldBe(new[] { firstId });

                (await ListIds(new Birko.SuperFaktura.Request.Invoice.Filter { Type = bothTypes, Ignore = new[] { firstId } }))
                    .ShouldBe(new[] { secondId });
                (await ListIds(new Birko.SuperFaktura.Request.Invoice.Filter { Type = bothTypes, Ignore = new[] { firstId, secondId } }))
                    .ShouldBeEmpty();
            }
            finally
            {
                await apiClient.Invoices.Delete(second.Invoice.ID.Value);
                await DeleteTestInvoice(first);
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
            try
            {
                task.ShouldNotBeNull();
                task.Invoice.ID.ShouldNotBeNull();
            }
            finally
            {
                // the client (matched by ICO) is shared, only the invoice is removed
                await apiClient.Invoices.Delete(task.Invoice.ID.Value);
            }
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
            try
            {
                task.ShouldNotBeNull();
                task.Invoice.ID.ShouldNotBeNull();
            }
            finally
            {
                // the client (matched by ICO) is shared, only the invoice is removed
                await apiClient.Invoices.Delete(task.Invoice.ID.Value);
            }
        }

        [Fact]
        public async Task TestEdit()
        {
            // round trip: the invoice detail read from the API is sent back with one change
            var created = await CreateTestInvoice();
            try
            {
                var detail = await apiClient.Invoices.View(created.Invoice.ID.Value);
                detail.Invoice.IssuedBy = "SF tester2";
                var task = await apiClient.Invoices.Edit(detail.Invoice, detail.Client, detail.InvoiceItems);
                task.ShouldNotBeNull();
                task.Invoice.IssuedBy.ShouldBe("SF tester2");
                task.InvoiceItems.Length.ShouldBe(created.InvoiceItems.Length);
            }
            finally
            {
                await DeleteTestInvoice(created);
            }
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

            var tagA = await apiClient.Tags.Add(new Birko.SuperFaktura.Request.Tags.Tag() { Name = UniqueName("edit tag A") });
            var tagB = await apiClient.Tags.Add(new Birko.SuperFaktura.Request.Tags.Tag() { Name = UniqueName("edit tag B") });
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

            var tagA = await apiClient.Tags.Add(new Birko.SuperFaktura.Request.Tags.Tag() { Name = UniqueName("replace tag A") });
            var tagB = await apiClient.Tags.Add(new Birko.SuperFaktura.Request.Tags.Tag() { Name = UniqueName("replace tag B") });
            var tagC = await apiClient.Tags.Add(new Birko.SuperFaktura.Request.Tags.Tag() { Name = UniqueName("replace tag C") });

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
            var detail = await CreateTestInvoice();
            try
            {
                var task = await apiClient.Invoices.View(detail.Invoice.ID.Value);
                task.ShouldNotBeNull();
                task.Invoice.ID.ShouldBe(detail.Invoice.ID);
            }
            finally
            {
                await DeleteTestInvoice(detail);
            }
        }

        [Fact]
        public async Task TestSetInvoiceLanguage()
        {
            var detail = await CreateTestInvoice();
            try
            {
                var task = await apiClient.Invoices.SetInvoiceLanguage(detail.Invoice.ID.Value, Birko.SuperFaktura.Request.ValueLists.LanguageType.German);
                task.ShouldNotBeNull();
                (await apiClient.Invoices.View(detail.Invoice.ID.Value)).InvoiceSetting.Language.ShouldBe(Birko.SuperFaktura.Request.ValueLists.LanguageType.German);
            }
            finally
            {
                await DeleteTestInvoice(detail);
            }
        }

        [Fact]
        public async Task TestWillNotBePaid()
        {
            var detail = await CreateTestInvoice();
            try
            {
                var task = await apiClient.Invoices.WillNotBePaid(detail.Invoice.ID.Value);
                task.ShouldNotBeNull();
            }
            finally
            {
                await DeleteTestInvoice(detail);
            }
        }

        [Fact]
        public async Task TestSendEmail()
        {
            var detail = await CreateTestInvoice();
            try
            {
                // error 11: "Posielanie e-mailov je vypnuté. Nastavte si vlastné SMTP." - no SMTP on the sandbox account
                var task = await AllowSandboxLimit(() => apiClient.Invoices.SendEmail(new Birko.SuperFaktura.Request.Invoice.Email()
                {
                    InvoiceID = detail.Invoice.ID.Value,
                    To = "recipient@example.com",
                    Subject = "test",
                }), 11);
                task?.InvoiceID.ShouldBe(detail.Invoice.ID.Value);
            }
            finally
            {
                await DeleteTestInvoice(detail);
            }
        }

        [Fact]
        public async Task TestMarkAsSentViaMail()
        {
            var detail = await CreateTestInvoice();
            try
            {
                var task = await apiClient.Invoices.MarkAsSentViaMail(new Birko.SuperFaktura.Request.Invoice.MarkEmail() {
                    InvoiceID  = detail.Invoice.ID.Value,
                    EmailAddres = "marked@example.com",
                    Message= "test",
                    Subject = "test",
                });
                task.ShouldNotBeNull();
            }
            finally
            {
                await DeleteTestInvoice(detail);
            }
        }

        [Fact]
        public async Task TestSendPost()
        {
            var detail = await CreateTestInvoice();
            try
            {
                // error 7: "No post stamps left." - the sandbox account has no post stamp credit
                var task = await AllowSandboxLimit(() => apiClient.Invoices.SendPost(new Birko.SuperFaktura.Request.Invoice.Post() {
                    InvoiceID = detail.Invoice.ID.Value,
                }), 7);
                task?.Invoice.ID.ShouldBe(detail.Invoice.ID);
            }
            finally
            {
                await DeleteTestInvoice(detail);
            }
        }

        [Fact]
        public async Task TestMarkAsSent()
        {
            var detail = await CreateTestInvoice();
            try
            {
                var task = await apiClient.Invoices.MarkAsSent(detail.Invoice.ID.Value);
                task.ShouldNotBeNull();
            }
            finally
            {
                await DeleteTestInvoice(detail);
            }
        }

        [Fact]
        public async Task TestDeleteItem()
        {
            var detail = await CreateTestInvoice();
            try
            {
                // an invoice keeps at least one item, so add a second one to delete
                await apiClient.Invoices.Edit(new Birko.SuperFaktura.Request.Invoice.Invoice { ID = detail.Invoice.ID }, items: new[]
                {
                    new Birko.SuperFaktura.Request.Invoice.Item { Name = "item to delete", Quantity = 1, UnitPrice = 5, Tax = 20 }
                });
                var view = await apiClient.Invoices.View(detail.Invoice.ID.Value);
                var toDelete = view.InvoiceItems.Single(x => x.Name == "item to delete");

                var task = await apiClient.Invoices.DeleteItem(detail.Invoice.ID.Value, toDelete.ID);
                task.ShouldNotBeNull();
                (await apiClient.Invoices.View(detail.Invoice.ID.Value)).InvoiceItems.ShouldNotContain(x => x.ID == toDelete.ID);
            }
            finally
            {
                await DeleteTestInvoice(detail);
            }
        }

        [Fact]
        public async Task TestAddPayment()
        {
            var detail = await CreateTestInvoice();
            try
            {
                var task = await apiClient.Invoices.AddPayment(new Birko.SuperFaktura.Request.Invoice.Payment() {
                    InvoiceID = detail.Invoice.ID.Value,
                    Amount = 0.5m,
                });
                task.ShouldNotBeNull();
                task.PaymentID.ShouldNotBeNull();
            }
            finally
            {
                await DeleteTestInvoice(detail);
            }
        }

        [Fact]
        public async Task TestDeletePayment()
        {
            var detail = await CreateTestInvoice();
            try
            {
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
            finally
            {
                await DeleteTestInvoice(detail);
            }
        }

        private static async Task<Birko.SuperFaktura.Response.Expense.Detail> AddTestExpense()
        {
            return await apiClient.Expenses.Add(new Birko.SuperFaktura.Request.Expense.Expense() { Name = UniqueName("related expense"), Currency = "EUR", Amount = 1 });
        }

        [Fact]
        public async Task TestAddRelatedItem()
        {
            var invoice = await CreateTestInvoice();
            var expense = await AddTestExpense();
            try
            {
                var task = await apiClient.Invoices.AddRelatedItem(new Birko.SuperFaktura.Request.RelatedItem() {
                    ParentID =  invoice.Invoice.ID.Value,
                    ParentType = Birko.SuperFaktura.Request.ValueLists.DocumentType.Invoice,
                    ChildID = expense.Expense.ID.Value,
                    ChildType = Birko.SuperFaktura.Request.ValueLists.DocumentType.Expense
                });
                task.ShouldNotBeNull();
                (await apiClient.Invoices.View(invoice.Invoice.ID.Value)).RelatedItems.ShouldNotBeEmpty();
            }
            finally
            {
                await apiClient.Expenses.Delete(expense.Expense.ID.Value);
                await DeleteTestInvoice(invoice);
            }
        }

        [Fact]
        public async Task TestDeleteRelatedItem()
        {
            var invoice = await CreateTestInvoice();
            var expense = await AddTestExpense();
            try
            {
                await apiClient.Invoices.AddRelatedItem(new Birko.SuperFaktura.Request.RelatedItem() {
                    ParentID =  invoice.Invoice.ID.Value,
                    ParentType = Birko.SuperFaktura.Request.ValueLists.DocumentType.Invoice,
                    ChildID = expense.Expense.ID.Value,
                    ChildType = Birko.SuperFaktura.Request.ValueLists.DocumentType.Expense
                });
                var view = await apiClient.Invoices.View(invoice.Invoice.ID.Value);

                var task = await apiClient.Invoices.DeleteRelatedItem(view.RelatedItems.Single().RelationID);
                task.ShouldNotBeNull();
                (await apiClient.Invoices.View(invoice.Invoice.ID.Value)).RelatedItems?.ShouldBeEmpty();
            }
            finally
            {
                await apiClient.Expenses.Delete(expense.Expense.ID.Value);
                await DeleteTestInvoice(invoice);
            }
        }

        [Fact]
        public async Task TestDelete()
        {
            // an invoice of its own: deleting the first listed invoice destroyed other tests' data
            var detail = await CreateTestInvoice();
            var task = await apiClient.Invoices.Delete(detail.Invoice.ID.Value);
            task.ShouldNotBeNull();
            task.Error.ShouldBe(0);
            await apiClient.Clients.Delete(detail.Invoice.ClientID.Value);
        }

        [Fact]
        public async Task TestDownload()
        {
            var detail = await CreateTestInvoice();
            try
            {
                var id = detail.Invoice.ID.Value;
                var token = detail.Invoice.Token;
                var slovak = Birko.SuperFaktura.Request.ValueLists.LanguageType.Slovak;
                (await apiClient.Invoices.Download(id, token)).ShouldNotBeEmpty();
                (await apiClient.Invoices.Download(id, token, slovak, true, false, false)).ShouldNotBeEmpty();
                (await apiClient.Invoices.Download(id, token, slovak, false, true, false)).ShouldNotBeEmpty();
                (await apiClient.Invoices.Download(id, token, slovak, false, false, true)).ShouldNotBeEmpty();
                (await apiClient.Invoices.Download(id, token, slovak, false, false, false)).ShouldNotBeEmpty();
                (await apiClient.Invoices.Download(id, token, slovak, true, true, true)).ShouldNotBeEmpty();
            }
            finally
            {
                await DeleteTestInvoice(detail);
            }
        }

        [Fact]
        public async Task TestDownloadReceipt()
        {
            var detail = await CreateTestInvoice();
            try
            {
                var bytes = await apiClient.Invoices.DownloadReceipt(detail.Invoice.ID.Value);
                bytes.ShouldNotBeEmpty();
            }
            finally
            {
                await DeleteTestInvoice(detail);
            }
        }
    }
}
