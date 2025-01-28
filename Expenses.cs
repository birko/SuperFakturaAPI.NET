using Birko.SuperFaktura.Request.Expense;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.Expense;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace Birko.SuperFaktura
{
    public class Expenses
    {
        private readonly AbstractSuperFaktura superFaktura;

        public Expenses(AbstractSuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<PagedResponse<Detail>> List(Filter filter, bool listInfo = true)
        {
            var result = await superFaktura.Get(string.Format("expenses/index.json{0}", filter.ToParameters(listInfo))).ConfigureAwait(false);
            if (listInfo)
            {
                return superFaktura.DeserializeResult<PagedResponse<Detail>>(result);
            }
            else
            {
                return new PagedResponse<Detail> { Items = superFaktura.DeserializeResult<IEnumerable<Detail>>(result) };
            }
        }

        public async Task<ErrorMessageResponse> Delete(int ID)
        {
            var result = await superFaktura.Get($"expenses/delete/{ID}").ConfigureAwait(false);
            return superFaktura.DeserializeResult<ErrorMessageResponse>(result);
        }

        public async Task<Detail> Add(Request.Expense.Expense expense, IEnumerable<Request.Expense.ExpenseItem> items = null, Request.Client.Client client= null, Request.Expense.Extra extra = null, IEnumerable<int> tags = null)
        {
            var result = await superFaktura.Post("expenses/add", new ExpenseData { Expense = expense, ExpenseExtra = extra, Tag = tags, Client = client }).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<Detail>>(result);
            return data.Data;
        }

        public async Task<Response<Detail>> Edit(Request.Expense.Expense expense, Request.Client.Client client = null, Request.Expense.Extra extra = null, IEnumerable<int> tags = null)
        {
            var result = await superFaktura.Post("expenses/edit", new ExpenseData { Expense = expense, ExpenseExtra = extra, Tag = tags, Client = client }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<Detail>>(result);
        }

        public async Task<Detail> View(int id)
        {
            var result = await superFaktura.Get($"expenses/view/{id}.json").ConfigureAwait(false);
            return superFaktura.DeserializeResult<Detail>(result);
        }

        public async Task<PaymentResponse> AddPayment(Request.Expense.Payment payment)
        {
            var result = await superFaktura.Post("expense_payments/add", new Request.Expense.ExpensePaymentData { ExpensePayment = payment }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<PaymentResponse>(result);
        }

        public async Task<ErrorMessageResponse> DeletePayment(int expensePaymentID)
        {
            var result = await superFaktura.Get($"expense_payments/delete/{expensePaymentID}").ConfigureAwait(false);
            return superFaktura.DeserializeResult<ErrorMessageResponse>(result);
        }

        public async Task<RelatedItemResponse> AddRelatedItem(Request.RelatedItem relatedItem)
        {
            var result = await superFaktura.Post("expenses/addRelatedItem", relatedItem).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<RelatedItemResponse>>(result);
            return data.Data;
        }

        public async Task<ErrorMessageResponse> DeleteRelatedItem(int relationID)
        {
            var result = await superFaktura.Get($"expenses/deleteRelatedItem/{relationID}").ConfigureAwait(false);
            return superFaktura.DeserializeResult<ErrorMessageResponse>(result);
        }
    }
}
