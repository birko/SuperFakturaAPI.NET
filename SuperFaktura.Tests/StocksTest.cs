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
        [Fact]
        public async Task TestList()
        {
            var list = await apiClient.Stock.Get(new Birko.SuperFaktura.Request.Stock.Filter() { });
            list.ShouldNotBeNull();
            list.Items.ShouldNotBeNull();
            list.Items.Count.Equals(0);
            list.ItemCount.Equals(0);
        }

        [Fact]
        public async Task TestAdd()
        {
            var task = await apiClient.Stock.Add(new Birko.SuperFaktura.Request.Stock.Item()
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
            task.ShouldNotBeNull();
        }


        [Fact]
        public async Task TestEdit()
        {
            var list = await apiClient.Stock.Get(new Birko.SuperFaktura.Request.Stock.Filter() { });
            if (!(list?.Items?.Any() ?? false))
            {
                return;
            }
            var task = await apiClient.Stock.Edit(list.Items.First().StockItem.ID.Value, new Birko.SuperFaktura.Request.Stock.Item()
            {
                Description = "Test desc Edit",
                Name = "test",
                SKU = "test0001",
                Unit = "ks",
                UnitPrice = 1,
                VAT = 20,
                WatchStock = true,
                PurchaseUnitPrice = 1,
            });
            task.ShouldNotBeNull();
        }

        [Fact]
        public async Task TestAddStockMovement()
        {
            var list = await apiClient.Stock.Get(new Birko.SuperFaktura.Request.Stock.Filter() { });
            if (!(list?.Items?.Any() ?? false))
            {
                return;
            }
            var task = await apiClient.Stock.AddStockMovement(new Birko.SuperFaktura.Request.Stock.Log()
            {
                StockItemID = list.Items.First().StockItem.ID.Value,
                Note = "TestMovement",
                Quantity = 1
            });
            task.ShouldNotBeNull();
        }

        [Fact]
        public async Task TestGetStockMovement()
        {
            var list = await apiClient.Stock.Get(new Birko.SuperFaktura.Request.Stock.Filter() { });
            if (!(list?.Items?.Any() ?? false))
            {
                return;
            }
            var task = await apiClient.Stock.GetStockMovement(list.Items.First().StockItem.ID.Value, new Birko.SuperFaktura.Request.PagedParameters());
            task.ShouldNotBeNull();
            task.Items.ShouldNotBeNull();
            task.Items.Count.ShouldBeGreaterThan(0);
            task.ItemCount.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task TestDelete()
        {
            var list = await apiClient.Stock.Get(new Birko.SuperFaktura.Request.Stock.Filter() { });
            if (!(list?.Items?.Any() ?? false))
            {
                return;
            }
            var task = await apiClient.Stock.Delete(list.Items.First().StockItem.ID.Value);
            task.ShouldNotBeNull();
        }
    }
}
