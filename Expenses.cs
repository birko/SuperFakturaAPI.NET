using Birko.SuperFaktura.Request.Expense;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.Expense;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace Birko.SuperFaktura
{
    public class Expenses
    {
        private readonly SuperFaktura superFaktura;

        public Expenses(SuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<Response<ExpandoObject>> Get(int ID)
        {
            var result = await superFaktura.Get(string.Format("expense/edit/{0}.json", ID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }

        public async Task<PagedResponse> Get(Filter filter, bool listInfo = true)
        {
            var result = await superFaktura.Get(string.Format("expenses/index.json{0}", filter.ToParameters(listInfo))).ConfigureAwait(false);
            if (listInfo)
            {
                return superFaktura.DeserializeResult<PagedResponse>(result);
            }
            else
            {
                return new PagedResponse { Items = superFaktura.DeserializeResult<ItemList<ListItem>>(result) };
            }
        }

        public async Task<Response<ExpandoObject>> Delete(int ID)
        {
            var result = await superFaktura.Get(string.Format("expenses/delete/{0}", ID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }

        public async Task<Response<ListItem>> Save(Request.Expense.Expense expense)
        {
            var result = await superFaktura.Post("/expenses/add", new { Expense = expense }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ListItem>>(result);
        }

        public async Task<Response<ListItem>> Edit(Request.Expense.Expense expense)
        {
            var result = await superFaktura.Post("/expenses/edit", new { Expense = expense }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ListItem>>(result);
        }

        public async Task<Response<ListItem>> Pay(Request.Expense.Payment payment)
        {
            var result = await superFaktura.Post("expense_payments/add", new { ExpensePayment = payment }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ListItem>>(result);
        }

        public async Task<CategoryItem[]> GetExpenseCategories()
        {
            var result = await superFaktura.Get("expenses/expense_categories").ConfigureAwait(false);
            return superFaktura.DeserializeResult<CategoryItem[]>(result);
        }
    }
}
