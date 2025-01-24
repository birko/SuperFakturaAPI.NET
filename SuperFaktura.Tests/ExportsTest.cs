using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class XportsTest : SuperFakturaTest
    {
        [Fact]
        public async Task TestList()
        {
            var export = await apiClient.Exports.List(new Birko.SuperFaktura.Request.Export.Filter() { });
            export.ShouldNotBe(null);
        }


        [Fact]
        public async Task TestShow()
        {
            var export = await apiClient.Exports.Status(1363);
            export.ShouldNotBe(null);
        }
    }
}
