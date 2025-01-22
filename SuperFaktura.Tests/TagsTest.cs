using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class TagsTest: SuperFakturaTest
    {

        [Fact]
        public async Task TestTags()
        {
            var tags = await apiClient.Tags.GetTags();
            tags.ShouldNotBe(null);
        }

    }
}
