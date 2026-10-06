using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class OtherTest: SuperFakturaTest
    {

        [Fact]
        public async Task TestListAccounts()
        {
            var task = await apiClient.Other.ListAccounts();
            task.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestListUserCompanies()
        {
            var task = await apiClient.Other.ListUserCompanies(true);
            task.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestSendSMS()
        {
            var invoice = await CreateTestInvoice();
            try
            {
                // error 3: "SMS not sent" - the sandbox account has no prepaid SMS
                var task = await AllowSandboxLimit(() => apiClient.Other.SendSMS(new Birko.SuperFaktura.Request.Other.SMS()
                {
                    InvoiceID = invoice.Invoice.ID.Value,
                    Text = "test",
                    Phone = "+421900000000"
                }), 3);
                task?.InvoiceID.ShouldBe(invoice.Invoice.ID.Value);
            }
            finally
            {
                await DeleteTestInvoice(invoice);
            }
        }

        [Fact]
        public async Task TestListBankAccountMovements()
        {
            var task = await apiClient.Other.ListBankAccountMovements(new Birko.SuperFaktura.Request.Other.BankMovementFilter());
            task.ShouldNotBe(null);
        }

        [Fact]
        public async Task ListActivityLogs()
        {
            var invoice = await CreateTestInvoice();
            try
            {
                var task = await apiClient.Other.ListActivityLogs(Birko.SuperFaktura.Request.ValueLists.DocumentType.Invoice, invoice.Invoice.ID.Value);
                task.ShouldNotBe(null);
                task.ShouldNotBeEmpty(); // at least the "create" event
            }
            finally
            {
                await DeleteTestInvoice(invoice);
            }
        }
    }
}
