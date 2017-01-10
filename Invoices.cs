using Birko.SuperFaktura.Request.Client;
using Birko.SuperFaktura.Request.Invoice;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.Invoice;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birko.SuperFaktura
{
    public class Invoices
    {
        private readonly SuperFaktura superFaktura;

        public Invoices(SuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<Detail> Get(int ID)
        {
            return JsonConvert.DeserializeObject<Detail> (await superFaktura.Get(string.Format("invoices/view/{0}.json", ID)));
        }

        public async Task<PagedResponse> Get(Request.Invoice.Filter filter, bool listInfo = true)
        {
            var url = string.Format("invoices/index.json{0}", filter.ToParameters(listInfo));
            if (listInfo)
            {
                return JsonConvert.DeserializeObject<PagedResponse>(await superFaktura.Get(url));
            }
            else
            {
                return new PagedResponse { Items = JsonConvert.DeserializeObject<ItemList<ListItem>>(await superFaktura.Get(url)) };
            }
        }

        public async Task<Response.Invoice.ResponsePayment> Pay(Request.Invoice.Payment payment)
        {
            return JsonConvert.DeserializeObject<Response.Invoice.ResponsePayment>(await superFaktura.Post("invoice_payments/add/ajax:1/api:1", new { InvoicePayment = payment }));
        }

        public async Task<byte[]> GetPdf(int invoiceId, string token, string language = LanguageType.Slovak)
        {

            if (!LanguageType.Languages.Contains(language))
            {
                language = LanguageType.Slovak;
            }
            return await superFaktura.GetByte(string.Format("{0}/invoices/pdf/{1}/token:{2}", language, invoiceId, token));
        }

        public async Task<Response<ExpandoObject>> SetInvoiceLanguage(int ID, string language = LanguageType.Slovak)
        {
            if (!LanguageType.Languages.Contains(language))
            {
                language = LanguageType.Slovak;
            }
            // error response fix
            return JsonConvert.DeserializeObject<Response<ExpandoObject>>(await superFaktura.Get(string.Format("invoices/setinvoicelanguage/{0}/lang:{1}", ID, language)));
        }

        public async Task<Response<ExpandoObject>> MarkAsSent(MarkEmail email)
        {
            return JsonConvert.DeserializeObject<Response<ExpandoObject>>(await superFaktura.Post("invoices/mark_as_sent", new { InvoiceEmail = email }));
        }

        public async Task<Response<Response.Invoice.ResponseEmail>> SendEmail(Request.Invoice.Email email)
        {
            return JsonConvert.DeserializeObject<Response<Response.Invoice.ResponseEmail>>(await superFaktura.Post("invoices/send", new { Email = email }));
        }

        public async Task<Response<Detail>> Save(Request.Invoice.Invoice invoice, Client client, Request.Invoice.Item[] items, int[] tags = null)
        {
            return JsonConvert.DeserializeObject<Response<Detail>>(await superFaktura.Post("/invoices/create", new
            {
                Invoice = invoice,
                InvoiceItem = items,
                Tag = tags,
                Client = client,
            }));
        }

        public async Task<Response<DetailBasic>> Update(Request.Invoice.Invoice invoice, Client client, Request.Invoice.Item[] items, int[] tags = null)
        {
            return JsonConvert.DeserializeObject<Response<DetailBasic>>(await superFaktura.Post("/invoices/Edit", new
            {
                Invoice = invoice,
                InvoiceItem = items,
                Tag = tags,
                Client = client,
            }));
        }

        public async Task<Response<bool>> DeleteItem(int invoiceID, int itemID)
        {
            return JsonConvert.DeserializeObject<Response<bool>>(await superFaktura.Get(string.Format("invoice_items/delete/{1}/invoice_id:{0}", invoiceID, itemID)));
        }

        public async Task<Response<ExpandoObject>> Delete(int invoiceID)
        {
            return JsonConvert.DeserializeObject<Response<ExpandoObject>>(await superFaktura.Get(string.Format("invoices/delete/{0}", invoiceID)));
        }

        /*-------*/

        public async Task<Response<ExpandoObject>> SendPost(Post post)
        {
            // Otestovat na produkcii
            return JsonConvert.DeserializeObject<Response<ExpandoObject>>(await superFaktura.Post("invoices/post", new { Post = post }));
        }
    }
}
