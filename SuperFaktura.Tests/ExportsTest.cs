using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    public class ExportsTest : SuperFakturaTest
    {
        private static async Task<Birko.SuperFaktura.Response.Export.Export> StartExport(params int[] invoiceIds)
        {
            return await apiClient.Exports.Export(new Birko.SuperFaktura.Request.Export.ExportData() {
                Invoice = new Birko.SuperFaktura.Request.Export.Invoice() {
                    IDS = invoiceIds
                },
                Export = new Birko.SuperFaktura.Request.Export.Export() {
                    InvoicesPDF = true
                }
            });
        }

        // Exports run asynchronously on the server.
        private static async Task<Birko.SuperFaktura.Response.Export.Export> WaitForExport(int exportId)
        {
            var timeout = DateTime.UtcNow.AddMinutes(2);
            Birko.SuperFaktura.Response.Export.Export status;
            do
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
                status = await apiClient.Exports.Status(exportId);
            }
            while (status.Status != Birko.SuperFaktura.Request.ValueLists.ExportStatus.Completed
                && status.Status != Birko.SuperFaktura.Request.ValueLists.ExportStatus.Failed
                && DateTime.UtcNow < timeout);
            return status;
        }

        [Fact]
        public async Task TestExport()
        {
            var invoice = await CreateTestInvoice();
            try
            {
                var export = await StartExport(invoice.Invoice.ID.Value);
                export.ShouldNotBe(null);
                export.ID.ShouldBeGreaterThan(0);
            }
            finally
            {
                await DeleteTestInvoice(invoice);
            }
        }

        [Fact]
        public async Task TestStatus()
        {
            var invoice = await CreateTestInvoice();
            try
            {
                var export = await StartExport(invoice.Invoice.ID.Value);
                var status = await apiClient.Exports.Status(export.ID);
                status.ShouldNotBe(null);
                status.ID.ShouldBe(export.ID);
            }
            finally
            {
                await DeleteTestInvoice(invoice);
            }
        }

        [Fact]
        public async Task TestDownload()
        {
            var invoice = await CreateTestInvoice();
            try
            {
                var export = await StartExport(invoice.Invoice.ID.Value);
                var status = await WaitForExport(export.ID);
                status.Status.ShouldBe(Birko.SuperFaktura.Request.ValueLists.ExportStatus.Completed);
                var bytes = await apiClient.Exports.Download(export.ID);
                bytes.ShouldNotBeEmpty();
            }
            finally
            {
                await DeleteTestInvoice(invoice);
            }
        }
    }
}
