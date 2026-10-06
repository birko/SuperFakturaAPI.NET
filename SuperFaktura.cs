using Birko.SuperFaktura.Request;
using Birko.SuperFaktura.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
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

        private static readonly ConcurrentDictionary<string, DateTime> _lastRequest = new ConcurrentDictionary<string, DateTime>();
        private static readonly ConcurrentDictionary<string, HttpClient> _clientList = new ConcurrentDictionary<string, HttpClient>();
        // Requests made per profile today; the API limit is 1000 requests per day (faq.md).
        private static readonly Dictionary<string, KeyValuePair<DateTime, int>> _requestCount = new Dictionary<string, KeyValuePair<DateTime, int>>();
        private static readonly object _requestCountLock = new object();

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

        // All values need to be URL encoded (intro.md > Authentication), e.g. hello+world@example.com -> hello%2Bworld%40example.com.
        private string AuthorizationHeader
        {
            get
            {
                return string.Format("{0} email={1}&apikey={2}&company_id={3}&module={4}",
                    APIAUTHKEYWORD,
                    Uri.EscapeDataString(Email ?? string.Empty),
                    Uri.EscapeDataString(ApiKey ?? string.Empty),
                    CompanyId,
                    Uri.EscapeDataString(Module ?? string.Empty));
            }
        }

        protected HttpClient CreateClient(bool force = false)
        {
            // Cached per URL + full credentials, so clients sharing an API key but differing in
            // email, module or company_id never reuse each other's Authorization header.
            var authorization = AuthorizationHeader;
            var key = string.Format("{0} {1}", APIURL, authorization);
            HttpClient client;
            if (force || !_clientList.ContainsKey(key) || _clientList[key].Timeout.TotalSeconds != TimeoutSeconds)
            {
                client = new HttpClient
                {
                    BaseAddress = new Uri(APIURL),
                    Timeout = new TimeSpan(0, 0, 0, TimeoutSeconds, 0)
                };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorization);
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
            JToken token;
            try
            {
                token = JToken.Parse(result);
            }
            catch (Exception ex)
            {
                throw new Exceptions.ParseException(ex.Message, string.Format("Returned Response: {0}", result), ex);
            }

            var apiError = CreateApiError(token);
            if (apiError != null)
            {
                throw apiError;
            }
        }

        private static Exceptions.Exception CreateApiError(JToken token, Exception inner = null, string url = null)
        {
            // Arrays and other non-object responses never carry an error code.
            if (!(token is JObject response)
                || !int.TryParse(response["error"]?.ToString(), out int error)
                || error <= 0)
            {
                return null;
            }

            // "message" and "error_message" are either a string or an object (e.g. validation errors per field).
            return new Exceptions.Exception(error, TokenToText(response["message"]), TokenToText(response["error_message"]), inner, url);
        }

        private static string TokenToText(JToken token)
        {
            if (token == null || token.Type == JTokenType.Null)
            {
                return null;
            }
            return token.Type == JTokenType.String ? token.Value<string>() : token.ToString(Formatting.None);
        }

        private void RequestDelay()
        {
            DateTime now = DateTime.Now;
            var key = ProfileKey;
            //request throttling
            if (RequestCount(key, now) >= 1000)
            {
                Task.Delay(TimeSpan.FromSeconds(5)).Wait();
            }
            else if (_lastRequest.TryGetValue(key, out DateTime last) && (now - last).TotalSeconds <= 1)
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
                            && ParseRateLimitReset(response.Headers.GetValues("X-RateLimit-DailyReset").Last()) is DateTime dailyReset
                        )
                        {
                            if (RateLimit.Daily == null)
                            {
                                RateLimit.Daily = new DetailLimit();
                            }
                            RateLimit.Daily.Reset = dailyReset;
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
                            && ParseRateLimitReset(response.Headers.GetValues("X-RateLimit-MonthlyReset").Last()) is DateTime monthlyReset)
                        {
                            if (RateLimit.Monthly == null)
                            {
                                RateLimit.Monthly = new DetailLimit();
                            }
                            RateLimit.Monthly.Reset = monthlyReset;
                        }
                    }
                }
                if (EnsureSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        // intro.md "Limit headers": e.g. "24.12.2030 00:00:00" (24-hour clock).
        internal static DateTime? ParseRateLimitReset(string value)
        {
            return DateTime.TryParseExact(value, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date)
                ? date
                : (DateTime?)null;
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

        internal async Task<string> Delete(string uri, object data)
        {
            return await Delete(uri, JsonConvert.SerializeObject(data)).ConfigureAwait(false);
        }

        internal async Task<string> Delete(string uri, string data)
        {
            RequestDelay();
            var client = CreateClient();
            HttpResponseMessage response = null;
            try
            {
                IncreaseRequestCount(ProfileKey);
                response = await client.SendAsync(CreateDeleteRequest(uri, data)).ConfigureAwait(false);
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

        // Relative URI is resolved against the client's BaseAddress; body is sent as "data=<json>" like POST.
        internal static HttpRequestMessage CreateDeleteRequest(string uri, string data)
        {
            return new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri, UriKind.Relative),
                Content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("data", data) })
            };
        }

        // The checksum is a caller-chosen unique identifier (e.g. order number), max 32 chars.
        // It allows fetching a lost response via ResponseByChecksum (see FAQ "I did not receive response").
        internal static string CheckSum(Request.Data data)
        {
            if (data.CheckSum != null && data.CheckSum.Length > 32)
            {
                throw new ArgumentException("Checksum can be at most 32 characters long.", nameof(data.CheckSum));
            }
            return data.CheckSum;
        }

        private static void IncreaseRequestCount(string key)
        {
            IncreaseRequestCount(key, DateTime.Now);
        }

        internal static int IncreaseRequestCount(string key, DateTime now)
        {
            lock (_requestCountLock)
            {
                var count = RequestCount(key, now) + 1;
                _requestCount[key] = new KeyValuePair<DateTime, int>(now.Date, count);
                return count;
            }
        }

        private static int RequestCount(string key, DateTime now)
        {
            lock (_requestCountLock)
            {
                return _requestCount.TryGetValue(key, out var day) && day.Key == now.Date ? day.Value : 0;
            }
        }

        internal static Exceptions.Exception HandleRequestException(string uri, HttpResponseMessage response, Exception ex, string data = null)
        {
            string content = null;
            if (response != null)
            {
                var contentTask = response.Content.ReadAsStringAsync();
                contentTask.ConfigureAwait(false);
                contentTask.Wait();
                content = contentTask.Result;
            }

            // HTTP 4xx responses usually carry the regular API error body ({"error": N, ...}).
            if (!string.IsNullOrEmpty(content))
            {
                try
                {
                    var apiError = CreateApiError(JToken.Parse(content), ex, uri);
                    if (apiError != null)
                    {
                        return apiError;
                    }
                }
                catch (JsonException)
                {
                    // Not JSON (e.g. proxy HTML page) - fall back to the generic exception below.
                }
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

        [Obsolete("Not found in API documentation")]
        public async Task<Response<Register>> Register(string email, bool sendEmail = true)
        {
            var result = await Post("users/create", new Request.UserData
            {
                User = new Request.User
                {
                    Email = email,
                    SendEmail = sendEmail
                }
            }).ConfigureAwait(false);
            return DeserializeResult<Response<Register>>(result);
        }

        [Obsolete("Not found in API documentation")]
        public async Task<Response<ExpandoObject>> GetCourierData(string curierType, object data)
        {
            if (new[] { "slp", "csp" }.Contains(curierType))
            {
                var result = await Post($"{curierType}_exports/export", new Request.DataData { Data = data }).ConfigureAwait(false);
                return DeserializeResult<Response<ExpandoObject>>(result);
            }
            return null;
        }

        // Returns the original response of a request sent with this checksum (up to 3 months back),
        // or null when no request with this checksum reached the server.
        public async Task<string> ResponseByChecksum(string checksum)
        {
            var result = await Get($"api_logs/getResponseByChecksum/{Uri.EscapeDataString(checksum)}").ConfigureAwait(false);
            return ParseResponseByChecksum(result);
        }

        internal static string ParseResponseByChecksum(string result)
        {
            TestError(result);
            // Unknown checksum is answered with an empty array, not with an error.
            return JToken.Parse(result) is JArray array && array.Count == 0 ? null : result;
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
