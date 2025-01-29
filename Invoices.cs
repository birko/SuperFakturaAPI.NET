using Birko.SuperFaktura.Request.Client;
using Birko.SuperFaktura.Request.Invoice;
using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.Invoice;
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
        private readonly AbstractSuperFaktura superFaktura;

        public Invoices(AbstractSuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<PagedResponse> List(Request.Invoice.Filter filter, bool listInfo = true)
        {
            var result = await superFaktura.Get(string.Format("invoices/index.json{0}", filter.ToParameters(listInfo))).ConfigureAwait(false);
            if (listInfo)
            {
                return superFaktura.DeserializeResult<PagedResponse>(result);
            }
            else
            {
                return new PagedResponse { Items = superFaktura.DeserializeResult<IEnumerable<Detail>>(result) };
            }
        }

        public async Task<Detail> Add(Request.Invoice.Invoice invoice, Client client, Request.Invoice.Item[] items, int[] tags = null, InvoiceSettings setting = null, Extra extra = null, Request.Invoice.MyData myData = null)
        {
            var data = new InvoiceData
            {
                Invoice = invoice,
                InvoiceItem = items,
                Tag = tags,
                Client = client,
                InvoiceSetting = setting,
                InvoiceExtra = extra,
                MyData = myData
            };

            var result = await superFaktura.Post("invoices/create", data).ConfigureAwait(false);
            var response = superFaktura.DeserializeResult<StatusResponse<Detail>>(result);
            return response.Data;
        }

        public async Task<Detail> View(int ID)
        {
            var result = await superFaktura.Get(string.Format("invoices/view/{0}.json", ID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Detail>(result);
        }

        public async Task<Dictionary<int, Detail>> ListDetails(IEnumerable<int> IDS)
        {
            var result = await superFaktura.Get(string.Format("invoices/getInvoiceDetails/{0}", string.Join(",", IDS))).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Dictionary<int, Detail>>(result);
        }

        public async Task<DetailData> Edit(Request.Invoice.Invoice invoice, Client client, Request.Invoice.Item[] items, int[] tags = null, InvoiceSettings setting = null, Extra extra = null, Request.Invoice.MyData myData = null)
        {
            var data = new InvoiceData
            {
                Invoice = invoice,
                InvoiceItem = items,
                Tag = tags,
                Client = client,
                InvoiceSetting = setting,
                InvoiceExtra = extra,
                MyData = myData
            };

            var result = await superFaktura.Post("invoices/edit", data).ConfigureAwait(false);
            var response = superFaktura.DeserializeResult<Response<DetailData>>(result);
            return response.Data;
        }

        public async Task<StringMessageResponse> SetInvoiceLanguage(int ID, string language = Request.ValueLists.LanguageType.Slovak)
        {
            if (!Request.ValueLists.LanguageType.Languages.Contains(language))
            {
                language = Request.ValueLists.LanguageType.Slovak;
            }
            // error response fix
            var result = await superFaktura.Get(string.Format("invoices/setinvoicelanguage/{0}/lang:{1}", ID, language)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<StringMessageResponse>(result);
        }

        public async Task<byte[]> Download(int invoiceId, string token, string language = Request.ValueLists.LanguageType.Slovak)
        {
            if (!Request.ValueLists.LanguageType.Languages.Contains(language))
            {
                language = Request.ValueLists.LanguageType.Slovak;
            }
            var result = await superFaktura.GetByte($"{language}/invoices/pdf/{invoiceId}/token:{token}").ConfigureAwait(false);
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

        public async Task<StringMessageResponse> Delete(int id)
        {
            var result = await superFaktura.Get(string.Format("invoices/delete/{0}", id)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<StringMessageResponse>(result);
        }

        public async Task<DetailBasic> WillNotBePaid(int id)
        {
            var result = await superFaktura.Get(string.Format("invoices/will_not_be_paid/{0}", id)).ConfigureAwait(false);
            var detail = superFaktura.DeserializeResult<Response<DetailBasic>>(result);
            return detail.Data;
        }

        public async Task<ResponseEmailInvoice> SendEmail(Request.Invoice.Email email)
        {
            var result = await superFaktura.Post("invoices/send", new EmailData { Email = email }).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<ResponseEmail>>(result);
            return data.Data.Invoice;
        }

        public async Task<ErrorMessageResponse> MarkAsSentViaMail(MarkEmail email)
        {
            var result = await superFaktura.Post("invoices/mark_as_sent", new MarkEmailData { InvoiceEmail = email }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<ErrorMessageResponse>(result);
        }

        public async Task<DetailBasic> SendPost(Post post)
        {
            var result = await superFaktura.Post("invoices/post", new PostData { Post = post }).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<DetailBasic>>(result);
            return data.Data;
        }

        public async Task<Mark> MarkAsSend(int invoiceID)
        {
            var result = await superFaktura.Get(string.Format("invoices/mark_sent/{0}", invoiceID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Mark>(result);
        }

        public async Task<DetailInvoice> DeleteItem(int invoiceID, int itemID)
        {
            return await DeleteItem(invoiceID, new int[] { itemID }).ConfigureAwait(false);
        }

        public async Task<DetailInvoice> DeleteItem(int invoiceID, int[] itemID)
        {
            if (!(itemID?.Any() ?? false))
            {
                return null;
            }
            var result = await superFaktura.Get(string.Format("invoice_items/delete/{1}/invoice_id:{0}", invoiceID, string.Join(",", itemID))).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<DetailInvoice>>(result);
            return data.Data;
        }

        public async Task<ResponsePayment> AddPayment(Request.Invoice.Payment payment)
        {
            var result = await superFaktura.Post("invoice_payments/add/ajax:1/api:1", new InvoicePaymentData { InvoicePayment = payment }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<ResponsePayment>(result);
        }

        public async Task<DeletePayment> DeletePayment(int invoicePaymentID)
        {
            var result = await superFaktura.Get(string.Format("invoice_payments/delete/{0}", invoicePaymentID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<DeletePayment>(result);
        }

        public async Task<RelatedItemResponse> AddRelatedItem(Request.RelatedItem related)
        {
            var result = await superFaktura.Post("invoices/addRelatedItem", related).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<RelatedItemResponse>>(result);
            return data.Data;
        }

        public async Task<StringMessageResponse> DeleteRelatedItem(int relationID)
        {
            var result = await superFaktura.Get(string.Format("invoices/deleteRelatedItem/{0}", relationID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<StringMessageResponse>(result);
        }

        public async Task<byte[]> DownloadReceipt(int invoiceID)
        {
            var result = await superFaktura.GetByte(string.Format("invoices/receipt/{0}", invoiceID)).ConfigureAwait(false);
            return result;
        }

        [Obsolete("Not found in API documentation")]
        public async Task<Detail> CreateFromProforma(int proformaID)
        {
            var proforma = await superFaktura.Get(string.Format("invoices/regular.json/{0}", proformaID)).ConfigureAwait(false);
            var data =  superFaktura.DeserializeResult<ExpandoObject>(proforma);
            var result = await superFaktura.Post("/invoices/create", new Request.DataData { Data =  data}).ConfigureAwait(false);
            var resultdata = superFaktura.DeserializeResult<Response<Detail>>(result);
            return resultdata.Data;
        }

        [Obsolete("Not found in API documentation")]
        public async Task<ExpandoObject> SetEstimateStatus(int estimateID, int status)
        {
            var result = await superFaktura.Get(string.Format("invoices/set_estimate_status/{0}/{1}/ajax:1", estimateID, status)).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
            return data.Data;
        }
    }
}
