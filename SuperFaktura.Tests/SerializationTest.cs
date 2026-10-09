using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shouldly;
using System.Linq;
using Xunit;

namespace SuperFaktura.Tests
{
    // Offline tests: serialize request models exactly like the client does (JsonConvert.SerializeObject)
    // and check the JSON sent as "data=". No API calls, no credentials needed.
    public class SerializationTest
    {
        private static JObject Serialize(object data)
        {
            return JObject.Parse(JsonConvert.SerializeObject(data));
        }

        [Theory]
        [InlineData(true, "1")]
        [InlineData(false, "0")]
        public void InvoiceVatTransferIsSentAsZeroOrOne(bool vatTransfer, string expected)
        {
            var json = Serialize(new Birko.SuperFaktura.Request.Invoice.Invoice { VATTransfer = vatTransfer });

            json["vat_transfer"].Value<string>().ShouldBe(expected);
        }

        [Fact]
        public void InvoicePaymentWithoutDocumentNumberDoesNotSendIt()
        {
            var json = Serialize(new Birko.SuperFaktura.Request.Invoice.Payment { InvoiceID = 1 });

            json.ContainsKey("document_no").ShouldBeFalse();
        }

        [Fact]
        public void InvoicePaymentSendsDocumentNumberWhenSet()
        {
            var json = Serialize(new Birko.SuperFaktura.Request.Invoice.Payment { InvoiceID = 1, DocumentNumber = "PD2026/001" });

            json["document_no"].Value<string>().ShouldBe("PD2026/001");
        }

        [Fact]
        public void ErrorResponseWithStringMessageThrowsApiException()
        {
            var ex = Should.Throw<Birko.SuperFaktura.Exceptions.Exception>(() =>
                Birko.SuperFaktura.AbstractSuperFaktura.TestError("{\"error\":3,\"error_message\":\"Invoice not found\",\"message\":\"Invoice not found\"}"));

            ex.ShouldNotBeOfType<Birko.SuperFaktura.Exceptions.ParseException>();
            ex.Error.ShouldBe(3);
            ex.ErrorMessage.ShouldBe("Invoice not found");
            ex.ResponseMessage.ShouldBe("Invoice not found");
        }

        [Fact]
        public void ErrorResponseWithObjectErrorMessageThrowsApiException()
        {
            var ex = Should.Throw<Birko.SuperFaktura.Exceptions.Exception>(() =>
                Birko.SuperFaktura.AbstractSuperFaktura.TestError("{\"error\":4,\"error_message\":{\"data_bad_format\":\"Missing required client data.\"}}"));

            ex.ShouldNotBeOfType<Birko.SuperFaktura.Exceptions.ParseException>();
            ex.Error.ShouldBe(4);
            ex.ErrorMessage.ShouldContain("Missing required client data.");
        }

        [Theory]
        [InlineData("{\"error\":0,\"message\":\"ok\"}")]
        [InlineData("{\"Invoice\":{\"id\":1}}")]
        [InlineData("[{\"id\":1}]")]
        [InlineData("[]")]
        public void SuccessResponseDoesNotThrow(string response)
        {
            Should.NotThrow(() => Birko.SuperFaktura.AbstractSuperFaktura.TestError(response));
        }

        [Fact]
        public void HttpErrorWithApiErrorBodyKeepsErrorCode()
        {
            var response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
            {
                Content = new System.Net.Http.StringContent("{\"error\":3,\"error_message\":\"Invoice not found\",\"message\":\"Invoice not found\"}")
            };
            var inner = new System.Net.Http.HttpRequestException("404 (Not Found)");

            var ex = Birko.SuperFaktura.AbstractSuperFaktura.HandleRequestException("invoices/view/1.json", response, inner);

            ex.Error.ShouldBe(3);
            ex.ErrorMessage.ShouldBe("Invoice not found");
            ex.ResponseMessage.ShouldBe("Invoice not found");
            ex.URL.ShouldBe("invoices/view/1.json");
            ex.InnerException.ShouldBe(inner);
        }

        [Fact]
        public void HttpErrorWithNonJsonBodyKeepsGenericException()
        {
            var response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.BadGateway)
            {
                Content = new System.Net.Http.StringContent("<html>Bad Gateway</html>")
            };
            var inner = new System.Net.Http.HttpRequestException("502 (Bad Gateway)");

            var ex = Birko.SuperFaktura.AbstractSuperFaktura.HandleRequestException("invoices/view/1.json", response, inner);

            ex.Error.ShouldBeNull();
            ex.ResponseMessage.ShouldContain("<html>Bad Gateway</html>");
            ex.InnerException.ShouldBe(inner);
        }

        [Fact]
        public void NetworkErrorWithoutResponseKeepsGenericException()
        {
            var inner = new System.Net.Http.HttpRequestException("No such host is known.");

            var ex = Birko.SuperFaktura.AbstractSuperFaktura.HandleRequestException("invoices/view/1.json", null, inner);

            ex.Error.ShouldBeNull();
            ex.InnerException.ShouldBe(inner);
        }

        [Fact]
        public void CheckSumIsSentAsGivenByCaller()
        {
            var data = new Birko.SuperFaktura.Request.Invoice.InvoiceData { CheckSum = "ORDER-2026-0042" };

            Birko.SuperFaktura.AbstractSuperFaktura.CheckSum(data).ShouldBe("ORDER-2026-0042");
            var json = Serialize(data);
            json["checksum"].Value<string>().ShouldBe("ORDER-2026-0042");
            json.ContainsKey("date").ShouldBeFalse();
        }

        [Fact]
        public void NoCheckSumAndNoDateAreSentWhenCallerDoesNotSetIt()
        {
            var data = new Birko.SuperFaktura.Request.Invoice.InvoiceData();

            Birko.SuperFaktura.AbstractSuperFaktura.CheckSum(data).ShouldBeNull();
            var json = Serialize(data);
            json.ContainsKey("checksum").ShouldBeFalse();
            json.ContainsKey("date").ShouldBeFalse();
        }

