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
        [Fact]
        public async Task TestExport()
        {
            var export = await apiClient.Exports.Export(new Birko.SuperFaktura.Request.Export.ExportData() {
                Invoice = new Birko.SuperFaktura.Request.Export.Invoice() {
                    IDS = new[] { 60121, 37882 }
                },
                Export = new Birko.SuperFaktura.Request.Export.Export() {
                    InvoicesPDF = true
                }
            });
            export.ShouldNotBe(null);
        }


        [Fact]
        public async Task TestStatus()
        {
            var export = await apiClient.Exports.Status(132);
            export.ShouldNotBe(null);
        }

        [Fact]
        public async Task TestDownload()
        {
            var bytes = await apiClient.Exports.Download(132);
            bytes.ShouldNotBeEmpty();
        }
    }
}
