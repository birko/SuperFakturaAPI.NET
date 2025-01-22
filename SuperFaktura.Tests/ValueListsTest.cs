using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class ValueListsTest : SuperFakturaTest
    {
        [Fact]
        public async Task TestCountries()
        {
            var countries = await apiClient.GetCountries();
            countries.Count.ShouldBe(253);
            countries[191].ShouldBe("Slovensko");
        }
    }
}
