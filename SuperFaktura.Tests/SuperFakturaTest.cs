namespace SuperFaktura.Tests
{
    public class SuperFakturaTest
    {
        protected static Birko.SuperFaktura.SuperFaktura apiClient = null;

        static SuperFakturaTest()
        {
            LaunchSettingsFixture.Load();
            var superFakturaApiEmail = System.Environment.GetEnvironmentVariable("SuperFakturaApiEmail");
            var superFakturaApiKey = System.Environment.GetEnvironmentVariable("SuperFakturaApiKey");
            var superFakturaApiCompanyId = System.Environment.GetEnvironmentVariable("SuperFakturaApiCompanyId");
#if DEBUG
            apiClient = new Birko.SuperFaktura.SuperFakturaSandbox(superFakturaApiEmail, superFakturaApiKey, companyId: !string.IsNullOrEmpty(superFakturaApiCompanyId) ? int.Parse(superFakturaApiCompanyId) : null);
#endif
#if !DEBUG
            apiClient = new Birko.SuperFaktura.SuperFaktura(superFakturaApiEmail, superFakturaApiKey, companyId: !string.IsNullOrEmpty(superFakturaApiCompanyId) ? int.Parse(superFakturaApiCompanyId) : null);
#endif
        }

        // Unique suffix so tests running in parallel never touch each other's data.
        protected static string UniqueName(string prefix)
        {
            return $"{prefix}-{System.Guid.NewGuid().ToString("N").Substring(0, 8)}";
        }

        protected static async System.Threading.Tasks.Task<int> CreateTestClient(string prefix = "sf-test-client")
        {
            var client = await apiClient.Clients.Add(new Birko.SuperFaktura.Request.Client.Client { Name = UniqueName(prefix) });
            return client.ID.Value;
        }

        // Regular invoice with one item for a new client; delete with DeleteTestInvoice.
        protected static async System.Threading.Tasks.Task<Birko.SuperFaktura.Response.Invoice.Detail> CreateTestInvoice(string prefix = "sf-test-invoice")
        {
            return await apiClient.Invoices.Add(
                new Birko.SuperFaktura.Request.Invoice.Invoice { Name = UniqueName(prefix) },
                // full address: sending by post fails with "Address data error" (6) without it
                new Birko.SuperFaktura.Request.Client.Client
                {
                    Name = UniqueName(prefix + "-client"),
                    Phone = "+421900000000",
                    Email = "recipient@example.com",
                    Address = "Hlavná 1",
                    City = "Bratislava",
                    ZIP = "81101",
                    CountryID = 191,
                },
                new[] { new Birko.SuperFaktura.Request.Invoice.Item { Name = "test item", Quantity = 1, UnitPrice = 10, Tax = 20 } });
        }

        protected static async System.Threading.Tasks.Task DeleteTestInvoice(Birko.SuperFaktura.Response.Invoice.Detail invoice)
        {
            await apiClient.Invoices.Delete(invoice.Invoice.ID.Value);
            if (invoice.Invoice.ClientID.HasValue)
            {
                await apiClient.Clients.Delete(invoice.Invoice.ClientID.Value);
            }
        }

        // Features the sandbox account has not enabled (SMTP, post stamps, SMS credit) answer with a known
        // API error; the request still reached the API correctly, so the test accepts these codes.
        protected static async System.Threading.Tasks.Task<T> AllowSandboxLimit<T>(System.Func<System.Threading.Tasks.Task<T>> call, params int[] errorCodes) where T : class
        {
            try
            {
                return await call();
            }
            catch (Birko.SuperFaktura.Exceptions.Exception ex) when (ex.Error.HasValue && System.Array.IndexOf(errorCodes, ex.Error.Value) >= 0)
            {
                return null;
            }
        }
    }
}
