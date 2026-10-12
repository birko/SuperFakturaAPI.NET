using Newtonsoft.Json.Linq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SuperFaktura.Tests
{
    // Read-only: lists the account's existing data and checks every returned value fits its model type
    // (ModelCoverage.Mismatched), to catch undocumented fields like InvoiceEmail "due":"-7" before they
    // break a sync. Unmapped keys are only printed.
    // Production data has shapes the sandbox lacks; to probe it, set SuperFakturaProbeEmail,
    // SuperFakturaProbeApiKey and SuperFakturaProbeCompanyId - only this test then reads production.
    public class ResponseTypesTest : SuperFakturaTest
    {
        private static readonly Birko.SuperFaktura.SuperFaktura client = ProbeClient() ?? apiClient;

        private static Birko.SuperFaktura.SuperFaktura ProbeClient()
        {
            var email = Environment.GetEnvironmentVariable("SuperFakturaProbeEmail");
            var apiKey = Environment.GetEnvironmentVariable("SuperFakturaProbeApiKey");
            var companyId = Environment.GetEnvironmentVariable("SuperFakturaProbeCompanyId");
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(apiKey))
            {
                return null;
            }
            return new Birko.SuperFaktura.SuperFaktura(email, apiKey, companyId: !string.IsNullOrEmpty(companyId) ? int.Parse(companyId) : null);
        }

        private readonly ITestOutputHelper output;

        public ResponseTypesTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        public static IEnumerable<object[]> Lists()
        {
            yield return new object[] { "invoices/index.json" + new Birko.SuperFaktura.Request.Invoice.Filter { PerPage = 200 }.ToParameters(), typeof(Birko.SuperFaktura.Response.Invoice.PagedResponse) };
            yield return new object[] { "expenses/index.json" + new Birko.SuperFaktura.Request.Expense.Filter { PerPage = 200 }.ToParameters(), typeof(Birko.SuperFaktura.Response.PagedResponse<Birko.SuperFaktura.Response.Expense.Detail>) };
            yield return new object[] { "clients/index.json" + new Birko.SuperFaktura.Request.Client.Filter { PerPage = 200 }.ToParameters(), typeof(Birko.SuperFaktura.Response.PagedResponse<Birko.SuperFaktura.Response.Client.Item>) };
            yield return new object[] { "bank_accounts/index", typeof(Birko.SuperFaktura.Response.BankAccounts.BankAccounts) };
            yield return new object[] { "cash_registers/getDetails", typeof(IEnumerable<Birko.SuperFaktura.Response.CashRegister.CashRegisterData>) };
        }

        [Theory]
        [MemberData(nameof(Lists))]
        public async Task TestListValuesFitModelTypes(string url, Type type)
        {
            var json = await client.Get(url);
            var result = ModelCoverage.Compare(JToken.Parse(json), type);
            output.WriteLine(client.APIURL + url);
            output.WriteLine(result.ToString());

            Should.NotThrow(() => Newtonsoft.Json.JsonConvert.DeserializeObject(json, type));
            result.Mismatched.ShouldBeEmpty();
        }
    }
}