        [Fact]
        public async System.Threading.Tasks.Task InvoiceAddRejectsCheckSumLongerThan32CharsBeforeSending()
        {
            // Dummy credentials: the call must fail on validation, before any HTTP request.
            var client = new Birko.SuperFaktura.SuperFakturaSandbox("offline@example.com", "offline");

            await Should.ThrowAsync<System.ArgumentException>(() => client.Invoices.Add(
                new Birko.SuperFaktura.Request.Invoice.Invoice(),
                new Birko.SuperFaktura.Request.Client.Client(),
                new Birko.SuperFaktura.Request.Invoice.Item[0],
                checksum: new string('x', 33)));
        }

        [Fact]
        public async System.Threading.Tasks.Task ExpenseAddPassesCheckSumToRequest()
        {
            // The 33-char checksum is only rejected if Add actually puts it into the request data.
            var client = new Birko.SuperFaktura.SuperFakturaSandbox("offline@example.com", "offline");

            await Should.ThrowAsync<System.ArgumentException>(() => client.Expenses.Add(
                new Birko.SuperFaktura.Request.Expense.Expense(),
                checksum: new string('x', 33)));
        }

        [Fact]
        public async System.Threading.Tasks.Task CashRegisterAddItemPassesCheckSumToRequest()
        {
            var client = new Birko.SuperFaktura.SuperFakturaSandbox("offline@example.com", "offline");

            await Should.ThrowAsync<System.ArgumentException>(() => client.CashRegisters.AddItem(
                new Birko.SuperFaktura.Request.CashRegister.CashRegisterItem(),
                checksum: new string('x', 33)));
        }

        [Fact]
        public void ResponseByChecksumReturnsNullWhenNothingWasStored()
        {
            Birko.SuperFaktura.SuperFaktura.ParseResponseByChecksum("[]").ShouldBeNull();
        }

        [Fact]
        public void ResponseByChecksumReturnsOriginalResponse()
        {
            var original = "{\"error\":0,\"error_message\":\"Invoice created\",\"data\":{\"Invoice\":{\"id\":\"1\"}}}";

            Birko.SuperFaktura.SuperFaktura.ParseResponseByChecksum(original).ShouldBe(original);
        }

        [Fact]
        public void ResponseByChecksumThrowsWhenOriginalResponseWasError()
        {
            Should.Throw<Birko.SuperFaktura.Exceptions.Exception>(() =>
                Birko.SuperFaktura.SuperFaktura.ParseResponseByChecksum("{\"error\":2,\"error_message\":\"Chýbajúce údaje\"}"))
                .Error.ShouldBe(2);
        }

        // Exposes the protected CreateClient so the cached HttpClient can be inspected offline.
        private class InspectableClient : Birko.SuperFaktura.SuperFakturaSandbox
        {
            public InspectableClient(string email, string apiKey, string module = "API", int? companyId = null)
                : base(email, apiKey, module: module, companyId: companyId) { }

            public string AuthorizationHeader()
            {
                return string.Join("", CreateClient().DefaultRequestHeaders.GetValues("Authorization"));
            }
        }

        [Fact]
        public void AuthorizationHeaderValuesAreUrlEncoded()
        {
            // Example from intro.md: hello+world@example.com -> hello%2Bworld%40example.com
            var header = new InspectableClient("hello+world@example.com", "key+1/2=", "WordPress 5.2.3 (WC 3.8.0, WC SF 1.9.17)", 7).AuthorizationHeader();

            header.ShouldBe("SFAPI email=hello%2Bworld%40example.com&apikey=key%2B1%2F2%3D&company_id=7&module=WordPress%205.2.3%20%28WC%203.8.0%2C%20WC%20SF%201.9.17%29");
        }

        [Fact]
        public void AuthorizationHeaderWithoutCompanyIdSendsEmptyCompanyId()
        {
            var header = new InspectableClient("offline-nocompany@example.com", "offline").AuthorizationHeader();

            header.ShouldBe("SFAPI email=offline-nocompany%40example.com&apikey=offline&company_id=&module=API");
        }

        [Fact]
        public void ClientsWithSameApiKeyButDifferentCompanyUseTheirOwnHeader()
        {
            var first = new InspectableClient("offline-company@example.com", "shared-key", companyId: 1);
            var second = new InspectableClient("offline-company@example.com", "shared-key", companyId: 2);

            first.AuthorizationHeader().ShouldContain("company_id=1&");
            second.AuthorizationHeader().ShouldContain("company_id=2&");
        }

