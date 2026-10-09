using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class ExpensesTest : SuperFakturaTest
    {
        private static async Task<Birko.SuperFaktura.Response.Expense.Detail> AddTestExpense(string prefix = "Expense test")
        {
            return await apiClient.Expenses.Add(new Birko.SuperFaktura.Request.Expense.Expense()
            {
                Name = UniqueName(prefix),
                Currency = "EUR",
                Amount = 12,
            });
        }

        [Fact]
        public async Task TestList()
        {
            var added = await AddTestExpense("List test");
            try
            {
                // search filter: exactly the expense created by this test
                var expenses = await apiClient.Expenses.List(new Birko.SuperFaktura.Request.Expense.Filter() { Search = added.Expense.Name });
                expenses.ShouldNotBe(null);
                expenses.Items.ShouldNotBeEmpty();
                expenses.Items.ShouldContain(x => x.Expense.ID == added.Expense.ID);
            }
            finally
            {
                await apiClient.Expenses.Delete(added.Expense.ID.Value);
            }
        }

        [Fact]
        public async Task TestAdd()
        {
            var expense = await AddTestExpense("Foo bar");
            try
            {
                expense.ShouldNotBe(null);
                expense.Expense.Amount.ShouldBe(12);
            }
            finally
            {
                await apiClient.Expenses.Delete(expense.Expense.ID.Value);
            }
        }

        [Fact]
        public async Task TestEdit()
        {
            var added = await AddTestExpense();
            try
            {
                var expense = await apiClient.Expenses.Edit(new Birko.SuperFaktura.Request.Expense.Expense()
                {
                    ID = added.Expense.ID,
                    Name = "Foo bar Edit",
                    Amount = 14,
                });
                expense.ShouldNotBe(null);
                var view = await apiClient.Expenses.View(added.Expense.ID.Value);
                view.Expense.Name.ShouldBe("Foo bar Edit");
                view.Expense.Amount.ShouldBe(14);
            }
            finally
            {
                await apiClient.Expenses.Delete(added.Expense.ID.Value);
            }
        }

        [Fact]
        public async Task TestView()
        {
            var added = await AddTestExpense();
            try
            {
                var expense = await apiClient.Expenses.View(added.Expense.ID.Value);
                expense.ShouldNotBe(null);
                expense.Expense.ID.ShouldBe(added.Expense.ID);
            }
            finally
            {
                await apiClient.Expenses.Delete(added.Expense.ID.Value);
            }
        }

        [Fact]
        public async Task TestDelete()
        {
            var added = await AddTestExpense();
            var expense = await apiClient.Expenses.Delete(added.Expense.ID.Value);
            expense.ShouldNotBe(null);
            expense.Message.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public async Task TestListFiltersByMultipleStatuses()
        {
            // one unpaid and one fully paid expense sharing a unique name, found by search
            var name = UniqueName("Status test");
            var unpaid = await apiClient.Expenses.Add(new Birko.SuperFaktura.Request.Expense.Expense { Name = name + " unpaid", Currency = "EUR", Amount = 12 });
            var paid = await apiClient.Expenses.Add(new Birko.SuperFaktura.Request.Expense.Expense { Name = name + " paid", Currency = "EUR", Amount = 12 });
            try
            {
                await apiClient.Expenses.AddPayment(new Birko.SuperFaktura.Request.Expense.Payment
                {
                    ExpenseID = paid.Expense.ID.Value,
                    Currency = "EUR",
                    Amount = (await apiClient.Expenses.View(paid.Expense.ID.Value)).Expense.Total,
                });

                async Task<int[]> ListIds(params int[] statuses)
                {
                    var list = await apiClient.Expenses.List(new Birko.SuperFaktura.Request.Expense.Filter { Search = name, Status = statuses });
                    return (list?.Items ?? Enumerable.Empty<Birko.SuperFaktura.Response.Expense.Detail>())
                        .Select(x => x.Expense.ID.Value).OrderBy(x => x).ToArray();
                }

                var both = new[] { unpaid.Expense.ID.Value, paid.Expense.ID.Value }.OrderBy(x => x).ToArray();
                (await ListIds(Birko.SuperFaktura.Request.ValueLists.ExpenseStatus.New, Birko.SuperFaktura.Request.ValueLists.ExpenseStatus.Paid))
                    .ShouldBe(both);
                (await ListIds(Birko.SuperFaktura.Request.ValueLists.ExpenseStatus.New)).ShouldBe(new[] { unpaid.Expense.ID.Value });
                (await ListIds(Birko.SuperFaktura.Request.ValueLists.ExpenseStatus.Paid)).ShouldBe(new[] { paid.Expense.ID.Value });
            }
            finally
            {
                await apiClient.Expenses.Delete(unpaid.Expense.ID.Value);
                await apiClient.Expenses.Delete(paid.Expense.ID.Value);
            }
        }

        [Fact]
        public async Task TestAddPayment()
        {
            var added = await AddTestExpense();
            try
            {
                var payment = await apiClient.Expenses.AddPayment(new Birko.SuperFaktura.Request.Expense.Payment()
                {
                    ExpenseID = added.Expense.ID.Value,
                    Currency = "EUR",
                    Amount = 2,
                });
                payment.ShouldNotBe(null);
                payment.Data.ExpensePayment.ShouldNotBeEmpty();
            }
            finally
            {
                await apiClient.Expenses.Delete(added.Expense.ID.Value);
            }
        }

        [Fact]
        public async Task TestDeletePayment()
        {
            var added = await AddTestExpense();
            try
            {
                await apiClient.Expenses.AddPayment(new Birko.SuperFaktura.Request.Expense.Payment()
                {
                    ExpenseID = added.Expense.ID.Value,
                    Currency = "EUR",
                    Amount = 2,
                });
                var expense = await apiClient.Expenses.View(added.Expense.ID.Value);
                var paymentId = expense.ExpensePayment.Single().ID.Value;

                var payment = await apiClient.Expenses.DeletePayment(paymentId);
                payment.ShouldNotBe(null);
                (await apiClient.Expenses.View(added.Expense.ID.Value)).ExpensePayment?.ShouldNotContain(x => x.ID == paymentId);
            }
            finally
            {
                await apiClient.Expenses.Delete(added.Expense.ID.Value);
            }
        }

        [Fact]
        public async Task TestAddRelatedItem()
        {
            var added = await AddTestExpense();
            var invoice = await CreateTestInvoice();
            try
            {
                var related = await apiClient.Expenses.AddRelatedItem(new Birko.SuperFaktura.Request.RelatedItem()
                {
                    ParentID = added.Expense.ID.Value,
                    ParentType = Birko.SuperFaktura.Request.ValueLists.DocumentType.Expense,
                    ChildID = invoice.Invoice.ID.Value,
                    ChildType = Birko.SuperFaktura.Request.ValueLists.DocumentType.Invoice
                });
                related.ShouldNotBe(null);
                (await apiClient.Expenses.View(added.Expense.ID.Value)).RelatedItem.ShouldNotBeEmpty();
            }
            finally
            {
                await apiClient.Expenses.Delete(added.Expense.ID.Value);
                await DeleteTestInvoice(invoice);
            }
        }

        [Fact]
        public async Task TestDeleteRelatedItem()
        {
            var added = await AddTestExpense();
            var invoice = await CreateTestInvoice();
            try
            {
                await apiClient.Expenses.AddRelatedItem(new Birko.SuperFaktura.Request.RelatedItem()
                {
                    ParentID = added.Expense.ID.Value,
                    ParentType = Birko.SuperFaktura.Request.ValueLists.DocumentType.Expense,
                    ChildID = invoice.Invoice.ID.Value,
                    ChildType = Birko.SuperFaktura.Request.ValueLists.DocumentType.Invoice
                });
                var expense = await apiClient.Expenses.View(added.Expense.ID.Value);

                var related = await apiClient.Expenses.DeleteRelatedItem(expense.RelatedItem.Single().RelationID);
                related.ShouldNotBe(null);
                (await apiClient.Expenses.View(added.Expense.ID.Value)).RelatedItem?.ShouldBeEmpty();
            }
            finally
            {
                await apiClient.Expenses.Delete(added.Expense.ID.Value);
                await DeleteTestInvoice(invoice);
            }
        }
    }
}
