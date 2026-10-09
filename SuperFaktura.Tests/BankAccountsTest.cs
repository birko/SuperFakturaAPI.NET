using Newtonsoft.Json.Linq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SuperFaktura.Tests
{
    public class BankAccountsTest: SuperFakturaTest
    {
        private readonly ITestOutputHelper output;

        public BankAccountsTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        // Own account, not default - the sandbox default bank account stays untouched.
        private static async Task<Birko.SuperFaktura.Response.BankAccounts.BankAccount> AddTestAccount()
        {
            return await apiClient.BankAccounts.Add(new Birko.SuperFaktura.Request.BankAccounts.BankAccount()
            {
                BankName = UniqueName("testBank"),
                Show = false,
                IBAN = "SK3112000000198742637541"
            });
        }

        [Fact]
        public async Task TestList()
        {
            var added = await AddTestAccount();
            try
            {
                var accounts = await apiClient.BankAccounts.List();
                accounts.ShouldNotBe(null);
                accounts.ShouldContain(x => x.ID == added.ID);
            }
            finally
            {
                await apiClient.BankAccounts.Delete(added.ID.Value);
            }
        }

        [Fact]
        public async Task TestAdd()
        {
            var account = await AddTestAccount();
            try
            {
                account.ShouldNotBe(null);
                account.IBAN.ShouldBe("SK3112000000198742637541");
                account.Show.ShouldBe(false);
            }
            finally
            {
                await apiClient.BankAccounts.Delete(account.ID.Value);
            }
        }

        [Fact]
        public async Task TestEdit()
        {
            var added = await AddTestAccount();
            try
            {
                var account = await apiClient.BankAccounts.Edit(added.ID.Value, new Birko.SuperFaktura.Request.BankAccounts.BankAccount()
                {
                    BankName = "testBankEdit",
                    Show = true,
                });
                account.ShouldNotBe(null);
                account.BankName.ShouldBe("testBankEdit");
                account.Show.ShouldBe(true);
                account.IBAN.ShouldBe("SK3112000000198742637541");
            }
            finally
            {
                await apiClient.BankAccounts.Delete(added.ID.Value);
            }
        }

        [Fact]
        public async Task TestListRawResponseFlags()
        {
            var added = await AddTestAccount();
            try
            {
                var raw = JObject.Parse(await apiClient.Get("bank_accounts/index"));
                var account = (JObject)raw["BankAccounts"]
                    .Select(x => x["BankAccount"])
                    .Single(x => x["id"].Value<string>() == added.ID.ToString());
                output.WriteLine(account.ToString());

                // Undocumented "currency" is returned (null when not set on the account);
                // "show_account" is not part of the list response.
                account.ContainsKey("currency").ShouldBeTrue();
                account["currency"].Type.ShouldBe(JTokenType.Null);
                account["show"].Type.ShouldBe(JTokenType.Boolean);
                account.ContainsKey("show_account").ShouldBeFalse();
            }
            finally
            {
                await apiClient.BankAccounts.Delete(added.ID.Value);
            }
        }

        [Fact]
        public async Task TestInvoiceMyDataBankAccountFlags()
        {
            var added = await AddTestAccount();
            Birko.SuperFaktura.Response.Invoice.Detail invoice = null;
            try
            {
                invoice = await CreateTestInvoice();
                var raw = JObject.Parse(await apiClient.Get(string.Format("invoices/view/{0}.json", invoice.Invoice.ID)));
                output.WriteLine(raw["MyData"]["BankAccount"].ToString());
                output.WriteLine(raw["Invoice"]["my_data"].ToString());

                // A newly created invoice lists every account of the profile, with "show", "show_account"
                // and "currency" - unlike older invoices whose snapshot has only "show_account" as "0"/"1".
                var account = (JObject)raw["MyData"]["BankAccount"].Single(x => x["id"].Value<string>() == added.ID.ToString());
                account["show"].Type.ShouldBe(JTokenType.Boolean);
                account["show_account"].Type.ShouldBe(JTokenType.Boolean);
                account.ContainsKey("currency").ShouldBeTrue();

                var typed = (await apiClient.Invoices.View(invoice.Invoice.ID.Value)).MyData.BankAccount.Single(x => x.ID == added.ID);
                typed.Show.ShouldBe(false);
                typed.ShowAccount.ShouldBe(false);
                typed.Currency.ShouldBeNull();
            }
            finally
            {
                if (invoice != null)
                {
                    await DeleteTestInvoice(invoice);
                }
                await apiClient.BankAccounts.Delete(added.ID.Value);
            }
        }

        [Fact]
        public async Task TestDelete()
        {
            var added = await AddTestAccount();
            var result = await apiClient.BankAccounts.Delete(added.ID.Value);
            result.ShouldNotBe(null);
            result.Error.ShouldBe(0);
            (await apiClient.BankAccounts.List()).ShouldNotContain(x => x.ID == added.ID);
        }
    }
}
