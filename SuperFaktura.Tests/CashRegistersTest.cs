using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    // Cash registers cannot be created through the API: the tests use the first existing one (read-only)
    // and return when there is none. Items are created by the tests and deleted afterwards.
    public class CashRegistersTest : SuperFakturaTest
    {
        private static async Task<int?> FirstCashRegisterId()
        {
            var cashRegisters = await apiClient.CashRegisters.List();
            return cashRegisters?.FirstOrDefault()?.ID;
        }

        private static async Task<Birko.SuperFaktura.Response.CashRegister.CashRegisterItemResponse> AddTestItem(int cashRegisterId, string description)
        {
            return await apiClient.CashRegisters.AddItem(new Birko.SuperFaktura.Request.CashRegister.CashRegisterItem()
            {
                CashRegisterID = cashRegisterId,
                Amount = 5,
                Description = description
            });
        }

        [Fact]
        public async Task TestList()
        {
            var cashRegisters = await apiClient.CashRegisters.List();
            cashRegisters.ShouldNotBe(null);
            cashRegisters.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task TestView()
        {
            var id = await FirstCashRegisterId();
            if (id == null)
            {
                return;
            }

            var cashRegister = await apiClient.CashRegisters.View(id.Value);
            cashRegister.ShouldNotBe(null);
            cashRegister.ID.ShouldBe(id);
        }

        [Fact]
        public async Task TestGetItems()
        {
            var id = await FirstCashRegisterId();
            if (id == null)
            {
                return;
            }
            var description = UniqueName("TEST list");
            var added = await AddTestItem(id.Value, description);
            try
            {
                var items = await apiClient.CashRegisters.ListItems(new Birko.SuperFaktura.Request.CashRegister.Filter()
                {
                    ID = id.Value,
                    Term = description
                });
                items.ShouldNotBe(null);
                items.CashRegister.ID.ShouldBe(id);
                items.Items.ShouldContain(x => x.CashRegisterItem.ID == added.CashRegisterItem.ID);
            }
            finally
            {
                await apiClient.CashRegisters.DeleteItem(added.CashRegisterItem.ID);
            }
        }

        [Fact]
        public async Task TestAddItem()
        {
            var id = await FirstCashRegisterId();
            if (id == null)
            {
                return;
            }

            var item = await AddTestItem(id.Value, "TEST");
            try
            {
                item.ShouldNotBe(null);
                item.CashRegisterItem.Description.ShouldBe("TEST");
            }
            finally
            {
                await apiClient.CashRegisters.DeleteItem(item.CashRegisterItem.ID);
            }
        }

        [Fact]
        public async Task TestDeleteItem()
        {
            var id = await FirstCashRegisterId();
            if (id == null)
            {
                return;
            }
            var added = await AddTestItem(id.Value, "TEST delete");

            var summary = await apiClient.CashRegisters.DeleteItem(added.CashRegisterItem.ID);
            summary.ShouldNotBe(null);
            summary.Status.ShouldBe(1);
        }

        [Fact]
        public async Task TestDeleteItems()
        {
            var id = await FirstCashRegisterId();
            if (id == null)
            {
                return;
            }
            var first = await AddTestItem(id.Value, "TEST delete items 1");
            var second = await AddTestItem(id.Value, "TEST delete items 2");

            var summary = await apiClient.CashRegisters.DeleteItems(new[] { first.CashRegisterItem.ID, second.CashRegisterItem.ID });
            summary.ShouldNotBe(null);
            summary.Status.ShouldBe(1);
        }

        [Fact]
        public async Task TestDownload()
        {
            var id = await FirstCashRegisterId();
            if (id == null)
            {
                return;
            }
            var added = await AddTestItem(id.Value, "TEST receipt");
            try
            {
                var bytes = await apiClient.CashRegisters.Download(added.CashRegisterItem.ID);
                bytes.ShouldNotBeEmpty();
            }
            finally
            {
                await apiClient.CashRegisters.DeleteItem(added.CashRegisterItem.ID);
            }
        }
    }
}
