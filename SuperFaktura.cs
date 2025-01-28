using Birko.SuperFaktura.Request;
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

        public string Email { get; }
        public string ApiKey { get; }
        public int? CompanyId { get; internal set; }
        public string AppTitle { get; }
        public string Module { get; }

        public bool EnsureSuccessStatusCode { get; set; } = true;
        public int TimeoutSeconds { get; set; } = 30;

        public RateLimits RateLimit { get; set; } = null;

        private static Dictionary<string, DateTime> _lastRequest = null;
        private static Dictionary<string, HttpClient> _clientList = null;
        private static Dictionary<string, int> _requestCount = null;

        public string LastCheckSum { get; private set; }

        private string ProfileKey
        {
            get
            {
                return string.Format("{0}-{1}", APIURL, ApiKey);
            }
        }

        protected AbstractSuperFaktura(string email, string apiKey, string apptitle = null, string module = "API", int? companyId = null)
        {
            Email = email;
            ApiKey = apiKey;
            CompanyId = companyId;
            AppTitle = apptitle;
            Module = module;
        }

        protected HttpClient CreateClient(bool force = false)
        {
            if (_clientList == null)
            {
                _clientList = new Dictionary<string, HttpClient>();
            }

            var key = ProfileKey;
            HttpClient client;
            if (force || !_clientList.ContainsKey(key) || _clientList[key].Timeout.Seconds != TimeoutSeconds)
            {
                client = new HttpClient
                {
                    BaseAddress = new Uri(APIURL),
                    Timeout = new TimeSpan(0, 0, 0, TimeoutSeconds, 0)
                };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", string.Format("{0} email={1}&apikey={2}&company_id={3}&module={4}", APIAUTHKEYWORD, Email, ApiKey, CompanyId, Module));
                _clientList[key] = client;
            }
            else
            {
                client = _clientList[key];
            }

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
                var exception = new Exceptions.ParseException(ex.Message, string.Format("Returned Response: {0}", result), ex);
                throw (exception);
            }
        }

        internal static void TestError(string result)
        {
            try
            {
                var testResult = JsonConvert.DeserializeObject<ErrorMessageResponse>(result);
                if (testResult.Error != null && testResult.Error.Value > 0)
                {
                    var testMessageResult = JsonConvert.DeserializeObject<StringMessageResponse>(result);
                    var exception = new Exceptions.Exception(testResult.Error.Value, testMessageResult.Message, testResult.ErrorMessage);
                    throw (exception);
                }
            }
            catch (Exception ex)
            {
                //Test if response is not an array;
                try
                {
                    var testResult = JsonConvert.DeserializeObject<ExpandoObject[]>(result);
                }
                catch
                {
                    try
                    {
                        var testResult = JsonConvert.DeserializeObject<string[]>(result);
                    }
                    catch
                    {
                        var exception = new Exceptions.ParseException(ex.Message, string.Format("Returned Response: {0}", result));
                        throw (exception);
                    }
                }
            }
        }

        private void RequestDelay()
        {
            DateTime now = DateTime.Now;
            if (_lastRequest == null)
            {
                _lastRequest = new Dictionary<string, DateTime>();
            }
            if (_requestCount == null)
            {
                _requestCount = new Dictionary<string, int>();
            }
            var key = ProfileKey;
            //request throttling
            if (_requestCount.ContainsKey(key) && _requestCount[key] >= 1000)
            {
                Task.Delay(TimeSpan.FromSeconds(5)).Wait();
            }
            else if (_lastRequest.ContainsKey(key) && (now - _lastRequest[key]).Seconds <= 1)
            {
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            }
            now = DateTime.Now;
            _lastRequest[key] = now;
        }

        private void ParseResponse(HttpResponseMessage response)
        {
            if (response != null)
            {
                if (response.Headers?.Any() == true)
                {
                    if (
                        response.Headers.Contains("X-RateLimit-DailyLimit")
                        || response.Headers.Contains("X-RateLimit-DailyRemaining")
                        || response.Headers.Contains("X-RateLimit-DailyReset")
                        || response.Headers.Contains("X-RateLimit-MonthlyLimit")
                        || response.Headers.Contains("X-RateLimit-MonthlyRemaining")
                        || response.Headers.Contains("X-RateLimit-MonthlyReset")
                        || response.Headers.Contains("X-RateLimit-Message")
                    )
                    {
                        if (RateLimit == null)
                        {
                            RateLimit = new RateLimits();
                        }
                        RateLimit.Message = response.Headers.Contains("X-RateLimit-Message") ? response.Headers.GetValues("X-RateLimit-Message").Last() : null;
                        if (
                            response.Headers.Contains("X-RateLimit-DailyLimit")
                            && long.TryParse(response.Headers.GetValues("X-RateLimit-DailyLimit").Last(), out long limit)
                        )
                        {
                            if (RateLimit.Daily == null)
                            {
                                RateLimit.Daily = new DetailLimit();
                            }
                            RateLimit.Daily.Limit = limit;
                        }
                        if (
                            response.Headers.Contains("X-RateLimit-DailyRemaining")
                            && long.TryParse(response.Headers.GetValues("X-RateLimit-DailyRemaining").Last(), out limit)
                        )
                        {
                            if (RateLimit.Daily == null)
                            {
                                RateLimit.Daily = new DetailLimit();
                            }
                            RateLimit.Daily.Remaining = limit;
                        }
                        if (
                            response.Headers.Contains("X-RateLimit-DailyReset")
                            && DateTime.TryParseExact(response.Headers.GetValues("X-RateLimit-DailyReset").Last(), "dd.MM.yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date)
                        )
                        {
                            if (RateLimit.Daily == null)
                            {
                                RateLimit.Daily = new DetailLimit();
                            }
                            RateLimit.Daily.Reset = date;
                        }
                        if (
                            response.Headers.Contains("X-RateLimit-MonthlyLimit")
                            && long.TryParse(response.Headers.GetValues("X-RateLimit-MonthlyLimit").Last(), out limit)
                        )
                        {
                            if (RateLimit.Monthly == null)
                            {
                                RateLimit.Monthly = new DetailLimit();
                            }
                            RateLimit.Monthly.Limit = limit;
                        }
                        if (
                            response.Headers.Contains("X-RateLimit-MonthlyRemaining")
                            && long.TryParse(response.Headers.GetValues("X-RateLimit-MonthlyRemaining").Last(), out limit)
                        )
                        {
                            if (RateLimit.Monthly == null)
                            {
                                RateLimit.Monthly = new DetailLimit();
                            }
                            RateLimit.Monthly.Remaining = limit;
                        }
                        if (
                            response.Headers.Contains("X-RateLimit-MonthlyReset")
                            && DateTime.TryParseExact(response.Headers.GetValues("X-RateLimit-MonthlyReset").Last(), "dd.MM.yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                        {
                            if (RateLimit.Monthly == null)
                            {
                                RateLimit.Monthly = new DetailLimit();
                            }
                            RateLimit.Monthly.Reset = date;
                        }
                    }
                }
                if (EnsureSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        internal async Task<string> Get(string uri)
        {
            RequestDelay();
            var client = CreateClient();
            HttpResponseMessage response = null;
            try
            {
                IncreaseRequestCount(ProfileKey);
                response = await client.GetAsync(uri).ConfigureAwait(false);
                ParseResponse(response);
                if (response.IsSuccessStatusCode || !EnsureSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                return await Task.FromResult<string>(null).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw HandleRequestException(uri, response, ex);
            }
        }

        internal async Task<byte[]> GetByte(string uri)
        {
            RequestDelay();
            var client = CreateClient();
            HttpResponseMessage response = null;
            try
            {
                IncreaseRequestCount(ProfileKey);
                response = await client.GetAsync(uri).ConfigureAwait(false);
                ParseResponse(response);
                if (response.IsSuccessStatusCode || !EnsureSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                }
                return await Task.FromResult<byte[]>(null).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw HandleRequestException(uri, response, ex);
            }
        }

        internal async Task<string> Post(string uri, string data, string checkSum = null)
        {
            RequestDelay();
            var client = CreateClient();
            HttpResponseMessage response = null;
            try
            {
                IncreaseRequestCount(ProfileKey);
                this.LastCheckSum = checkSum;
                response = await client.PostAsync(uri, new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("data", data) })).ConfigureAwait(false);
                ParseResponse(response);
                if (response.IsSuccessStatusCode || !EnsureSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                return await Task.FromResult<string>(null).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw HandleRequestException(uri, response, ex, data);
            }
        }

        internal async Task<byte[]> PostByte(string uri, string data, string checkSum = null)
        {
            RequestDelay();
            var client = CreateClient();
            HttpResponseMessage response = null;
            try
            {
                IncreaseRequestCount(ProfileKey);
                this.LastCheckSum = checkSum;
                response = await client.PostAsync(uri, new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("data", data) })).ConfigureAwait(false);
                ParseResponse(response);
                if (response.IsSuccessStatusCode || !EnsureSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                }
                return await Task.FromResult<byte[]>(null).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw HandleRequestException(uri, response, ex, data);
            }
        }

        internal async Task<string> Post(string uri, object data)
        {
            string checkSum = null;
            if (data is Data dataData)
            {
                checkSum = CheckSum(dataData);
                dataData.CheckSum = checkSum;
            }
#if DEBUG
            Console.WriteLine(JsonConvert.SerializeObject(data));
#endif
            return await Post(uri, JsonConvert.SerializeObject(data), checkSum).ConfigureAwait(false);
        }

        internal async Task<byte[]> PostByte(string uri, object data)
        {
            string checkSum = null;
            if (data is Data dataData)
            {
                checkSum = CheckSum(dataData);
                dataData.CheckSum = checkSum;
            }
#if DEBUG
            Console.WriteLine(JsonConvert.SerializeObject(data));
#endif
            return await PostByte(uri, JsonConvert.SerializeObject(data), checkSum).ConfigureAwait(false);
        }

        internal async Task<string> Patch(string uri, string data, string checkSum = null)
        {
            RequestDelay();
            var client = CreateClient();
            HttpResponseMessage response = null;
            try
            {
                IncreaseRequestCount(ProfileKey);
                this.LastCheckSum = checkSum;
                HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), uri)
                {
                    Content = new StringContent(data, Encoding.UTF8, "application/json")
                };
                response = await client.SendAsync(request).ConfigureAwait(false);
                ParseResponse(response);
                if (response.IsSuccessStatusCode || !EnsureSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                return await Task.FromResult<string>(null).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw HandleRequestException(uri, response, ex, data);
            }
        }

        internal async Task<string> Patch(string uri, object data)
        {
            string checkSum = null;
            if (data is Data dataData)
            {
                checkSum = CheckSum(dataData);
                dataData.CheckSum = checkSum;
            }
#if DEBUG
            Console.WriteLine(JsonConvert.SerializeObject(data));
#endif
            return await Patch(uri, JsonConvert.SerializeObject(data), checkSum).ConfigureAwait(false);
        }

        internal async Task<string> Delete(string uri)
        {
            RequestDelay();
            var client = CreateClient();
            HttpResponseMessage response = null;
            try
            {
                IncreaseRequestCount(ProfileKey);
                response = await client.DeleteAsync(uri).ConfigureAwait(false);
                ParseResponse(response);
                if (response.IsSuccessStatusCode || !EnsureSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                return await Task.FromResult<string>(null).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw HandleRequestException(uri, response, ex);
            }
        }

        private string CheckSum(Request.Data data)
        {
            data.Date = DateTime.Now.ToString("yyyy-MM-dd");
            StringBuilder sum = new StringBuilder();
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data)));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sum.Append(hashBytes[i].ToString("X2"));
                }
            }
            return data.CheckSum;
        }

        private static void IncreaseRequestCount(string key, int count = 1, bool set = false)
        {
            if (_requestCount == null)
            {
                _requestCount = new Dictionary<string, int>();
            }
            if (!_requestCount.ContainsKey(key))
            {
                _requestCount.Add(key, 0);
            }
            if (set)
            {
                _requestCount[key] = count;
            }
            else
            {
                _requestCount[key] += count;
            }
        }

        private Exceptions.Exception HandleRequestException(string uri, HttpResponseMessage response, Exception ex, string data = null)
        {
            string content = null;
            if (response != null)
            {
                var contentTask = response.Content.ReadAsStringAsync();
                contentTask.ConfigureAwait(false);
                contentTask.Wait();
                content = contentTask.Result;
            }
            string message = string.Format("ReasonPhrase: {0}\nContent: {1}\nRequest: {2}\nData: {3}",
                    response,
                    content,
                    response?.RequestMessage,
                    data
                );
            return new Exceptions.Exception(null, message, ex, uri);
        }
    }

    public class SuperFaktura : AbstractSuperFaktura
    {
        private BankAccounts _bankAccounts = null;
        private CashRegisters _cashRegisters = null;
        private Clients _clients = null;
        private ContactPersons _contactPersons = null;
        private Expenses _expenses = null;
        private Exports _exports = null;
        private Invoices _invoices = null;
        private Other _other = null;
        private Stock _stock = null;
        private Tags _tags = null;
        private ValueLists _valueList = null;


        public BankAccounts BankAccounts
        {
            get
            {
                return _bankAccounts ?? (_bankAccounts = new BankAccounts(this));
            }
        }

        public CashRegisters CashRegisters
        {
            get
            {
                return _cashRegisters ?? (_cashRegisters = new CashRegisters(this));
            }
        }

        public Clients Clients
        {
            get
            {
                return _clients ?? (_clients = new Clients(this));
            }
        }

        public ContactPersons ContactPersons
        {
            get
            {
                return _contactPersons ?? (_contactPersons = new ContactPersons(this));
            }
        }

        public Expenses Expenses
        {
            get
            {
                return _expenses ?? (_expenses = new Expenses(this));
            }
        }

        public Exports Exports
        {
            get
            {
                return _exports ?? (_exports = new Exports(this));
            }
        }

        public Invoices Invoices
        {
            get
            {
                return _invoices ?? (_invoices = new Invoices(this));
            }
        }

        public Other Other
        {
            get
            {
                return _other ?? (_other = new Other(this));
            }
        }

        public Stock Stock
        {
            get
            {
                return _stock ?? (_stock = new Stock(this));
            }
        }


        public Tags Tags
        {
            get
            {
                return _tags ?? (_tags = new Tags(this));
            }
        }

        public ValueLists ValueLists
        {
            get
            {
                return _valueList ?? (_valueList = new ValueLists(this));
            }
        }

        public SuperFaktura(string email, string apiKey, string apptitle = null, string module = "API", int? companyId = null)
            : base(email,apiKey, apptitle, module, companyId)
        {
            APIURL = "https://moja.superfaktura.sk/";
        }

        public async Task<Response<Register>> Register(string email, bool sendEmail = true)
        {
            var result = await Post("/users/create", new Request.UserData
            {
                User = new Request.User
                {
                    Email = email,
                    SendEmail = sendEmail
                }
            }).ConfigureAwait(false);
            return DeserializeResult<Response<Register>>(result);
        }


        public async Task<Response<ExpandoObject>> GetCourierData(string curierType, object data)
        {
            if (new[] { "slp", "csp" }.Contains(curierType))
            {
                var result = await Post($"{curierType}_exports/export", new Request.DataData { Data = data }).ConfigureAwait(false);
                return DeserializeResult<Response<ExpandoObject>>(result);
            }
            return null;
        }

        public async Task<string> ResponseByChecksum(string checksum)
        {
            return await Get($"api_logs/getResponseByChecksum/{checksum}").ConfigureAwait(false);
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
            APIURL = "https://meine.superfaktura.at/";
        }
    }

    public class SuperFakturaSandbox : SuperFaktura
    {
        public SuperFakturaSandbox(string email, string apiKey, string apptitle = null, string module = "API", int? companyId = null)
            : base(email, apiKey, apptitle, module, companyId)
        {
            APIURL = "https://sandbox.superfaktura.sk/";
        }
    }

    public class SuperFakturaSandboxCZ : SuperFaktura
    {
        public SuperFakturaSandboxCZ(string email, string apiKey, string apptitle = null, string module = "API", int? companyId = null)
            : base(email, apiKey, apptitle, module, companyId)
        {
            APIURL = "https://sandbox.superfaktura.cz/";
        }
    }
}
