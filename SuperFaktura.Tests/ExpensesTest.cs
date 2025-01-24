using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class ExpensesTest : SuperFakturaTest
    {
        [Fact]
        public async Task TestList()
        {
            var expenses = await apiClient.Expenses.List(new Birko.SuperFaktura.Request.Expense.Filter() { });
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
        public async Task TestEdit()
        {
            var expense = await apiClient.Expenses.Edit(new Birko.SuperFaktura.Request.Expense.Expense()
            {
                ID = 1363,
                Name = "Foo bar2",
                Currency = "EUR",
                Amount = 14,
            });
            expense.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestView()
        {
            var expense = await apiClient.Expenses.View(1363);
            expense.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestDelete()
        {
            var expense = await apiClient.Expenses.Delete(1363);
            expense.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestAddPayment()
        {
            var payment = await apiClient.Expenses.AddPayment(new Birko.SuperFaktura.Request.Expense.Payment()
            {
                ExpenseID = 1363,
                Currency = "EUR",
                Amount = 12,
            });
            payment.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestDeletePayment()
        {
            var payment = await apiClient.Expenses.DeletePayment(1363);
            payment.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestAddRelatedItem()
        {
            var related = await apiClient.Expenses.AddRelatedItem(new Birko.SuperFaktura.Request.Expense.RelatedItem()
            {
            });
            related.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestDeleteRelatedItem()
        {
            var related = await apiClient.Expenses.DeleteRelatedItem(1363);
            related.ShouldNotBe(null);
        }
    }
}
