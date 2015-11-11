using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Birko.SuperFaktura.Entities;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using System.Dynamic;

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

        public static IEnumerable<RegionInfo> getRegionsInfos()
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(c => new RegionInfo(c.LCID))
                .Distinct()
                .OrderBy(c=>c.TwoLetterISORegionName)
                .ToList();

            return cultures;
        }

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
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", string.Format("{0} email={1}&apikey={2}", Client.APIAUTHKEYWORD, this.Email, this.APIKey));

            return client;
        }

        private async Task<PostResponse<T>> Post<T>(string uri, object data)
        {
            using (var client = this.CreateClient())
            {
                HttpResponseMessage response = await client.PostAsync(uri, new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("data", JsonConvert.SerializeObject(data)) }));
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    if (response.Content.Headers.ContentType.MediaType == "application/json")
                    {
                        return await response.Content.ReadAsAsync<PostResponse<T>>();
                    }
                    else
                    {
                        string jsonstring = await response.Content.ReadAsStringAsync();
                        return (PostResponse<T>)JsonConvert.DeserializeObject(jsonstring, typeof(PostResponse<T>));
                    }
                }
            }
            return null;
        }

        public void AddItem(Invoice.Item item)
        {
            if (item != null)
            {
                if (this.Data.InvoiceItem == null)
                {
                    this.Data.InvoiceItem = new List<Invoice.Item>();
                }
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
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>().ConfigureAwait(false);
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
                response.EnsureSuccessStatusCode();
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
                if (this.Data.Tag == null)
                {
                    this.Data.Tag = new List<int>();
                }
                foreach (var tag in tagIDs)
                {
                    if (!this.Data.Tag.Contains(tag))
                    {
                        this.Data.Tag.Add(tag);
                    }
                }
            }
        }

        public async Task<PageResponse<ClientItem>> Clients(Entities.Client.Filter filter, bool listInfo = true)
        {
            using (var client = this.CreateClient())
            {
                filter = (filter != null) ? filter : new Entities.Client.Filter();
                string uri = string.Format("clients/index.json{0}", filter.ToParams(listInfo));
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<PageResponse<ClientItem>>();
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
                response.EnsureSuccessStatusCode();
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
                response.EnsureSuccessStatusCode();
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
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<Dictionary<int, string>> GetCountries()
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("{0}countries", Client.APIURL);
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Dictionary<int, string>>();
                }
            }
            return null;
        }

        public async Task<IEnumerable<dynamic>> GetSequences()
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("sequences/index.json");
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<dynamic>>();
                }
            }
            return null;
        }

        public async Task<Dictionary<int, string>> GetTags()
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("tags/index.json");
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    if (response.Content.Headers.ContentLength > 2)
                    {
                        return await response.Content.ReadAsAsync<Dictionary<int, string>>();
                    }
                }
            }
            return null;
        }

        // language [eng,slo,cze]
        public async Task<byte[]> GetPdf(int invoiceId, string token, string language = "slo")
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("{0}/invoices/pdf/{1}/token:{2}", language, invoiceId, token );
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
            }
            return new byte[0];
        }

        public async Task<InvoiceResponse> Invoice(int ID)
        {
            using (var client = this.CreateClient())
            {
                string uri = string.Format("invoices/view/{0}.json", ID);
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<InvoiceResponse>();
                }
            }
            return null;
        }

        public async Task<PageResponse<InvoiceItem>> Invoices(Invoice.Filter filter, bool listInfo = true)
        {
            using (var client = this.CreateClient())
            {
                filter = (filter != null) ? filter : new Invoice.Filter();
                string uri = string.Format("invoices/index.json{0}", filter.ToParams(listInfo));
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<PageResponse<InvoiceItem>>();
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
                response.EnsureSuccessStatusCode();
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
                response.EnsureSuccessStatusCode();
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
                if (this.Data.StockLog == null)
                {
                    this.Data.StockLog = new List<Entities.Stock.Movement>();
                }
                foreach (var item in items)
                {
                    this.Data.StockLog.Add(item);
                }
                string uri = string.Format("stock_items/addstockmovement");
                return await this.Post<dynamic>(uri, this.Data);
            }
            return null;
        }

        public async Task<dynamic> AddStockMovement(Stock.Movement item)
        {
            return await this.AddStockMovement(new Stock.Movement[] { item });
        }

        public async Task<dynamic> AddStockItem(Stock.Item item)
        {
            string uri = string.Format("stock_items/add");
            return await this.Post<dynamic>(uri, item);
        }

        public async Task<dynamic> StockItemEdit(Stock.Item item)
        {
            string uri = string.Format("stock_items/edit");
            return await this.Post<dynamic>(uri, item);
        }


        public async Task<dynamic> StockItems(Stock.Filter filter, bool listInfo = true)
        {
            using (var client = this.CreateClient())
            {
                filter = (filter != null) ? filter : new Stock.Filter();
                string uri = string.Format("stock_items/index.json{0}", filter.ToParams(listInfo));
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
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
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<dynamic>();
                }
            }
            return null;
        }

        public async Task<PostResponse<ExpandoObject>> MarkAsSent(Invoice.MarkEmail email)
        {
            string uri = string.Format("invoices/mark_as_sent");
            return await this.Post<ExpandoObject>(uri, email);
        }


        public async Task<PostResponse<ExpandoObject>> SendInvoiceEmail(Invoice.Email email)
        {
            string uri = string.Format("invoices/send");
            return await this.Post<ExpandoObject>(uri, email);
        }

        public async Task<PostResponse<ExpandoObject>> SendInvoicePost(Invoice.Post post)
        {
            string uri = string.Format("invoices/post");
            return await this.Post<ExpandoObject>(uri, post);
        }

        public async Task<PostResponse<ExpandoObject>> PayInvoice(Invoice.Payment payment)
        {
            string uri = string.Format("invoice_payments/add/ajax:1/api:1");
            return await this.Post<ExpandoObject>(uri, payment);
        }
        public async Task<PostResponse<ExpandoObject>> PayExpense(Expense.Payment payment)
        {
            string uri = string.Format("expense_payments/add");
            return await this.Post<ExpandoObject>(uri, payment);
        }

        public async Task<PostResponse<InvoiceResponse>>SaveInvoice()
        {
            string uri = string.Format("/invoices/create");
            return await this.Post<InvoiceResponse>(uri, this.Data);

        }

        public async Task<PostResponse<EmptyItem>> SaveExpense()
        {
            if (this.Data.Expense != null)
            {
                string uri = string.Format("/expenses/add");
                return await this.Post<EmptyItem>(uri, this.Data);
            }
            return null;
        }

        public async Task<PostResponse<InvoiceItem>> EditInvoice()
        {
            string uri = string.Format("/invoices/edit");
            return await this.Post<InvoiceItem>(uri, this.Data);
        }

        public async Task<PostResponse<EmptyItem>> EditExpense()
        {
            if (this.Data.Expense != null)
            {
                string uri = string.Format("/expenses/edit");
                return await this.Post<EmptyItem>(uri, this.Data);
            }
            return null;
        }

        public async Task<PostResponse<ExpandoObject>> SaveClient()
        {
            string uri = string.Format("clients/create");
            return await Post<ExpandoObject>(uri, this.Data);
        }

        public void SetClient(Entities.Client client)
        {
            this.Data.Client = client;
        }

        public void SetInvoice(Entities.Invoice invoice)
        {
            this.Data.Expense = null;
            this.Data.StockLog = null;
            this.Data.Invoice = invoice;
        }

        public void SetExpense(Entities.Expense expense)
        {
            this.Data.Invoice = null;
            this.Data.InvoiceItem = null;
            this.Data.StockLog = null;
            this.Data.Expense = expense;
        }

        public async Task<PostResponse<ExpandoObject>> AddContactPerson(Entities.Client.ContactPerson person)
        {
            string uri = string.Format("/contact_people/add/api:1");
            return await this.Post<ExpandoObject>(uri, person);
        }
    }
}
