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

        internal async Task<string> Get(string uri)
        {
            using (var client = CreateClient())
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            return await Task.FromResult<string>(null);
        }

        internal async Task<byte[]> GetByte(string uri)
        {
            using (var client = CreateClient())
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
            }
            return await Task.FromResult<byte[]>(null);
        }

        internal async Task<string> Post(string uri, string data)
        {
            using (var client = CreateClient())
            {
                HttpResponseMessage response = await client.PostAsync(uri, new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("data", data) }));
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            return await Task.FromResult<string>(null);
        }

        internal async Task<string> Post(string uri, object data)
        {
            return await Post(uri, JsonConvert.SerializeObject(data));
        }

        public async Task<Dictionary<int, string>> GetCountries()
        {
            return JsonConvert.DeserializeObject<Dictionary<int, string>>(await Get(string.Format("{0}countries", APIURL)));
        }

        public async Task<Dictionary<string, Sequence[]>> GetSequences()
        {
            return JsonConvert.DeserializeObject<Dictionary<string, Sequence[]>>(await Get("sequences/index.json"));
        }

        public async Task<Dictionary<int, string>> GetTags()
        {
            return JsonConvert.DeserializeObject<Dictionary<int, string>>(await Get("tags/index.json"));
        }

        public async Task<Logo[]> GetLogos()
        {
            return JsonConvert.DeserializeObject<Logo[]>(await Get("/users/logo"));
        }

        public async Task<Response<Register>> Register(string email, bool sendEmail = true)
        {
            return JsonConvert.DeserializeObject<Response<Register>>(await Post("/users/create", new
            {
                User =  new User {
                    Email = email,
                    SendEmail = sendEmail
                }
            }));
        }

        public static IEnumerable<RegionInfo> GetRegionInfos()
        {
            return CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(c => new RegionInfo(c.LCID))
                .Distinct()
                .OrderBy(c => c.TwoLetterISORegionName);
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
