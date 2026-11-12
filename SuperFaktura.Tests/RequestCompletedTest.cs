using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    // End to end on the sandbox (always SuperFakturaSandbox, also in Release): RequestCompleted fires for
    // successful and failed requests and never exposes the credentials.
    public class RequestCompletedTest : SuperFakturaTest
    {
        [Fact]
        public async Task TestRequestCompletedReportsRequestsWithoutCredentials()
        {
            var email = System.Environment.GetEnvironmentVariable("SuperFakturaApiEmail");
            var apiKey = System.Environment.GetEnvironmentVariable("SuperFakturaApiKey");
            var companyId = System.Environment.GetEnvironmentVariable("SuperFakturaApiCompanyId");
            // own instance: the event belongs to the client object, the shared apiClient stays untouched
            var client = new Birko.SuperFaktura.SuperFakturaSandbox(email, apiKey, companyId: !string.IsNullOrEmpty(companyId) ? int.Parse(companyId) : null)
            {
                IncludeBodiesInEvents = true,
            };
            var events = new List<Birko.SuperFaktura.RequestCompletedEventArgs>();
            client.RequestCompleted += (s, e) => events.Add(e);

            await client.ValueLists.ListCountries();
            await Should.ThrowAsync<Birko.SuperFaktura.Exceptions.Exception>(() => client.Invoices.SendPeppol(0));

            events.Count.ShouldBe(2);
            events[0].Method.ShouldBe("GET");
            events[0].StatusCode.ShouldBe(200);
            events[0].ResponseBody.ShouldNotBeNullOrEmpty();
            events[0].Exception.ShouldBeNull();

            events[1].Method.ShouldBe("POST");
            events[1].Uri.ShouldBe("invoices/sendPeppol");
            events[1].StatusCode.ShouldBe(400);
            events[1].RequestBody.ShouldBe("{\"invoice_id\":0}");
            events[1].ResponseBody.ShouldContain(Birko.SuperFaktura.Request.ValueLists.PeppolErrorCode.InvalidPayload);
            events[1].Exception.ShouldBeOfType<Birko.SuperFaktura.Exceptions.Exception>().ErrorCode.ShouldBe(Birko.SuperFaktura.Request.ValueLists.PeppolErrorCode.InvalidPayload);

            foreach (var e in events)
            {
                var text = $"{e.Method} {e.Uri} {e.RequestBody} {e.ResponseBody} {e.Exception}";
                text.ShouldNotContain(apiKey);
                text.ShouldNotContain(System.Uri.EscapeDataString(apiKey));
                e.Duration.ShouldBeGreaterThan(System.TimeSpan.Zero);
            }
        }
    }
}
