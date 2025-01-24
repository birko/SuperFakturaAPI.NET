using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class TagsTest: SuperFakturaTest
    {

        [Fact]
        public async Task TestList()
        {
            var tags = await apiClient.Tags.List();
            tags.ShouldNotBe(null);
        }


        [Fact]
        public async Task TestAdd()
        {
            var tag = await apiClient.Tags.Add(new Birko.SuperFaktura.Request.Tags.Tag()
            {
                Name = "test"
            });
            tag.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestEdit()
        {
            var tags = await apiClient.Tags.List();
            if (!(tags?.Any() ?? false))
            {
                return;
            }
            var tag = await apiClient.Tags.Edit(tags.Keys.Max(), new Birko.SuperFaktura.Request.Tags.Tag()
            {
                Name = "test edit"
            });
            tag.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestDelete()
        {
            var tags = await apiClient.Tags.List();
            if (!(tags?.Any() ?? false))
            {
                return;
            }
            var tag = await apiClient.Tags.Delete(tags.Keys.Max());
            tag.ShouldNotBe(null);
        }
    }
}
