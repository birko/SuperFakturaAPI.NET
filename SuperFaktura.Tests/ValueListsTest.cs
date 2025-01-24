using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class ValueListsTest : SuperFakturaTest
    {
        [Fact]
        public async Task TestListCountries()
        {
            var countries = await apiClient.ValueLists.ListCountries();
            countries.Count.ShouldBe(253);
            countries[191].ShouldBe("Slovensko");
        }

        [Fact]
        public async Task TestListCountriesFull()
        {
            var countries = await apiClient.ValueLists.ListCountriesFull();
            countries.Count().ShouldBe(253);
            countries.First().Name.ShouldBe("Slovensko");
        }

        [Fact]
        public async Task TestListtExpenseCategories()
        {
            var categories = await apiClient.ValueLists.ListExpenseCategories();
            categories.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task TestListLogos()
        {
            var logos = await apiClient.ValueLists.ListLogos();
            logos.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task TestListSequences()
        {
            var seqv = await apiClient.ValueLists.ListSequences();
            seqv.Count.ShouldBeGreaterThan(0);
        }

    }
}
