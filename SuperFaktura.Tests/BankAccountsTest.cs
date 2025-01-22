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
        [Fact]
        public async Task TestList()
        {
            var accounts = await apiClient.BankAccounts.List();
            accounts.ShouldNotBe(null);
            accounts.ShouldBeEmpty();
        }

        [Fact]
        public async Task TestAdd()
        {
            var account = await apiClient.BankAccounts.Add(new Birko.SuperFaktura.Request.BankAccounts.BankAccount()
            {
                Default = true,
                BankName = "testBank",
                Show = false,
                IBAN = "SK3112000000198742637541"
            });
            account.ShouldNotBe(null);
            account.IBAN.Equals("SK3112000000198742637541");
        }

        [Fact]
        public async Task TestEdit()
        {
            var accounts = await apiClient.BankAccounts.List();
            if (!(accounts?.Any() ?? false))
            {
                return;
            }

            var account = await apiClient.BankAccounts.Edit(accounts.First().ID, new Birko.SuperFaktura.Request.BankAccounts.BankAccount()
            {
                Default = true,
                BankName = "testBankEdit",
                Show = true,
                IBAN = "SK3112000000198742637541"
            });
            account.ShouldNotBe(null);
            account.BankName.Equals("testBankEdit");
            account.Show.Equals(false);
        }

        [Fact]
        public async Task TestDelete()
        {
            var accounts = await apiClient.BankAccounts.List();
            if (!(accounts?.Any() ?? false))
            {
                return;
            }

            var account = await apiClient.BankAccounts.Delete(accounts.First().ID);
            account.ShouldNotBe(null);
            account.Error.Equals(0);
        }
    }
}
