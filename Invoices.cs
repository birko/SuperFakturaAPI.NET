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
        private readonly AbstractSuperFaktura superFaktura;

        public Invoices(AbstractSuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<Detail> Get(int ID)
        {
            var result = await superFaktura.Get(string.Format("invoices/view/{0}.json", ID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Detail>(result);
        }

        public async Task<Dictionary<int, Detail>> Get(IEnumerable<int> IDS)
        {
            var result = await superFaktura.Get(string.Format("invoices/getInvoiceDetails/{0}", string.Join(",", IDS))).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Dictionary<int, Detail>>(result);
        }

        public async Task<PagedResponse> Get(Request.Invoice.Filter filter, bool listInfo = true)
        {
            var result = await superFaktura.Get(string.Format("invoices/index.json{0}", filter.ToParameters(listInfo))).ConfigureAwait(false);
            if (listInfo)
            {
                return superFaktura.DeserializeResult<PagedResponse>(result);
            }
            else
            {
                return new PagedResponse { Items = superFaktura.DeserializeResult<ItemList<Detail>>(result) };
            }
        }

        public async Task<ResponsePayment> Pay(Request.Invoice.Payment payment)
        {
            var result = await superFaktura.Post("invoice_payments/add/ajax:1/api:1", new InvoicePaymentData { InvoicePayment = payment }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<ResponsePayment>(result);
        }

        public async Task<Response<DetailBasic>> NotPay(int invoiceID)
        {
            var result = await superFaktura.Get(string.Format("invoices/will_not_be_paid/{0}", invoiceID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<DetailBasic>>(result);
        }

        public async Task<byte[]> GetPdf(int invoiceId, string token, string language = LanguageType.Slovak)
        {
            if (!LanguageType.Languages.Contains(language))
            {
                language = LanguageType.Slovak;
            }
            var result = await superFaktura.GetByte(string.Format("{0}/invoices/pdf/{1}/token:{2}", language, invoiceId, token)).ConfigureAwait(false);
            //Code below tests if response is a SuperFaktura error response or PDF File
            try
            {
                string testResult = Encoding.UTF8.GetString(result);
                superFaktura.DeserializeResult<Response<ExpandoObject>>(testResult);
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

        public async Task<byte[]> Exports(int[] invoiceIds, Export export)
        {
            var result = await superFaktura.PostByte("/exports", new Request.DataData
            {
                Data = new { 
                    Invoice = new { 
                        ids = invoiceIds
                    },
                    Export = export
                }
            }).ConfigureAwait(false);
            try
            {
                string testResult = Encoding.UTF8.GetString(result);
                superFaktura.DeserializeResult<Response<ExpandoObject>>(testResult);
            }
            catch (Exceptions.ParseException) when (result != null && result.Length != 0)
            {
                //test deserialization failed. it is a binary file
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<Response<ExpandoObject>> SetInvoiceLanguage(int ID, string language = LanguageType.Slovak)
        {
            if (!LanguageType.Languages.Contains(language))
            {
                language = LanguageType.Slovak;
            }
            // error response fix
            var result = await superFaktura.Get(string.Format("invoices/setinvoicelanguage/{0}/lang:{1}", ID, language)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }

        public async Task<Response<ExpandoObject>> MarkAsSent(MarkEmail email)
        {
            var result = await superFaktura.Post("invoices/mark_as_sent", new MarkEmailData { InvoiceEmail = email }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }

        public async Task<Response<ResponseEmail>> SendEmail(Request.Invoice.Email email)
        {
            var result = await superFaktura.Post("invoices/send", new EmailData { Email = email }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ResponseEmail>>(result);
        }

        public async Task<Response<Detail>> Save(Request.Invoice.Invoice invoice, Client client, Request.Invoice.Item[] items, int[] tags = null, Setting setting = null, Extra extra = null, Request.Invoice.MyData myData = null)
        {
            var data = new InvoiceData
            {
                Invoice = invoice,
                InvoiceItem = items,
                Tag = tags,
                Client = client,
                InvoiceSetting =  new InvoiceSettings
                {
                    Settings = JsonConvert.SerializeObject(setting ?? new Setting())
                },
                InvoiceExtra = extra,
                MyData = myData
            };

            var result = await superFaktura.Post("/invoices/create", data).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<Detail>>(result);
        }

        public async Task<Response<DetailData>> Update(Request.Invoice.Invoice invoice, Client client, Request.Invoice.Item[] items, int[] tags = null, Setting setting = null, Extra extra = null, Request.Invoice.MyData myData = null)
        {
            var data = new InvoiceData
            {
                Invoice = invoice,
                InvoiceItem = items,
                Tag = tags,
                Client = client,
                InvoiceSetting = new InvoiceSettings
                {
                    Settings = JsonConvert.SerializeObject(setting ?? new Setting())
                },
                InvoiceExtra = extra,
                MyData = myData
            };

            var result = await superFaktura.Post("/invoices/Edit", data).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<DetailData>>(result);
        }

        public async Task<Response<DetailInvoice>> DeleteItem(int invoiceID, int itemID)
        {
            return await DeleteItem(invoiceID, new int[] { itemID }).ConfigureAwait(false);
        }

        public async Task<Response<DetailInvoice>> DeleteItem(int invoiceID, int[] itemID)
        {
            if (itemID?.Any() == true)
            {
                var result = await superFaktura.Get(string.Format("invoice_items/delete/{1}/invoice_id:{0}", invoiceID, string.Join(",", itemID))).ConfigureAwait(false);
                return superFaktura.DeserializeResult<Response<DetailInvoice>>(result);
            }
            return null;
        }

        public async Task<Response<ExpandoObject>> Delete(int invoiceID)
        {
            var result = await superFaktura.Get(string.Format("invoices/delete/{0}", invoiceID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }

        public async Task<Response<ExpandoObject>> MarkAsSend(int invoiceID)
        {
            var result = await superFaktura.Get(string.Format("invoices/mark_sent/{0}", invoiceID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }

        public async Task<Response<DetailBasic>> SendPost(Post post)
        {
            var result = await superFaktura.Post("invoices/post", new PostData { Post = post }).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<DetailBasic>>(result);
        }

        public async Task<Response<ExpandoObject>> DeletePayment(int invoicePaymentID)
        {
            var result = await superFaktura.Get(string.Format("/invoice_payments/delete/{0}", invoicePaymentID)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }

        public async Task<Response<ExpandoObject>> SendSMS(SMS data)
        {
            var result = await superFaktura.Post(string.Format("/sms/send"), data).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }

        public async Task<Detail[]> GetInvoiceDetails(IEnumerable<int> ids)
        {
            if (ids?.Any() == true)
            {
                var result = await superFaktura.Get(string.Format("/invoices/getInvoiceDetails/{0}", string.Join(",", ids.Distinct()))).ConfigureAwait(false);
                return superFaktura.DeserializeResult<Detail[]>(result);
            }
            return null;
        }

        public async Task<Response<Detail>> CreateFromProforma(int proformaID)
        {
            var proforma = await superFaktura.Get(string.Format("/invoices/regular.json/{0}", proformaID)).ConfigureAwait(false);
            var data =  superFaktura.DeserializeResult<ExpandoObject>(proforma);
            var result = await superFaktura.Post("/invoices/create", new Request.DataData { Data =  data}).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<Detail>>(result);
        }

        public async Task<Response<ExpandoObject>> SetEstimateStatus(int estimateID, int status)
        {
            var result = await superFaktura.Get(string.Format("/invoices/set_estimate_status/{0}/{1}/ajax:1", estimateID, status)).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Response<ExpandoObject>>(result);
        }
    }
}
