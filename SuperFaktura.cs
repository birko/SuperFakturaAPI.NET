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
    public class SuperFaktura
    {
        public const string APIAUTHKEYWORD = "SFAPI";
        public const string APIURL = "https://moja.superfaktura.sk/";
        public bool EnsureSuccessStatusCode { get; set; } = true;

        public string Email { get; private set; }
        public string ApiKey { get; private set; }
        public int? CompanyId { get; private set; }
        public string AppTitle { get; private set; }
        public string Module { get; private set; }
        public int TimeoutSeconds { get; set; } = 30;

        public Clients Clients { get; private set; }
        public Expenses Expenses { get; private set; }
        public Invoices Invoices { get; private set; }
        public Stock Stock { get; private set; }


        public SuperFaktura(string email, string apiKey, string apptitle = null, string module = "API", int? companyId = null)
        {
            Email = email;
            ApiKey = apiKey;
            CompanyId = companyId;
            AppTitle = apptitle;
            Module = module;
            Clients = new Clients(this);
            Expenses = new Expenses(this);
            Invoices = new Invoices(this);
            Stock = new Stock(this);
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

        internal T DeserializeResult<T>(string result)
        {
            TestError(result);
            return JsonConvert.DeserializeObject<T>(result);
        }

        internal static void TestError(string result)
        {
            var testResult = JsonConvert.DeserializeObject<Response<ExpandoObject>>(result);
            if (testResult.Error != null && testResult.Error.Value > 0)
            {
                var exception = new Exceptions.Exception(testResult.Error.Value, testResult.Message, testResult.ErrorMessage);
                throw (exception);
            }
        }

        internal async Task<string> Get(string uri)
        {
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

        public async Task<Dictionary<int, string>> GetCountries()
        {
            var result = await Get(string.Format("{0}countries", APIURL)).ConfigureAwait(false);
            return DeserializeResult<Dictionary<int, string>>(result);
        }

        public async Task<Dictionary<string, Sequence[]>> GetSequences()
        {
            var result = await Get("sequences/index.json").ConfigureAwait(false);
            return DeserializeResult<Dictionary<string, Sequence[]>>(result);
        }

        public async Task<Dictionary<int, string>> GetTags()
        {
            var result = await Get("tags/index.json").ConfigureAwait(false);
            return DeserializeResult<Dictionary<int, string>>(result);
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
        public new const string APIURL = "https://moje.superfaktura.cz/";

        public SuperFakturaCZ(string email, string apiKey, string apptitle = null, string module = "API", int? companyId = null)
            : base(email, apiKey, apptitle, module, companyId)
        {
        }
    }

    public class SuperFakturaAT : SuperFaktura
    {
        public new const string APIURL = "http://meine.superfaktura.at/";

        public SuperFakturaAT(string email, string apiKey, string apptitle = null, string module = "API", int? companyId = null)
            : base(email, apiKey, apptitle, module, companyId)
        {
        }
    }
}
