using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Birko.SuperFaktura.Entities;

namespace Birko.SuperFaktura
{
    public class Client
    {
        public string Email { get; internal set; }
        public string APIKey { get; internal set; }
        public Data Data { get; internal set; } = new Data();
        // constants
        public const string APIAUTHKEYWORD = "SFAPI";
        public const string APIURL = "https://moja.superfaktura.sk/";

        public Client(string email, string apikey)
        {
            this.Email = email;
            this.APIKey = apikey;
        }

        protected HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Client.APIURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", string.Format("{0} email={1}&apiKey={2}", Client.APIAUTHKEYWORD, this.Email, this.APIKey));

            return client;
        }

        public void AddItem(Invoice.Item item)
        {
            if (item != null)
            {
                this.Data.InvoiceItem.Add(item);
            }
        }

        public void ResetData()
        {
            this.Data = new Data();
        }

        public async Task<dynamic> DeleteInvoiceItem(int invoiceID, int itemID)
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("invoice_items/delete/{0}/invoice_id:{1}", invoiceID, itemID);
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> DeleteExpense(int ID)
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("expenses/delete/{0}", ID);
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public void AddTags(IEnumerable<int> tagIDs)
        {
            if (tagIDs != null)
            {
                foreach (var tag in tagIDs)
                {
                    if (!this.Data.Tag.Contains(tag))
                    {
                        this.Data.Tag.Add(tag);
                    }
                }
            }
        }

        public async Task<dynamic> Clients(Entities.Client.Filter filter, bool listInfo = true)
        {
            using (var client = this.CreateClient())
            {
                filter = (filter != null) ? filter : new Entities.Client.Filter();
                string uri = string.Format("clients/index.json{0}", filter.ToParams(listInfo));
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> Delete(int ID)
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("invoices/delete/{0}", ID);
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> DeleteStockItem(int ID)
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("stock_items/delete/{0}", ID);
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> Expenses(Expense.Filter filter, bool listInfo = true)
        {
            using (var client = this.CreateClient())
            {
                filter = (filter != null) ? filter : new Expense.Filter();
                string uri = string.Format("expenses/index.json{0}", filter.ToParams(listInfo));
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> GetCountries()
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("countries");
                //HttpResponseMessage response = await client.GetAsync(uri);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri) {};
                request.Headers.Add("Authorization", string.Format("{0} email={1}&apiKey={2}", Client.APIAUTHKEYWORD, this.Email, this.APIKey));
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> GetSequences()
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("sequences/index.json");
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> GetTags()
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("tags/index.json");
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        // language [eng,slo,cze]
        public async Task<dynamic> GetPdf(int invoiceId, string token, string language = "slo")
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("{0}/invoices/pdf/{1}/token:{2}", language, invoiceId, token );
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> Invoice(int ID)
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("invoices/view/{0}.json", ID);
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> Invoices(Invoice.Filter filter, bool listInfo = true)
        {
            using (var client = this.CreateClient())
            {
                filter = (filter != null) ? filter : new Invoice.Filter();
                string uri = string.Format("invoices/index.json{0}", filter.ToParams(listInfo));
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> Expense(int ID)
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("expense/edit/{0}.json", ID);
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> StockItem(int ID)
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("stock_items/edit/{0}.json", ID);
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> AddStockMovement(IEnumerable<Stock.Movement> items)
        {
            if (items != null)
            {
                using (var client = this.CreateClient())
                {
                    foreach (var item in items)
                    {
                        this.Data.StockLog.Add(item);
                    }

                    string uri = string.Format("stock_items/addstockmovement");
                    HttpResponseMessage response = await client.PostAsJsonAsync(uri, this.Data);
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<dynamic>();
                    }
                }
            }
            return null;
        }

        public async Task<dynamic> AddStockMovement(Stock.Movement item)
        {
            return await this.AddStockMovement(new Stock.Movement[] { item });
        }

        public async Task<dynamic> AddStockItem(Stock.Item item)
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("stock_items/add");
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, item);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> StockItemEdit(Stock.Item item)
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("stock_items/edit");
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, item);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }


        public async Task<dynamic> StockItems(Stock.Filter filter, bool listInfo = true)
        {
            using (var client = this.CreateClient())
            {
                filter = (filter != null) ? filter : new Stock.Filter();
                string uri = string.Format("stock_items/index.json{0}", filter.ToParams(listInfo));
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> SetInvoiceLanguage(int ID, string lang = "slo")
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("invoices/setinvoicelanguage/{0}/lang:{1}", ID, lang);
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> MarkAsSent(Invoice.MarkEmail email)
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("invoices/mark_as_sent");
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, email);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }


        public async Task<dynamic> SendInvoiceEmail(Invoice.Email email)
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("invoices/send");
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, email);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> SendInvoicePost(Invoice.Post post)
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("invoices/post");
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, post);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> PayInvoice(Invoice.Payment payment)
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("invoice_payments/add/ajax:1/api:1");
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, payment);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }
        public async Task<dynamic> PayExpense(Invoice.Payment payment)
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("expense_payments/add");
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, payment);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> Save()
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format((Data.Expense == null) ? "/invoices/create": "/expenses/add");
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, this.Data);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> Edit()
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format((Data.Expense == null) ? "/invoices/edit": "/expenses/edit");
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, this.Data);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<dynamic> SaveClient()
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("clients/create");
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, this.Data);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public void SetClient(Entities.Client client)
        {
            this.Data.Client = client;
        }

        public void SetInvoice(Entities.Invoice invoice)
        {
            this.Data.Expense = null;
            this.Data.Invoice = invoice;
        }

        public void SetExpense(Entities.Expense expense)
        {
            this.Data.Invoice = null;
            this.Data.InvoiceItem = null;
            this.Data.Expense = expense;
        }
    }
}
