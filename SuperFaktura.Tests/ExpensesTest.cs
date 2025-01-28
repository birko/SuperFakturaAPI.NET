using Shouldly;
using System.Linq;
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
            var expenses = await apiClient.Expenses.List(new Birko.SuperFaktura.Request.Expense.Filter() { });
            if (!(expenses.Items?.Any() ?? false))
            {
                return;
            }
            var last = expenses.Items.FirstOrDefault().Expense;
            last.Name = "Foo bar Edit";
            last.Amount = 14;
            var expense = await apiClient.Expenses.Edit(last);
            expense.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestView()
        {
            var expenses = await apiClient.Expenses.List(new Birko.SuperFaktura.Request.Expense.Filter() { });
            if (!(expenses.Items?.Any() ?? false))
            {
                return;
            }
            var expense = await apiClient.Expenses.View(expenses.Items.FirstOrDefault().Expense.ID.Value);
            expense.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestDelete()
        {
            var expenses = await apiClient.Expenses.List(new Birko.SuperFaktura.Request.Expense.Filter() { });
            if (!(expenses.Items?.Any() ?? false))
            {
                return;
            }
            var expense = await apiClient.Expenses.Delete(expenses.Items.FirstOrDefault().Expense.ID.Value);
            expense.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestAddPayment()
        {
            var expenses = await apiClient.Expenses.List(new Birko.SuperFaktura.Request.Expense.Filter() { });
            if (!(expenses.Items?.Any() ?? false))
            {
                return;
            }
            var payment = await apiClient.Expenses.AddPayment(new Birko.SuperFaktura.Request.Expense.Payment()
            {
                ExpenseID = expenses.Items.FirstOrDefault().Expense.ID.Value,
                Currency = "EUR",
                Amount = 2,
            });
            payment.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestDeletePayment()
        {
            var expenses = await apiClient.Expenses.List(new Birko.SuperFaktura.Request.Expense.Filter() { });
            if (!(expenses.Items?.Any() ?? false))
            {
                return;
            }
            var expense = await apiClient.Expenses.View(expenses.Items.FirstOrDefault().Expense.ID.Value);
            if (expense == null)
            {
                return;
            }
            var payment = await apiClient.Expenses.DeletePayment(expenses.Items.Last().ExpensePayment.FirstOrDefault().ID.Value);
            payment.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestAddRelatedItem()
        {
            var expenses = await apiClient.Expenses.List(new Birko.SuperFaktura.Request.Expense.Filter() { });
            if (!(expenses.Items?.Any() ?? false))
            {
                return;
            }
            var invoices = await apiClient.Invoices.List(new Birko.SuperFaktura.Request.Invoice.Filter() { PerPage = 200 });
            if (!(invoices?.Items?.Any() ?? false))
            {
                return;
            }
            var invoice = invoices?.Items?.First();
            var related = await apiClient.Expenses.AddRelatedItem(new Birko.SuperFaktura.Request.RelatedItem()
            {
                ParentID = expenses.Items.FirstOrDefault().Expense.ID.Value,
                ParentType =  "expense",
                ChildID = invoice.Invoice.ID.Value,
                ChildType = "invoice"
            });
            related.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestDeleteRelatedItem()
        {
            var expenses = await apiClient.Expenses.List(new Birko.SuperFaktura.Request.Expense.Filter() { });
            if (!(expenses.Items?.Any() ?? false))
            {
                return;
            }
            var expense = await apiClient.Expenses.View(expenses.Items.FirstOrDefault().Expense.ID.Value);
            if (!(expense?.RelatedItem?.Any() ?? false))
            {
                return;
            }
             var related = await apiClient.Expenses.DeleteRelatedItem(expense.RelatedItem.FirstOrDefault().RelationID);
             related.ShouldNotBe(null);
        }
    }
}