        [Fact]
        public void ClientsWithSameApiKeyButDifferentEmailOrModuleUseTheirOwnHeader()
        {
            var first = new InspectableClient("offline-a@example.com", "shared-key-2", "ModuleA");
            var second = new InspectableClient("offline-b@example.com", "shared-key-2", "ModuleB");

            first.AuthorizationHeader().ShouldContain("email=offline-a%40example.com&");
            first.AuthorizationHeader().ShouldEndWith("module=ModuleA");
            second.AuthorizationHeader().ShouldContain("email=offline-b%40example.com&");
            second.AuthorizationHeader().ShouldEndWith("module=ModuleB");
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteWithBodySendsFormEncodedDataToRelativeUrl()
        {
            // expenses.md "Delete expense items": curl -X DELETE -d "data=$data" .../expense_items/delete
            var request = Birko.SuperFaktura.AbstractSuperFaktura.CreateDeleteRequest("expense_items/delete", "{\"expense_id\":100,\"delete_ids\":[200,201]}");

            request.Method.ShouldBe(System.Net.Http.HttpMethod.Delete);
            request.RequestUri.IsAbsoluteUri.ShouldBeFalse();
            request.RequestUri.OriginalString.ShouldBe("expense_items/delete");
            request.Content.Headers.ContentType.MediaType.ShouldBe("application/x-www-form-urlencoded");
            var body = await request.Content.ReadAsStringAsync();
            System.Net.WebUtility.UrlDecode(body).ShouldBe("data={\"expense_id\":100,\"delete_ids\":[200,201]}");
        }

        [Theory]
        // other.md activity logs + invoice.md/expenses.md related items: "type of document (invoice,expense)"
        [InlineData(Birko.SuperFaktura.Request.ValueLists.DocumentType.Invoice, "invoice")]
        [InlineData(Birko.SuperFaktura.Request.ValueLists.DocumentType.Expense, "expense")]
        public void DocumentTypesMatchDocumentation(string constant, string documented)
        {
            constant.ShouldBe(documented);
        }

        [Fact]
        public void BankAccountSendsOnlyFieldsThatWereSet()
        {
            // bank-account.md: default/show are "null (will not be set)" unless given.
            var json = Serialize(new Birko.SuperFaktura.Request.BankAccounts.BankAccount { BankName = "NovaBanka" });

            json.Properties().Select(p => p.Name).ShouldBe(new[] { "bank_name" });
        }

        [Fact]
        public void BankAccountSendsExplicitFlagsAsZeroOrOne()
        {
            var json = Serialize(new Birko.SuperFaktura.Request.BankAccounts.BankAccount { Default = false, Show = true, ShowAccount = true });

            json["default"].Value<string>().ShouldBe("0");
            json["show"].Value<string>().ShouldBe("1");
            json["show_account"].Value<string>().ShouldBe("1");
        }

        [Fact]
        public void BankAccountResponseFromDocumentationDeserializes()
        {
            // bank-account.md "Get list of bank accounts", plus a null flag.
            var account = JsonConvert.DeserializeObject<Birko.SuperFaktura.Response.BankAccounts.BankAccount>(
                "{\"account\":\"\",\"bank_code\":\"\",\"bank_name\":\"FatraBanka\",\"country_id\":\"191\",\"created\":\"2050-01-01 23:59:59\"," +
                "\"currency\":null,\"default\":true,\"iban\":\"SK012345678901234567890000\",\"id\":\"1\",\"modified\":\"2050-01-01 23:59:59\"," +
                "\"show\":true,\"show_account\":null,\"swift\":\"SUZUKI\",\"user_id\":\"1\",\"user_profile_id\":\"1\"}");

            account.Default.ShouldBe(true);
            account.Show.ShouldBe(true);
            account.ShowAccount.ShouldBeNull();
            account.CountryID.ShouldBe(191);
            account.Currency.ShouldBeNull();
        }

        [Fact]
        public void BankMovementFilterSendsDatesAsIsoDateWithDate3()
        {
            // other.md: accounts/index.json/date:3/date_since:2050-01-27/date_to:2050-01-27
            var parameters = new Birko.SuperFaktura.Request.Other.BankMovementFilter
            {
                DateSince = new System.DateTime(2050, 1, 27),
                DateTo = new System.DateTime(2050, 1, 27, 15, 30, 0),
            }.ToParameters();

            parameters.ShouldContain("/date:3/");
            parameters.ShouldContain("/date_since:2050-01-27");
            parameters.ShouldContain("/date_to:2050-01-27");
        }

        [Fact]
        public void BankMovementFilterKeepsExplicitDateFilter()
        {
            var parameters = new Birko.SuperFaktura.Request.Other.BankMovementFilter { Date = Birko.SuperFaktura.Request.ValueLists.TimeFilterConstants.ThisMonth }.ToParameters();

            parameters.ShouldContain($"/date:{Birko.SuperFaktura.Request.ValueLists.TimeFilterConstants.ThisMonth}");
            parameters.ShouldNotContain("date_since");
        }

        [Fact]
        public void BankMovementFilterSendsAmountsWithDotRegardlessOfCulture()
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("sk-SK");
                var parameters = new Birko.SuperFaktura.Request.Other.BankMovementFilter { AmountFrom = 10.5m, AmountTo = -2.25m }.ToParameters();

                parameters.ShouldContain("/amount_from:10.5");
                parameters.ShouldContain("/amount_to:-2.25");
            }
            finally
            {
                System.Globalization.CultureInfo.CurrentCulture = culture;
            }
        }

        [Theory]
        // intro.md "Limit headers": X-RateLimit-DailyReset: 24.12.2030 00:00:00
        [InlineData("24.12.2030 00:00:00", 2030, 12, 24, 0, 0)]
        [InlineData("01.12.2030 15:30:00", 2030, 12, 1, 15, 30)]
        public void RateLimitResetHeaderIsParsedAs24HourTime(string header, int year, int month, int day, int hour, int minute)
        {
            Birko.SuperFaktura.AbstractSuperFaktura.ParseRateLimitReset(header).ShouldBe(new System.DateTime(year, month, day, hour, minute, 0));
        }

        [Fact]
        public void RequestCountStartsOverOnNewDay()
        {
            var key = "offline-count-" + System.Guid.NewGuid();
            var day = new System.DateTime(2026, 10, 6);

            Birko.SuperFaktura.AbstractSuperFaktura.IncreaseRequestCount(key, day.AddHours(9));
            Birko.SuperFaktura.AbstractSuperFaktura.IncreaseRequestCount(key, day.AddHours(10));
            Birko.SuperFaktura.AbstractSuperFaktura.IncreaseRequestCount(key, day.AddHours(23)).ShouldBe(3);

            Birko.SuperFaktura.AbstractSuperFaktura.IncreaseRequestCount(key, day.AddDays(1)).ShouldBe(1);
        }

        [Fact]
        public void ClientSendsOnlyFieldsThatWereSet()
        {
            // match_address, update and data_source keep their defaults on purpose (they change server-side matching).
            var json = Serialize(new Birko.SuperFaktura.Request.Client.Client { Name = "Acme" });

            json.Properties().Select(p => p.Name).OrderBy(n => n).ShouldBe(new[] { "data_source", "match_address", "name", "update" });
            json["match_address"].Value<string>().ShouldBe("1");
        }

        [Fact]
        public void ClientFilterSendsDateRangeOnceWithCreated3()
        {
            // clients.md: created_since/created_to require created:3
            var parameters = new Birko.SuperFaktura.Request.Client.Filter { CreatedSince = new System.DateTime(2026, 1, 2), CreatedTo = new System.DateTime(2026, 2, 3) }.ToParameters();

            parameters.ShouldContain("/created:3/");
            System.Text.RegularExpressions.Regex.Matches(parameters, "created_since:").Count.ShouldBe(1);
            parameters.ShouldContain("/created_since:2026-01-02");
            parameters.ShouldContain("/created_to:2026-02-03");
        }

        [Fact]
        public void ClientFilterSendsCharFilter()
        {
            new Birko.SuperFaktura.Request.Client.Filter { CharFilter = "A" }.ToParameters().ShouldContain("/char_filter:A");
        }

        [Fact]
        public void CountryResponseReadsOrder()
        {
            JsonConvert.DeserializeObject<Birko.SuperFaktura.Response.Country>("{\"id\":\"191\",\"name\":\"Slovensko\",\"iso\":\"sk\",\"eu\":\"1\",\"order\":\"2\"}")
                .Order.ShouldBe(2);
        }

        [Fact]
        public void StockItemSendsOnlyFieldsThatWereSet()
        {
            var json = Serialize(new Birko.SuperFaktura.Request.Stock.Item { Name = "Maslo" });

            json.Properties().Select(p => p.Name).ShouldBe(new[] { "name" });
        }

        [Fact]
        public void StockItemSendsFlagsAsZeroOrOneAndFractionalStock()
        {
            // stock.md: stock is float, hide_in_autocomplete / watch_stock are int 0|1
            var json = Serialize(new Birko.SuperFaktura.Request.Stock.Item { Stock = 12.5m, HideInAutoComplete = true, WatchStock = false });

            json["stock"].Value<decimal>().ShouldBe(12.5m);
            json["hide_in_autocomplete"].Value<string>().ShouldBe("1");
            json["watch_stock"].Value<string>().ShouldBe("0");
        }

        [Fact]
        public void StockMovementByItemIdSendsDateOnlyAndNoZeroPrices()
        {
            // stock.md "Add stock movement": created is YYYY-MM-DD; sku or stock_item_id identifies the item
            var json = Serialize(new Birko.SuperFaktura.Request.Stock.Log { StockItemID = 5, Quantity = -2, Created = new System.DateTime(2026, 10, 5, 15, 30, 0) });

            json.Properties().Select(p => p.Name).OrderBy(n => n).ShouldBe(new[] { "created", "quantity", "stock_item_id" });
            json["created"].Value<string>().ShouldBe("2026-10-05");
        }

        [Fact]
        public void StockMovementsResponseKeepsDocumentFields()
        {
            // Shape returned by sandbox for stock_items/movements/{id}
            var response = JsonConvert.DeserializeObject<Birko.SuperFaktura.Response.PagedResponse<Birko.SuperFaktura.Response.Stock.LogItem>>(
                "{\"itemCount\":1,\"pageCount\":1,\"perPage\":50,\"page\":1,\"items\":[{\"StockLog\":{\"id\":\"23111\",\"stock_item_id\":\"16285\"," +
                "\"user_id\":\"486\",\"user_profile_id\":\"1731\",\"invoice_id\":\"77\",\"expense_id\":null,\"document_item_id\":\"88\",\"quantity\":-2," +
                "\"note\":\"probe move\",\"document\":\"invoice\",\"document_subtype\":\"regular\",\"log_data\":\"{}\",\"created\":\"2026-10-05 00:00:00\",\"modified\":\"2026-10-06 13:01:02\"}}]}");

            var log = response.Items.Single().StockLog;
            log.StockItemID.ShouldBe(16285);
            log.InvoiceID.ShouldBe(77);
            log.ExpenseID.ShouldBeNull();
            log.DocumentItemID.ShouldBe(88);
            log.Document.ShouldBe("invoice");
            log.DocumentSubtype.ShouldBe("regular");
            log.UserID.ShouldBe(486);
            log.Modified.ShouldBe(new System.DateTime(2026, 10, 6, 13, 1, 2));
        }

        [Fact]
        public void StockItemEditResponseReadsPreviousStock()
        {
            JsonConvert.DeserializeObject<Birko.SuperFaktura.Response.Stock.Item>("{\"id\":1,\"stock\":10.5,\"stock_previous\":12.5}")
                .StockPrevious.ShouldBe(12.5m);
        }

        [Fact]
        public void StockFilterSendsPricesWithDotRegardlessOfCulture()
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("sk-SK");
                var parameters = new Birko.SuperFaktura.Request.Stock.Filter { PriceFrom = 10.5m, PriceTo = 20.25m }.ToParameters();

                parameters.ShouldContain("/price_from:10.5");
                parameters.ShouldContain("/price_to:20.25");
            }
            finally
            {
                System.Globalization.CultureInfo.CurrentCulture = culture;
            }
        }

        [Fact]
        public void ExpenseSendsOnlyFieldsThatWereSet()
        {
            var json = Serialize(new Birko.SuperFaktura.Request.Expense.Expense { Name = "Foo bar" });

            json.Properties().Select(p => p.Name).ShouldBe(new[] { "name" });
        }

        [Fact]
        public void ExpenseForEditSendsIdAndDocumentedFormats()
        {
            // expenses.md: id required for edit; dates YYYY-MM-DD; document_number is a string; already_paid int 0|1
            var json = Serialize(new Birko.SuperFaktura.Request.Expense.Expense
            {
                ID = 30,
                ExpenseCategoryID = 4,
                DocumentNumber = "FA2026/0042",
                Created = new System.DateTime(2026, 9, 1, 15, 30, 0),
                Due = new System.DateTime(2026, 9, 30),
                TaxableSupply = new System.DateTime(2026, 9, 3),
                AlreadyPaid = true,
                Amount = 12.14m,
            });

            json["id"].Value<int>().ShouldBe(30);
            json["expense_category_id"].Value<int>().ShouldBe(4);
            json["document_number"].Value<string>().ShouldBe("FA2026/0042");
            json["created"].Value<string>().ShouldBe("2026-09-01");
            json["due"].Value<string>().ShouldBe("2026-09-30");
            json["taxable_supply"].Value<string>().ShouldBe("2026-09-03");
            json["already_paid"].Value<string>().ShouldBe("1");
            json.ContainsKey("delivery").ShouldBeFalse();
            json.ContainsKey("version").ShouldBeFalse();
            json.ContainsKey("type").ShouldBeFalse();
        }

        [Fact]
        public void ExpenseExtraVatTransferCanBeSet()
        {
            Serialize(new Birko.SuperFaktura.Request.Expense.Extra { VATTransfer = 1 })["vat_transfer"].Value<int>().ShouldBe(1);
        }

        [Theory]
        // expenses.md: array for a single 0 % rate, object keyed by VAT rate for "items" version (shape confirmed on sandbox)
        [InlineData("[{\"base\":12.14,\"vat\":0}]", "0:12.14:0")]
        [InlineData("{\"20\":{\"base\":20,\"vat\":4},\"10\":{\"base\":5,\"vat\":0.5}}", "20:20:4|10:5:0.5")]
        public void ExpenseVatSummaryReadsArrayAndObjectShape(string vatSummary, string expected)
        {
            var detail = JsonConvert.DeserializeObject<Birko.SuperFaktura.Response.Expense.Detail>("{\"VatSummary\":" + vatSummary + "}");

            string.Join("|", detail.VATSummary.Select(s => $"{s.Rate.ToString(System.Globalization.CultureInfo.InvariantCulture)}:{s.Base.ToString(System.Globalization.CultureInfo.InvariantCulture)}:{s.VAT.ToString(System.Globalization.CultureInfo.InvariantCulture)}"))
                .ShouldBe(expected);
        }

        [Fact]
        public void ExpenseListResponseReadsTatrabankaFlagAndMyDataId()
        {
            var detail = JsonConvert.DeserializeObject<Birko.SuperFaktura.Response.Expense.Detail>(
                "{\"Expense\":{\"id\":\"1\",\"is_payable_by_tatrabanka\":true},\"MyData\":{\"id\":\"1731\",\"name\":\"Firma\"}}");

            detail.Expense.IsPayableByTatrabanka.ShouldBe(true);
            detail.MyData.ID.ShouldBe(1731);
        }

        [Fact]
        public void ExpenseFilterSendsPagingModifiedRangeAndInvariantAmounts()
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("sk-SK");
                var parameters = new Birko.SuperFaktura.Request.Expense.Filter
                {
                    Page = 2, PerPage = 25, AmountFrom = 10.5m,
                    ModifiedSince = new System.DateTime(2026, 1, 2), ModifiedTo = new System.DateTime(2026, 2, 3),
                    CreatedSince = new System.DateTime(2026, 3, 4),
                }.ToParameters();

                parameters.ShouldContain("/page:2");
                parameters.ShouldContain("/per_page:25");
                parameters.ShouldContain("/amount_from:10.5");
                parameters.ShouldContain("/modified:3/");
                parameters.ShouldContain("/modified_since:2026-01-02");
                parameters.ShouldContain("/modified_to:2026-02-03");
                parameters.ShouldContain("/created:3/");
            }
            finally
            {
                System.Globalization.CultureInfo.CurrentCulture = culture;
            }
        }

        [Fact]
        public void InvoiceSendsOnlyFieldsThatWereSet()
        {
            var json = Serialize(new Birko.SuperFaktura.Request.Invoice.Invoice { Name = "Test API" });

            json.Properties().Select(p => p.Name).ShouldBe(new[] { "name" });
        }

        [Fact]
        public void InvoiceForEditSendsIdAndDocumentedFormats()
        {
            // invoice.md "Edit invoice": {"Invoice": {"id": 2, "discount": 5}}; dates YYYY-MM-DD; proforma_id is a comma separated string
            var json = Serialize(new Birko.SuperFaktura.Request.Invoice.Invoice
            {
                ID = 2,
                Discount = 5,
                Created = new System.DateTime(2026, 9, 1, 15, 30, 0),
                DueDate = new System.DateTime(2026, 9, 30),
                ProformaID = "1,2,3",
            });

            json.Properties().Select(p => p.Name).OrderBy(n => n).ShouldBe(new[] { "created", "discount", "due", "id", "proforma_id" });
            json["created"].Value<string>().ShouldBe("2026-09-01");
            json["due"].Value<string>().ShouldBe("2026-09-30");
            json["proforma_id"].Value<string>().ShouldBe("1,2,3");
        }

        [Fact]
        public void InvoiceItemSendsOnlyFieldsThatWereSet()
        {
            // invoice.md: load_data_from_stock default 0, quantity default 1, unit_price default 0 - left to the server
            var json = Serialize(new Birko.SuperFaktura.Request.Invoice.Item { Name = "item 1" });

            json.Properties().Select(p => p.Name).ShouldBe(new[] { "name" });
            Serialize(new Birko.SuperFaktura.Request.Invoice.Item { LoadDataFromStock = true })["load_data_from_stock"].Value<string>().ShouldBe("1");
        }

        [Fact]
        public void InvoiceSettingsSendOnlyFieldsThatWereSet()
        {
            Serialize(new Birko.SuperFaktura.Request.Invoice.InvoiceSettings()).Properties().ShouldBeEmpty();
            Serialize(new Birko.SuperFaktura.Request.Invoice.InvoiceSettings { BySquare = false })["bysquare"].Value<string>().ShouldBe("0");
        }

        [Fact]
        public void InvoiceMyDataSendsOnlyFieldsThatWereSet()
        {
            // invoice.md MyData has no tax_payer / user_id; country_id only when set
            var json = Serialize(new Birko.SuperFaktura.Request.Invoice.MyData { CompanyName = "Firma s.r.o." });

            json.Properties().Select(p => p.Name).ShouldBe(new[] { "company_name" });
            var withCountry = new Birko.SuperFaktura.Request.Invoice.MyData { CountryID = 191 };
            Serialize(withCountry)["country_id"].Value<int>().ShouldBe(191);
            ((Birko.SuperFaktura.Response.UserProfile)withCountry).CountryID.ShouldBe(191);
        }

        [Fact]
        public void InvoiceClientSendsIsoCountriesAndUpdateAddressbook()
        {
            // invoice.md Client: country_iso_id, delivery_country_iso_id (ISO-3166-1 alpha-2), update_addressbook int 0|1
            var json = Serialize(new Birko.SuperFaktura.Request.Client.Client { Name = "Acme", CountryISOID = "CZ", DeliveryCountryISOID = "AT", UpdateAddressBook = true });

            json["country_iso_id"].Value<string>().ShouldBe("CZ");
            json["delivery_country_iso_id"].Value<string>().ShouldBe("AT");
            json["update_addressbook"].Value<string>().ShouldBe("1");
        }

        [Fact]
        public void InvoiceDetailReadsFieldsReturnedBySandbox()
        {
            // Shapes returned by sandbox for invoices/view and invoices/index.json
            var detail = JsonConvert.DeserializeObject<Birko.SuperFaktura.Response.Invoice.Detail>(
                "{\"0\":{\"to_pay\":12,\"sent_by\":\"manual\",\"sent_to_email\":\"a@example.com\",\"sent_to_email_cc\":\"\"}," +
                "\"InvoiceSetting\":{\"language\":\"eng\",\"force_iban\":true}," +
                "\"Client\":{\"name\":\"Acme\",\"country_iso_id\":\"CZ\",\"DeliveryCountry\":{\"id\":\"14\",\"name\":\"Rakúsko\",\"iso\":\"at\"}}," +
                "\"ClientData\":{\"name\":\"Acme\",\"update\":\"false\",\"match_address\":true,\"data_source\":\"empty\",\"country_iso_id\":\"CZ\",\"delivery_country_iso_id\":\"AT\",\"update_addressbook\":1}," +
                "\"InvoiceEmail\":[{\"id\":\"78671\",\"email\":\"\",\"phone\":\"+421900111222\",\"email_bcc\":\"b@example.com\",\"due\":null,\"created\":\"2026-09-18 12:58:25\"}]," +
                "\"Signature\":{\"id\":\"1\",\"delete_flag\":null}}");

            detail.PayInfo.SentBy.ShouldBe("manual");
            detail.PayInfo.SentToEmail.ShouldBe("a@example.com");
            detail.InvoiceSetting.ForceIBAN.ShouldBe(true);
            detail.Client.CountryISOID.ShouldBe("CZ");
            detail.Client.DeliveryCountryDetail.ID.ShouldBe(14);
            detail.ClientData.Update.ShouldBe(false);
            detail.ClientData.MatchAddress.ShouldBe(true);
            detail.ClientData.DataSource.ShouldBe("empty");
            detail.ClientData.CountryISOID.ShouldBe("CZ");
            detail.ClientData.DeliveryCountryISOID.ShouldBe("AT");
            detail.ClientData.UpdateAddressBook.ShouldBe(true);
            detail.InvoiceEmail.Single().Phone.ShouldBe("+421900111222");
            detail.InvoiceEmail.Single().EmailBCC.ShouldBe("b@example.com");
            detail.InvoiceEmail.Single().Due.ShouldBeNull();
            detail.SignatureRaw.DeleteFlag.ShouldBeNull();
        }

        [Fact]
        public void PagedResponseReadsFiltered()
        {
            JsonConvert.DeserializeObject<Birko.SuperFaktura.Response.Invoice.PagedResponse>("{\"itemCount\":0,\"items\":[],\"filtered\":true}")
                .Filtered.ShouldBe(true);
        }

        [Fact]
        public void CashRegisterItemSendsCreatedAsDate()
        {
            // cash-register-item.md: created is a date; sandbox stores only the date part anyway
            var json = Serialize(new Birko.SuperFaktura.Request.CashRegister.CashRegisterItem { CashRegisterID = 46, Amount = 12.5m, Created = new System.DateTime(2026, 10, 5, 15, 30, 0) });

            json["created"].Value<string>().ShouldBe("2026-10-05");
        }

        [Fact]
        public void CashRegisterItemsUrlFollowsDocumentation()
        {
            // cash-register-item.md: cash_register_items/index/{ID}/term:P2020003 ; dates YYYY-MM-DD
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("sk-SK");
                var url = Birko.SuperFaktura.CashRegisters.ItemsUrl(new Birko.SuperFaktura.Request.CashRegister.Filter
                {
                    ID = 46, DateFrom = new System.DateTime(2026, 10, 5), DateTo = new System.DateTime(2026, 10, 6), SumFrom = 10.5m, Type = "out",
                });

                url.ShouldStartWith("cash_register_items/index/46/");
                url.ShouldNotContain("//");
                url.ShouldContain("/date_from:2026-10-05");
                url.ShouldContain("/date_to:2026-10-06");
                url.ShouldContain("/sum_from:10.5");
                url.ShouldContain("/type:out");
            }
            finally
            {
                System.Globalization.CultureInfo.CurrentCulture = culture;
            }
        }

        [Fact]
        public void CashRegisterDeleteResponseReadsStatusAndRawSummary()
        {
            // Shape returned by sandbox for cash_register_items/delete
            var response = JsonConvert.DeserializeObject<Birko.SuperFaktura.Response.CashRegister.CashRegisterSummaryResponse>(
                "{\"status\":1,\"Summary\":{\"total\":{\"raw\":\"-2.00\",\"formatted\":\"-2,00 €\"},\"minus\":{\"raw\":null,\"formatted\":\"0,00 €\"},\"plus\":{\"raw\":\"1.00\",\"formatted\":\"1,00 €\"}}}");

            response.Status.ShouldBe(1);
            response.Summary.Total.Raw.ShouldBe(-2m);
            response.Summary.Minus.Raw.ShouldBeNull();
            response.Summary.Plus.Raw.ShouldBe(1m);
        }

        [Fact]
        public void CashRegisterItemsListReadsRegisterAndStorno()
        {
            // Shape returned by sandbox for cash_register_items/index/{ID}
            var list = JsonConvert.DeserializeObject<Birko.SuperFaktura.Response.CashRegister.ItemList>(
                "{\"error\":\"\",\"items\":[{\"CashRegisterItem\":{\"id\":\"120\",\"amount\":\"0.00\"},\"0\":{\"has_storno\":\"1\"}}],\"itemCount\":1,\"pageCount\":1,\"perPage\":10,\"page\":1," +
                "\"CashRegister\":{\"id\":\"46\",\"name\":\"test pokladnica\",\"default\":false,\"total\":\"0.00\",\"items\":\"1\",\"sequence_in_no\":\"P2026001\",\"sequence_out_no\":\"V2026001\"}}");

            list.ItemCount.ShouldBe(1);
            list.Items.Single().Flags.HasStorno.ShouldBe(true);
            list.CashRegister.ID.ShouldBe(46);
            list.CashRegister.Default.ShouldBe(false);
            list.CashRegister.Items.ShouldBe(1);
            list.CashRegister.SequenceInNumber.ShouldBe("P2026001");
            list.CashRegister.SequenceOutNumber.ShouldBe("V2026001");
        }

        [Fact]
        public void TagResponseReadsMessage()
        {
            // Shape returned by sandbox for tags/add
            var tag = JsonConvert.DeserializeObject<Birko.SuperFaktura.Response.Tag.Tag>("{\"error\":0,\"message\":\"Tag bol uložený\",\"tag_id\":\"556\",\"tag_name\":\"probe\"}");

            tag.ID.ShouldBe(556);
            tag.Name.ShouldBe("probe");
            tag.Message.ShouldBe("Tag bol uložený");
        }

        [Fact]
        public void ActivityLogReadsCreated()
        {
            var item = JsonConvert.DeserializeObject<Birko.SuperFaktura.Response.Other.ActivityLogItem>("{\"ActivityLog\":{\"id\":\"593705\",\"created\":\"2026-10-06 13:22:15\"}}");

            item.ActivityLog.Created.ShouldBe(new System.DateTime(2026, 10, 6, 13, 22, 15));
        }

        [Fact]
        public void PaymentTypesMatchDocumentation()
        {
            // value-lists.md "Payment types" + postal_order from the official PHP client (superfaktura/apiclient)
            Birko.SuperFaktura.Request.ValueLists.PaymentType.Types.OrderBy(t => t).ShouldBe(new[]
            {
                "accreditation", "barion", "besteron", "card", "cash", "cod", "credit", "debit", "gopay", "inkaso", "other", "paypal", "postal_order", "transfer", "trustpay", "viamo",
            });
        }

        [Fact]
        public void CashRegisterItemTypesMatchDocumentation()
        {
            // cash-register-item.md: type filter "in" for income and "out" for outgo
            Birko.SuperFaktura.Request.ValueLists.CashRegisterItemType.Types.ShouldBe(new[] { "in", "out" });
        }

        [Fact]
        public void SequenceTypesMatchDocumentation()
        {
            // value-lists.md "Sequences": keys returned by sequences/index.json (same 9 keys on sandbox)
            Birko.SuperFaktura.Request.ValueLists.SequenceType.Types.OrderBy(t => t).ShouldBe(new[]
            {
                "cash_register_in", "cash_register_out", "delivery", "estimate", "expense", "order", "proforma", "regular", "reverse_order",
            });
        }

        [Fact]
        public void InvoiceFilterSendsDateRangesWithFilter3AndInvariantAmounts()
        {
            // invoice.md: *_since / *_to require created:3, delivery:3, modified:3, paydate:3 (sandbox ignores the range with created:0)
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("sk-SK");
                var parameters = new Birko.SuperFaktura.Request.Invoice.Filter
                {
                    CreatedSince = new System.DateTime(2026, 9, 18), CreatedTo = new System.DateTime(2026, 9, 19),
                    PayDateSince = new System.DateTime(2026, 10, 1),
                    AmountFrom = 250.5m, AmountTo = 1000.25m,
                }.ToParameters();

                parameters.ShouldContain("/created:3/");
                parameters.ShouldContain("/paydate:3/");
                parameters.ShouldContain("/modified:0/");
                parameters.ShouldContain("/delivery:0/");
                parameters.ShouldContain("/amount_from:250.5");
                parameters.ShouldContain("/amount_to:1000.25");
            }
            finally
            {
                System.Globalization.CultureInfo.CurrentCulture = culture;
            }
        }

        [Fact]
        public void InvoiceFilterKeepsExplicitTimeFilter()
        {
            var parameters = new Birko.SuperFaktura.Request.Invoice.Filter { Created = Birko.SuperFaktura.Request.ValueLists.TimeFilterConstants.ThisMonth }.ToParameters();

            parameters.ShouldContain($"/created:{Birko.SuperFaktura.Request.ValueLists.TimeFilterConstants.ThisMonth}/");
        }

        [Fact]
        public void InvoiceItemFromDetailIsSentBackWithLowercaseId()
        {
            // Sandbox: items sent to invoices/edit with "ID" (upper case) are appended instead of updated
            var item = JsonConvert.DeserializeObject<Birko.SuperFaktura.Response.Invoice.Item>("{\"id\":\"103367380\",\"name\":\"edit item A\"}");

            var json = Serialize(item);
            json["id"].Value<int>().ShouldBe(103367380);
            json.ContainsKey("ID").ShouldBeFalse();
            ((Birko.SuperFaktura.Request.Invoice.Item)item).ID.ShouldBe(103367380);
            Serialize(new Birko.SuperFaktura.Request.Invoice.Item { ID = 5, Name = "x" })["id"].Value<int>().ShouldBe(5);
        }

        [Fact]
        public void ClientAndExpenseDetailReadTagObjects()
        {
            // Sandbox: clients/view returns [{"Tag":{...}}], expenses/view returns [{...}] - not a list of IDs
            var client = JsonConvert.DeserializeObject<Birko.SuperFaktura.Response.Client.DetailClient>(
                "{\"Client\":{\"id\":\"84746\"},\"Tag\":[{\"Tag\":{\"id\":\"460\",\"name\":\"CR_12\",\"client_count\":5}},{\"Tag\":{\"id\":\"528\",\"name\":\"CR_10\"}}]}");
            var expense = JsonConvert.DeserializeObject<Birko.SuperFaktura.Response.Expense.Detail>(
                "{\"Expense\":{\"id\":\"1\"},\"Tag\":[{\"id\":\"564\",\"name\":\"probe\",\"expense_count\":1}]}");

            client.Tag.Select(t => t.ID).ShouldBe(new[] { 460, 528 });
            client.Tag.First().Name.ShouldBe("CR_12");
            expense.Tag.Single().ID.ShouldBe(564);
            expense.Tag.Single().Name.ShouldBe("probe");
        }

        [Fact]
        public void InvoiceEmailSendsOnlyFieldsThatWereSet()
        {
            // invoice.md "Send invoice via mail": body/subject default to the templates, pdf_language optional
            var json = Serialize(new Birko.SuperFaktura.Request.Invoice.Email { InvoiceID = 1, To = "recipient@example.com" });

            json.Properties().Select(p => p.Name).OrderBy(n => n).ShouldBe(new[] { "invoice_id", "to" });
        }

        [Fact]
        public void ExpenseFilterSendsDueAsRange()
        {
            // Sandbox: "due:<date>" (expenses.md) is ignored; due:3/due_since/due_to works (inclusive), as in the PHP client
            var exact = new Birko.SuperFaktura.Request.Expense.Filter { Due = new System.DateTime(2026, 9, 30) }.ToParameters();
            exact.ShouldContain("/due:3/due_since:2026-09-30/due_to:2026-09-30");

            var range = new Birko.SuperFaktura.Request.Expense.Filter { DueSince = new System.DateTime(2026, 9, 1), DueTo = new System.DateTime(2026, 9, 30) }.ToParameters();
            range.ShouldContain("/due:3/due_since:2026-09-01/due_to:2026-09-30");

            new Birko.SuperFaktura.Request.Expense.Filter { DueFilter = Birko.SuperFaktura.Request.ValueLists.TimeFilterConstants.ThisMonth }.ToParameters()
                .ShouldContain($"/due:{Birko.SuperFaktura.Request.ValueLists.TimeFilterConstants.ThisMonth}");
        }

        [Fact]
        public void InvoiceFilterSendsPaidRange()
        {
            // PHP client (superfaktura/apiclient): paid / paid_since / paid_to, verified on sandbox
            var parameters = new Birko.SuperFaktura.Request.Invoice.Filter { PaidSince = new System.DateTime(2026, 9, 1), PaidTo = new System.DateTime(2026, 9, 30) }.ToParameters();

            parameters.ShouldContain("/paid:3/");
            parameters.ShouldContain("/paid_since:2026-09-01");
            parameters.ShouldContain("/paid_to:2026-09-30");
            new Birko.SuperFaktura.Request.Invoice.Filter().ToParameters().ShouldNotContain("paid");
        }

        [Fact]
        public void FiltersSendMultipleValuesSeparatedByPipe()
        {
            // invoice.md list: type, ignore, payment_type "Use | as separator for multiple values"
            // (delivery_type too per the docs, but the sandbox accepts a single value only)
            var parameters = new Birko.SuperFaktura.Request.Invoice.Filter
            {
                Type = new[] { "regular", "proforma" },
                Ignore = new[] { 1, 2 },
                PaymentType = new[] { "transfer", "cash" },
            }.ToParameters();

            parameters.ShouldContain("/type:regular|proforma");
            parameters.ShouldContain("/ignore:1|2");
            parameters.ShouldContain("/payment_type:transfer|cash");
            var empty = new Birko.SuperFaktura.Request.Invoice.Filter { Type = new string[0], Ignore = new int[0] }.ToParameters();
            empty.ShouldNotContain("/type:");
            empty.ShouldNotContain("/ignore:");

            // expenses.md list: status "use pipe (|) to add various statuses (e.g. 1|2)"
            new Birko.SuperFaktura.Request.Expense.Filter { Status = new[] { 1, 2 } }.ToParameters().ShouldContain("/status:1|2");
        }

        [Fact]
        public void InvalidJsonResponseThrowsParseException()
        {
            Should.Throw<Birko.SuperFaktura.Exceptions.ParseException>(() =>
                Birko.SuperFaktura.AbstractSuperFaktura.TestError("<html>Bad Gateway</html>"));
        }
    }
}
