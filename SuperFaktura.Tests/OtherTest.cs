using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class OtherTest: SuperFakturaTest
    {

        [Fact]
        public async Task TestListAccounts()
        {
            var task = await apiClient.Other.ListAccounts();
            task.ShouldNotBe(null);
            throw new NotImplementedException("Usage of ExpandoObject");
        }

        [Fact]
        public async Task TestListUserCompanies()
        {
            var task = await apiClient.Other.ListUserCompanies();
            task.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestSendSMS()
        {
            var task = await apiClient.Other.SendSMS(new Birko.SuperFaktura.Request.Other.SMS()
            {
                InvoiceID = 3,
                Text = "test"
            });
            task.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestListBankAccountMovements()
        {
            var task = await apiClient.Other.ListBankAccountMovements(new Birko.SuperFaktura.Request.Other.BankMovementFilter());
            task.ShouldNotBe(null);
        }

        [Fact]
        public async Task ListActivityLogs()
        {
            var task = await apiClient.Other.ListActivityLogs(Birko.SuperFaktura.Request.Other.DocumenType.Expense, 1363);
            task.ShouldNotBe(null);
        }
    }
}
