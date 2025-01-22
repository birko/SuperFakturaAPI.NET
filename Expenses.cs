﻿using Birko.SuperFaktura.Request.Client;
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

        public async Task<Response<ExpandoObject>> Get(int ID)
        {
            var result = await superFaktura.Get(string.Format("expense/edit/{0}.json", ID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }

        public async Task<Response<ExpandoObject>> Delete(int ID)
        {
            var result = await superFaktura.Get(string.Format("expenses/delete/{0}", ID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }

        public async Task<ListItem> Add(Request.Expense.Expense expense, IEnumerable<Request.Expense.ExpenseItem> items = null, Client client= null, Extra extra = null, IEnumerable<int> tags = null)
        {
            var result = await superFaktura.Post("/expenses/add", new ExpenseData { Expense = expense, ExpenseExtra = extra, Tag = tags }).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<ListItem>>(result);
            return data.Data;
        }

        public async Task<Response<ListItem>> Edit(Request.Expense.Expense expense)
        {
            var result = await superFaktura.Post("/expenses/edit", new ExpenseData { Expense = expense }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ListItem>>(result);
        }

        public async Task<Response<ListItem>> Pay(Request.Expense.Payment payment)
        {
            var result = await superFaktura.Post("expense_payments/add", new ExpensePaymentData { ExpensePayment = payment }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ListItem>>(result);
        }

        public async Task<CategoryItem[]> GetExpenseCategories()
        {
            var result = await superFaktura.Get("expenses/expense_categories").ConfigureAwait(false);
            return superFaktura.DeserializeResult<CategoryItem[]>(result);
        }

        public async Task<Response<ExpandoObject>> DeletePayment(int expensePaymentID)
        {
            var result = await superFaktura.Get(string.Format("/expense_payments/delete/{0}", expensePaymentID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }
    }
}
