using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class ExpensesTest : SuperFakturaTest
    {
        [Fact]
        public async Task TestGet()
        {
            var expenses = await apiClient.Expenses.Get(new Birko.SuperFaktura.Request.Expense.Filter() { });
            expenses.ShouldNotBe(null);
            expenses.Items.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task TestAdd()
        {
            var expense = await apiClient.Expenses.Add(new Birko.SuperFaktura.Request.Expense.Expense()
            {
                Name = "Foo bar",
                Currency = "EUR",
                Amount = 12,
            });
            expense.ShouldNotBe(null);
        }


        [Fact]
        public async Task TestDelete()
        {
            var persons = await apiClient.ContactPersons.List(7621);
            if (!(persons?.Any() ?? false))
            {
                return;
            }
            var client = await apiClient.ContactPersons.Delete(persons.First().ID.Value);
            client.ShouldNotBe(null);
            client.Error.Equals(0);
        }
    }
}
