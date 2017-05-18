using Birko.SuperFaktura.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Birko.SuperFaktura
{
    public abstract class AbstractSuperFaktura
    {
        public const string APIAUTHKEYWORD = "SFAPI";
        public string APIURL { get; protected set; }

        public string Email { get; private set; }
        public string ApiKey { get; private set; }
        public int? CompanyId { get; private set; }
        public string AppTitle { get; private set; }
        public string Module { get; private set; }

        public bool EnsureSuccessStatusCode { get; set; } = true;
        public int TimeoutSeconds { get; set; } = 30;

        private DateTime? _lastRequest = null;

        public AbstractSuperFaktura(string email, string apiKey, string apptitle = null, string module = "API", int? companyId = null)
        {
            Email = email;
            ApiKey = apiKey;
            CompanyId = companyId;
            AppTitle = apptitle;
            Module = module;
        }

        protected HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.Timeout = new TimeSpan(0, 0, 0, TimeoutSeconds, 0);
            client.BaseAddress = new Uri(APIURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", string.Format("{0} email={1}&apikey={2}&company_id={3}", APIAUTHKEYWORD, Email, ApiKey, CompanyId));

            return client;
        }

        internal T DeserializeResult<T>(string result, JsonSerializerSettings setting = null)
        {
            TestError(result);
            try
            {
                return JsonConvert.DeserializeObject<T>(result, setting);
            }
            catch (Exception ex)
            {
                var exception = new Exceptions.ParseException(ex.Message, string.Format("Returned Response: {0}", result));
                throw (exception);
            }
        }

        internal static void TestError(string result)
        {
            try
            {
                var testResult = JsonConvert.DeserializeObject<Response<ExpandoObject>>(result);
                if (testResult.Error != null && testResult.Error.Value > 0)
                {
                    var exception = new Exceptions.Exception(testResult.Error.Value, testResult.Message, testResult.ErrorMessage);
                    throw (exception);
                }
            }
            catch (Exception ex)
            {
                var exception = new Exceptions.ParseException(ex.Message, string.Format("Returned Response: {0}", result));
                throw (exception);
            }
        }

        private void RequestDelay()
        {
            DateTime now = DateTime.Now;
            if (_lastRequest != null && (now - _lastRequest.Value).Seconds <= 1)
            {
                Task.Delay(new TimeSpan(0, 0, 0, 1, 0)).Wait();
                now = DateTime.Now;
            }
            _lastRequest = now;
        }

        internal async Task<string> Get(string uri)
        {
            RequestDelay();
            using (var client = CreateClient())
            {
                HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);
                if (EnsureSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                }
                if (response.IsSuccessStatusCode || !EnsureSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
            }
            return await Task.FromResult<string>(null).ConfigureAwait(false);
        }

        internal async Task<byte[]> GetByte(string uri)
        {
            RequestDelay();
            using (var client = CreateClient())
            {
                HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);
                if (EnsureSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                }
                if (response.IsSuccessStatusCode || !EnsureSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                }
            }
            return await Task.FromResult<byte[]>(null).ConfigureAwait(false);
        }

        internal async Task<string> Post(string uri, string data)
        {
            RequestDelay();
            using (var client = CreateClient())
            {
                HttpResponseMessage response = await client.PostAsync(uri, new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("data", data) })).ConfigureAwait(false);
                if (EnsureSuccessStatusCode || !EnsureSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                }
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
            }
            return await Task.FromResult<string>(null).ConfigureAwait(false);
        }

        internal async Task<string> Post(string uri, object data)
        {
            return await Post(uri, JsonConvert.SerializeObject(data)).ConfigureAwait(false);
        }
    }

    public class SuperFaktura : AbstractSuperFaktura
    {
        private Clients _clients = null;
        private Expenses _expenses = null;
        private Invoices _invoices = null;
        private Stock _stock = null;

        public Clients Clients {
            get {
                if (_clients == null) {
                    _clients = new Clients(this);
                }
                return _clients;
            }
        }

        public Expenses Expenses {
            get
            {
                if (_expenses == null)
                {
                    _expenses = new Expenses(this);
                }
                return _expenses;
            }
        }
        public Invoices Invoices {
            get
            {
                if (_invoices == null)
                {
                    _invoices = new Invoices(this);
                }
                return _invoices;
            }
        }

        public Stock Stock {
            get
            {
                if (_stock== null)
                {
                    _stock = new Stock(this);
                }
                return _stock;
            }
        }

        public SuperFaktura(string email, string apiKey, string apptitle = null, string module = "API", int? companyId = null)
            : base(email,apiKey, apptitle, module, companyId)
        {
            APIURL = "https://moja.superfaktura.sk/";
        }

        public async Task<Dictionary<int, string>> GetCountries()
        {
            var result = await Get(string.Format("{0}countries", APIURL)).ConfigureAwait(false);
            try
            {
                return DeserializeResult<Dictionary<int, string>>(result);
            }
            catch (JsonSerializationException ex)
            {
                try
                {
                    DeserializeResult<string[]>(result);
                    return null;
                }
                catch
                {
                    throw ex;
                }
            }
        }

        public async Task<Dictionary<string, Sequence[]>> GetSequences()
        {
            var result = await Get("sequences/index.json").ConfigureAwait(false);
            try
            {
                return DeserializeResult<Dictionary<string, Sequence[]>>(result);
            }
            catch (JsonSerializationException ex)
            {
                try
                {
                    DeserializeResult<Sequence[][]>(result);
                    return null;
                }
                catch
                {
                    throw ex;
                }
            }
        }

        public async Task<Dictionary<int, string>> GetTags()
        {
            var result = await Get("tags/index.json").ConfigureAwait(false);
            try
            {
                return DeserializeResult<Dictionary<int, string>>(result);
            }
            catch (JsonSerializationException ex)
            {
                try
                {
                    DeserializeResult<string[]>(result);
                    return null;
                }
                catch
                {
                    throw ex;
                }
            }
        }

        public async Task<Logo[]> GetLogos()
        {
            var result = await Get("/users/logo").ConfigureAwait(false);
            return DeserializeResult<Logo[]>(result);
        }

        public async Task<Response<Register>> Register(string email, bool sendEmail = true)
        {
            var result = await Post("/users/create", new
            {
                User = new User
                {
                    Email = email,
                    SendEmail = sendEmail
                }
            });
            return DeserializeResult<Response<Register>>(result);
        }
    }


    public class SuperFakturaCZ : SuperFaktura
    {
        public SuperFakturaCZ(string email, string apiKey, string apptitle = null, string module = "API", int? companyId = null)
            : base(email, apiKey, apptitle, module, companyId)
        {
            APIURL = "https://moje.superfaktura.cz/";
        }
    }

    public class SuperFakturaAT : SuperFaktura
    {
        public SuperFakturaAT(string email, string apiKey, string apptitle = null, string module = "API", int? companyId = null)
            : base(email, apiKey, apptitle, module, companyId)
        {
            APIURL = "http://meine.superfaktura.at/";
        }
    }
}
