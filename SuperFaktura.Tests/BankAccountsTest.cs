using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class BankAccountsTest: SuperFakturaTest
    {
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
