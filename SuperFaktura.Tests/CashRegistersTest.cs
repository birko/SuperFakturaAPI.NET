using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class CashRegistersTest : SuperFakturaTest
    {
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
            var cashRegisters = await apiClient.CashRegisters.List();
            if (!(cashRegisters?.Any() ?? false))
            {
                return;
            }

            var cashRegister = await apiClient.CashRegisters.View(cashRegisters.First().ID.Value);
            cashRegister.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestGetItems()
        {
            var cashRegisters = await apiClient.CashRegisters.List();
            if (!(cashRegisters?.Any() ?? false))
            {
                return;
            }

            var items = await apiClient.CashRegisters.ListItems(new Birko.SuperFaktura.Request.CashRegister.Filter()
            {
                ID = cashRegisters.First().ID.Value
            });
            items.ShouldNotBe(null);
            items.ItemCount.Equals(1);
            items.Items.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task TestAddItem()
        {
            var cashRegisters = await apiClient.CashRegisters.List();
            if (!(cashRegisters?.Any() ?? false))
            {
                return;
            }

            var item = await apiClient.CashRegisters.AddItem(new Birko.SuperFaktura.Request.CashRegister.CashRegisterItem()
            {
                CashRegisterID = cashRegisters.First().ID.Value,
                Amount = 5,
                Description = "TEST"
            });
            item.ShouldNotBe(null);
            item.CashRegisterItem.Description.Equals("TEST");
        }

        [Fact]
        public async Task TestDeleteItem()
        {
            var cashRegisters = await apiClient.CashRegisters.List();
            if (!(cashRegisters?.Any() ?? false))
            {
                return;
            }
            var items = await apiClient.CashRegisters.ListItems(new Birko.SuperFaktura.Request.CashRegister.Filter()
            {
                ID = cashRegisters.First().ID.Value
            });

            if (!(items.Items?.Any() ?? false))
            {
                return;
            }
            var summary = await apiClient.CashRegisters.DeleteItem(items.Items.FirstOrDefault().CashRegisterItem.ID);
            summary.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestDeleteItems()
        {
            var cashRegisters = await apiClient.CashRegisters.List();
            if (!(cashRegisters?.Any() ?? false))
            {
                return;
            }
            var items = await apiClient.CashRegisters.ListItems(new Birko.SuperFaktura.Request.CashRegister.Filter()
            {
                ID = cashRegisters.First().ID.Value
            });

            if (!(items.Items?.Any() ?? false))
            {
                return;
            }
            var summary = await apiClient.CashRegisters.DeleteItems(items.Items.Select(x => x.CashRegisterItem.ID));
        }

        [Fact]
        public async Task TestDownload()
        {
            var cashRegisters = await apiClient.CashRegisters.List();
            if (!(cashRegisters?.Any() ?? false))
            {
                return;
            }
            var items = await apiClient.CashRegisters.ListItems(new Birko.SuperFaktura.Request.CashRegister.Filter()
            {
                ID = cashRegisters.First().ID.Value
            });

            if (!(items.Items?.Any() ?? false))
            {
                return;
            }
            var bytes = await apiClient.CashRegisters.Download(items.Items.FirstOrDefault().CashRegisterItem.ID);
            bytes.ShouldNotBeEmpty();
        }
    }
}
