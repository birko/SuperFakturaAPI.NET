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
            return JsonConvert.DeserializeObject<Response<ExpandoObject>>(await superFaktura.Get(string.Format("expense/edit/{0}.json", ID)));
        }

        public async Task<PagedResponse> Get(Filter filter, bool listInfo = true)
        {
            var url = string.Format("expenses/index.json{0}", filter.ToParameters(listInfo));
            if (listInfo)
            {
                return JsonConvert.DeserializeObject<PagedResponse>(await superFaktura.Get(url));
            }
            else
            {
                return new PagedResponse { Items = JsonConvert.DeserializeObject<ItemList<ListItem>>(await superFaktura.Get(url)) };
            }
        }

        public async Task<Response<ExpandoObject>> Delete(int ID)
        {
            return JsonConvert.DeserializeObject<Response<ExpandoObject>>(await superFaktura.Get(string.Format("expenses/delete/{0}", ID)));
        }

        public async Task<Response<ListItem>> Save(Request.Expense.Expense expense)
        {
            return JsonConvert.DeserializeObject<Response<ListItem>>(await superFaktura.Post("/expenses/add", new { Expense = expense }));
        }

        public async Task<Response<ListItem>> Edit(Request.Expense.Expense expense)
        {
            return JsonConvert.DeserializeObject<Response<ListItem>>(await superFaktura.Post("/expenses/edit", new { Expense = expense }));
        }

        public async Task<Response<ListItem>> Pay(Request.Expense.Payment payment)
        {
            return JsonConvert.DeserializeObject<Response<ListItem>>(await superFaktura.Post("expense_payments/add", new { ExpensePayment = payment }));
        }

        public async Task<CategoryItem[]> GetExpenseCategories()
        {
            return JsonConvert.DeserializeObject<CategoryItem[]>(await superFaktura.Get("expenses/expense_categories"));
        }
    }
}
