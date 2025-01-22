using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

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
            apiClient = new Birko.SuperFaktura.SuperFakturaSandbox(superFakturaApiEmail, superFakturaApiKey);
        }
    }
}
