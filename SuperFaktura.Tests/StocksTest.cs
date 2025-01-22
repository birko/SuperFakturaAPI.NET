using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class StocksTest: SuperFakturaTest
    {
        [Fact]
        public async Task TestStock()
        {
            var task = await apiClient.Stock.Save(new Birko.SuperFaktura.Request.Stock.Item()
            {
                Description = "Test desc",
                Name = "test",
                SKU = "test0001",
                Unit = "ks",
                UnitPrice = 1,
                VAT = 20,
                WatchStock = true,
                PurchaseUnitPrice = 1,
            });
            task.Error.ShouldBe(0);
        }
    }
}
