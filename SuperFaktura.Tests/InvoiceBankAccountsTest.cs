using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SuperFaktura.Tests
{
    // How MyData.BankAccount of an invoice looks depending on the invoice currency and on whether
    // bank accounts are sent with the invoice. Creates its own accounts, so runs on sandbox only.
    public class InvoiceBankAccountsTest : SuperFakturaTest
    {
        private readonly ITestOutputHelper output;

        public InvoiceBankAccountsTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        private async Task<Birko.SuperFaktura.Response.BankAccounts.BankAccount> AddAccount(string currency, string iban)
        {
            var account = await apiClient.BankAccounts.Add(new Birko.SuperFaktura.Request.BankAccounts.BankAccount
            {
                BankName = UniqueName("testBank-" + currency),
                IBAN = iban,
                Show = true,
                Currency = currency,
            });
            output.WriteLine($"added account {account.ID} sent currency={currency} stored currency={account.Currency ?? "null"}");
            // Not documented for bank_accounts/add, but stored.
            account.Currency.ShouldBe(currency);
            return account;
        }

        private void Dump(string label, JToken bankAccounts)
        {
            output.WriteLine(label);
            foreach (JObject account in bankAccounts)
            {
                output.WriteLine("  " + string.Join(", ", account.Properties()
                    .Where(p => new[] { "id", "bank_name", "iban", "currency", "default", "show", "show_account" }.Contains(p.Name))
                    .Select(p => $"{p.Name}={p.Value.ToString(Formatting.None)}")));
            }
        }

        [Theory]
        [InlineData("EUR", "none")]
        [InlineData("CZK", "none")]
        [InlineData("CZK", "new")]
        [InlineData("CZK", "existing")]
        public async Task TestInvoiceMyDataBankAccounts(string invoiceCurrency, string sentAccounts)
        {
            apiClient.ShouldBeOfType<Birko.SuperFaktura.SuperFakturaSandbox>();

            var eur = await AddAccount("EUR", "SK3112000000198742637541");
            var czk = await AddAccount("CZK", "CZ6508000000192000145399");
            int? invoiceID = null;
            int? clientID = null;
            try
            {
                var invoice = new Birko.SuperFaktura.Request.Invoice.Invoice
                {
                    Name = UniqueName("sf-test-invoice"),
                    InvoiceCurrency = invoiceCurrency,
                };
                if (sentAccounts == "new")
                {
                    invoice.BankAccounts = new[] { new Birko.SuperFaktura.Request.BankAccounts.BankAccount { BankName = "Sent Bank", IBAN = "CZ5508000000001234567899", SWIFT = "GIBACZPX" } };
                }
                else if (sentAccounts == "existing")
                {
                    invoice.BankAccounts = new[] { new Birko.SuperFaktura.Request.BankAccounts.BankAccount { ID = czk.ID } };
                }

                var data = new Birko.SuperFaktura.Request.Invoice.InvoiceData
                {
                    Invoice = invoice,
                    Client = new Birko.SuperFaktura.Request.Client.Client { Name = UniqueName("sf-test-invoice-client") },
                    InvoiceItem = new[] { new Birko.SuperFaktura.Request.Invoice.Item { Name = "test item", Quantity = 1, UnitPrice = 10, Tax = 20 } },
                };
                var created = JObject.Parse(await apiClient.Post("invoices/create", data));
                created["error"].Value<int>().ShouldBe(0, created.ToString());
                invoiceID = created["data"]["Invoice"]["id"].Value<int>();
                clientID = created["data"]["Invoice"]["client_id"].Value<int>();

                output.WriteLine($"invoice {invoiceID} currency={invoiceCurrency} sent bank_accounts={sentAccounts}");
                Dump("create MyData.BankAccount:", created["data"]["MyData"]["BankAccount"]);

                var view = JObject.Parse(await apiClient.Get(string.Format("invoices/view/{0}.json", invoiceID)));
                Dump("view MyData.BankAccount:", view["MyData"]["BankAccount"]);
                Dump("view Invoice.my_data BankAccount:", JObject.Parse(view["Invoice"]["my_data"].Value<string>())["MyData"]["BankAccount"]);

                var accounts = view["MyData"]["BankAccount"].Cast<JObject>().ToList();
                if (sentAccounts == "none")
                {
                    // Every profile account, whatever the invoice currency; show_account copies show.
                    accounts.Single(x => x["id"].Value<string>() == eur.ID.ToString())["currency"].Value<string>().ShouldBe("EUR");
                    accounts.Single(x => x["id"].Value<string>() == czk.ID.ToString())["currency"].Value<string>().ShouldBe("CZK");
                    accounts.ShouldAllBe(x => x["show_account"].Value<bool?>() == x["show"].Value<bool?>());
                }
                else if (sentAccounts == "new")
                {
                    // Only the sent account, with just the sent fields: no id, show nor currency.
                    var account = accounts.ShouldHaveSingleItem();
                    account["iban"].Value<string>().ShouldBe("CZ5508000000001234567899");
                    account["show_account"].Value<bool>().ShouldBeTrue();
                    account.ContainsKey("id").ShouldBeFalse();
                    account.ContainsKey("show").ShouldBeFalse();
                    account.ContainsKey("currency").ShouldBeFalse();
                }
                else
                {
                    // Only the referenced profile account, with all its data.
                    var account = accounts.ShouldHaveSingleItem();
                    account["id"].Value<string>().ShouldBe(czk.ID.ToString());
                    account["currency"].Value<string>().ShouldBe("CZK");
                    account["show_account"].Value<bool>().ShouldBeTrue();
                }
            }
            finally
            {
                if (invoiceID.HasValue)
                {
                    await apiClient.Invoices.Delete(invoiceID.Value);
                }
                if (clientID.HasValue)
                {
                    await apiClient.Clients.Delete(clientID.Value);
                }
                await apiClient.BankAccounts.Delete(eur.ID.Value);
                await apiClient.BankAccounts.Delete(czk.ID.Value);
            }
        }
    }
}
