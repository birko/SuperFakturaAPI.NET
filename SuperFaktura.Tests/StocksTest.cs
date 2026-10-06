using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class StocksTest: SuperFakturaTest
    {
        // Unique SKU, so the item is found by the sku filter and never collides with existing items.
        private static async Task<Birko.SuperFaktura.Response.Stock.Detail> AddTestItem(string name = "test")
        {
            return await apiClient.Stock.Add(new Birko.SuperFaktura.Request.Stock.Item()
            {
                Description = "Test desc",
                Name = name,
                SKU = UniqueName("SKU"),
                Unit = "ks",
                UnitPrice = 1,
                VAT = 20,
                WatchStock = true,
                Stock = 10,
                PurchaseUnitPrice = 1,
                PurchaseCurrency = "EUR"
            });
        }

        [Fact]
        public async Task TestList()
        {
            var added = await AddTestItem("List test");
            try
            {
                // sku filter: exactly the item created by this test
                var list = await apiClient.Stock.List(new Birko.SuperFaktura.Request.Stock.Filter() { SKU = added.StockItem.SKU });
                list.ShouldNotBeNull();
                list.Items.ShouldNotBeNull();
                list.ItemCount.ShouldBe(1);
                list.Items.Single().StockItem.ID.ShouldBe(added.StockItem.ID);
            }
            finally
            {
                await apiClient.Stock.Delete(added.StockItem.ID.Value);
            }
        }

        [Fact]
        public async Task TestAdd()
        {
            var task = await AddTestItem();
            try
            {
                task.ShouldNotBeNull();
                task.StockItem.ID.ShouldNotBeNull();
            }
            finally
            {
                await apiClient.Stock.Delete(task.StockItem.ID.Value);
            }
        }

        [Fact]
        public async Task TestView()
        {
            var added = await AddTestItem();
            try
            {
                var task = await apiClient.Stock.View(added.StockItem.ID.Value);
                task.ShouldNotBeNull();
                task.SKU.ShouldBe(added.StockItem.SKU);
            }
            finally
            {
                await apiClient.Stock.Delete(added.StockItem.ID.Value);
            }
        }

        [Fact]
        public async Task TestEdit()
        {
            var added = await AddTestItem();
            try
            {
                var task = await apiClient.Stock.Edit(added.StockItem.ID.Value, new Birko.SuperFaktura.Request.Stock.Item()
                {
                    Description = "Test desc Edit",
                });
                task.ShouldNotBeNull();
                var view = await apiClient.Stock.View(added.StockItem.ID.Value);
                view.Description.ShouldBe("Test desc Edit");
                view.Stock.ShouldBe(10);
            }
            finally
            {
                await apiClient.Stock.Delete(added.StockItem.ID.Value);
            }
        }

        [Fact]
        public async Task TestAddStockMovement()
        {
            var added = await AddTestItem();
            try
            {
                var task = await apiClient.Stock.AddStockMovement(new Birko.SuperFaktura.Request.Stock.Log()
                {
                    StockItemID = added.StockItem.ID.Value,
                    Note = "TestMovement",
                    Quantity = 1
                });
                task.ShouldNotBeNull();
                (await apiClient.Stock.View(added.StockItem.ID.Value)).Stock.ShouldBe(11);
            }
            finally
            {
                await apiClient.Stock.Delete(added.StockItem.ID.Value);
            }
        }

        [Fact]
        public async Task TestListStockMovements()
        {
            var added = await AddTestItem();
            try
            {
                await apiClient.Stock.AddStockMovement(new Birko.SuperFaktura.Request.Stock.Log()
                {
                    StockItemID = added.StockItem.ID.Value,
                    Note = "TestMovement",
                    Quantity = 1
                });
                var task = await apiClient.Stock.ListStockMovements(added.StockItem.ID.Value, new Birko.SuperFaktura.Request.PagedParameters());
                task.ShouldNotBeNull();
                task.Items.ShouldNotBeNull();
                task.Items.ShouldContain(x => x.StockLog.Note == "TestMovement");
                task.ItemCount.ShouldBeGreaterThan(0);
            }
            finally
            {
                await apiClient.Stock.Delete(added.StockItem.ID.Value);
            }
        }

        [Fact]
        public async Task TestDelete()
        {
            var added = await AddTestItem();
            var task = await apiClient.Stock.Delete(added.StockItem.ID.Value);
            task.ShouldNotBeNull();
            (await apiClient.Stock.List(new Birko.SuperFaktura.Request.Stock.Filter() { SKU = added.StockItem.SKU })).ItemCount.ShouldBe(0);
        }
    }
}
