using Birko.SuperFaktura.Request.Expense;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public async Task<PagedResponse<Detail>> List(Filter filter, bool listInfo = true, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get(string.Format("expenses/index.json{0}", filter.ToParameters(listInfo)), cancellationToken).ConfigureAwait(false);
            if (listInfo)
            {
                return superFaktura.DeserializeResult<PagedResponse<Detail>>(result);
            }
            else
            {
                return new PagedResponse<Detail> { Items = superFaktura.DeserializeResult<IEnumerable<Detail>>(result) };
            }
        }

        public async Task<StringMessageResponse> Delete(int ID, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get($"expenses/delete/{ID}", cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<StringMessageResponse>(result);
        }

        /// <param name="checksum">Your own unique identifier of this request (e.g. document number, max 32 chars).
        /// If no response arrives, call ResponseByChecksum with it to find out whether the expense was created.</param>
        public async Task<Detail> Add(Request.Expense.Expense expense, IEnumerable<Request.Expense.ExpenseItem> items = null, Request.Client.Client client = null, Request.Expense.Extra extra = null, IEnumerable<int> tags = null, string checksum = null, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Post("expenses/add", new ExpenseData { Expense = expense, ExpenseExtra = extra, Tag = tags, Client = client, ExpenseItems = items, CheckSum = checksum }, cancellationToken).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<Detail>>(result);
            return data.Data;
        }

        public async Task<Response<Detail>> Edit(Request.Expense.Expense expense, IEnumerable<Request.Expense.ExpenseItem> items = null, Request.Client.Client client = null, Request.Expense.Extra extra = null, IEnumerable<int> tags = null, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Post("expenses/edit", new ExpenseData { Expense = expense, ExpenseExtra = extra, Tag = tags, Client = client, ExpenseItems = items }, cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<Detail>>(result);
        }

        public async Task<Detail> View(int id, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get($"expenses/view/{id}.json", cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Detail>(result);
        }

        public async Task<PaymentResponse> AddPayment(Request.Expense.Payment payment, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Post("expense_payments/add", new Request.Expense.ExpensePaymentData { ExpensePayment = payment }, cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<PaymentResponse>(result);
        }

        public async Task<StringMessageResponse> DeletePayment(int expensePaymentID, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get($"expense_payments/delete/{expensePaymentID}", cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<StringMessageResponse>(result);
        }

        public async Task<RelatedItemResponse> AddRelatedItem(Request.RelatedItem relatedItem, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Post("expenses/addRelatedItem", relatedItem, cancellationToken).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<RelatedItemResponse>>(result);
            return data.Data;
        }

        public async Task<ErrorMessageResponse> DeleteRelatedItem(int relationID, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get($"expenses/deleteRelatedItem/{relationID}", cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<ErrorMessageResponse>(result);
        }

        public async Task<byte[]> DownloadAttachments(int expenseID, int? attachementID = null, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.GetByte($"expenses/downloadAttachment/{expenseID}/{attachementID}", cancellationToken).ConfigureAwait(false);
            //Code below tests if response is a SuperFaktura error response or PDF File
            try
            {
                string testResult = Encoding.UTF8.GetString(result);
                superFaktura.DeserializeResult<ErrorMessageResponse>(testResult);
            }
            catch (Exceptions.ParseException) when (result != null && result.Length != 0)
            {
                //test deserialization failed. it is a pdf file
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<Response<Detail>> DeleteExpenseItem(int expenseID, IEnumerable<int> deleteItemIDS, bool createBlank = true, CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Delete("expense_items/delete", new
            {
                expense_id = expenseID,
                delete_ids = deleteItemIDS.ToArray(),
                create_blank_if_empty = createBlank
            }, cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<Detail>>(result);
        }
    }
}
