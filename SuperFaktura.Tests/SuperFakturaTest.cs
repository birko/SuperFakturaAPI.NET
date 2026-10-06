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
    }
}
