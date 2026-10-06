using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class TagsTest : SuperFakturaTest
    {
        // Tag names must be unique on the account.
        private static async Task<Birko.SuperFaktura.Response.Tag.Tag> AddTestTag()
        {
            return await apiClient.Tags.Add(new Birko.SuperFaktura.Request.Tags.Tag()
            {
                Name = UniqueName("test")
            });
        }

        [Fact]
        public async Task TestList()
        {
            var added = await AddTestTag();
            try
            {
                var tags = await apiClient.Tags.List();
                tags.ShouldNotBe(null);
                tags.ShouldContainKey(added.ID);
            }
            finally
            {
                await apiClient.Tags.Delete(added.ID);
            }
        }

        [Fact]
        public async Task TestAdd()
        {
            var tag = await AddTestTag();
            try
            {
                tag.ShouldNotBe(null);
                tag.ID.ShouldBeGreaterThan(0);
                tag.Name.ShouldStartWith("test-");
            }
            finally
            {
                await apiClient.Tags.Delete(tag.ID);
            }
        }

        [Fact]
        public async Task TestEdit()
        {
            var added = await AddTestTag();
            try
            {
                var name = UniqueName("test edit");
                var tag = await apiClient.Tags.Edit(added.ID, new Birko.SuperFaktura.Request.Tags.Tag()
                {
                    Name = name
                });
                tag.ShouldNotBe(null);
                (await apiClient.Tags.List())[added.ID].ShouldBe(name);
            }
            finally
            {
                await apiClient.Tags.Delete(added.ID);
            }
        }

        [Fact]
        public async Task TestDelete()
        {
            var added = await AddTestTag();
            var tag = await apiClient.Tags.Delete(added.ID);
            tag.ShouldNotBe(null);
            (await apiClient.Tags.List()).ShouldNotContainKey(added.ID);
        }
    }
}
