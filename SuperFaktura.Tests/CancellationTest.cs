using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SuperFaktura.Tests
{
    // Offline: a local TCP server that accepts the connection, reads the request and never answers.
    public class CancellationTest
    {
        // A regression (token not passed on) would otherwise wait out TimeoutSeconds; this fails it fast with TimeoutException.
        private static readonly TimeSpan FailFast = TimeSpan.FromSeconds(5);

        private sealed class LocalSuperFaktura : Birko.SuperFaktura.SuperFaktura
        {
            // A unique API key per client, so the static 1-second spacing never links two tests.
            public LocalSuperFaktura(int port)
                : base("test@example.com", Guid.NewGuid().ToString("N"))
            {
                APIURL = $"http://127.0.0.1:{port}/";
            }
        }

        private sealed class SilentServer : IDisposable
        {
            private readonly TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
            private readonly TaskCompletionSource<TcpClient> accepted = new TaskCompletionSource<TcpClient>();

            public SilentServer()
            {
                listener.Start();
                _ = AcceptAsync();
            }

            public int Port => ((IPEndPoint)listener.LocalEndpoint).Port;

            public Task<TcpClient> Accepted => accepted.Task;

            private async Task AcceptAsync()
            {
                try
                {
                    accepted.TrySetResult(await listener.AcceptTcpClientAsync());
                }
                catch (Exception ex)
                {
                    accepted.TrySetException(ex);
                }
            }

            // True when the client closed the connection: the HTTP request was aborted, not just abandoned.
            public static async Task<bool> ClosedByClient(TcpClient connection, TimeSpan within)
            {
                var stream = connection.GetStream();
                var buffer = new byte[4096];
                using var timeout = new CancellationTokenSource(within);
                try
                {
                    while (await stream.ReadAsync(buffer, 0, buffer.Length, timeout.Token) > 0)
                    {
                        // Skip the request itself.
                    }
                    return true;
                }
                catch (IOException)
                {
                    return true; // Connection reset.
                }
                catch (OperationCanceledException)
                {
                    return false;
                }
            }

            public void Dispose()
            {
                listener.Stop();
                if (accepted.Task.IsCompletedSuccessfully)
                {
                    accepted.Task.Result.Dispose();
                }
            }
        }

        [Fact]
        public async Task CancelledTokenAbortsSlowRequest()
        {
            using var server = new SilentServer();
            var client = new LocalSuperFaktura(server.Port) { TimeoutSeconds = 30 };
            using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(300));
            var stopwatch = Stopwatch.StartNew();

            await Should.ThrowAsync<OperationCanceledException>(() => client.ValueLists.ListCountries(cts.Token).WaitAsync(FailFast));

            stopwatch.Elapsed.ShouldBeLessThan(TimeSpan.FromSeconds(5));
            var connection = await server.Accepted.WaitAsync(TimeSpan.FromSeconds(5));
            (await SilentServer.ClosedByClient(connection, TimeSpan.FromSeconds(5))).ShouldBeTrue();
        }

        [Fact]
        public async Task CancellationIsNotWrappedAsApiException()
        {
            using var server = new SilentServer();
            var client = new LocalSuperFaktura(server.Port);
            using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(200));

            var ex = await Should.ThrowAsync<Exception>(() => client.Invoices.View(1, cts.Token).WaitAsync(FailFast));

            ex.ShouldBeAssignableTo<OperationCanceledException>();
            ex.ShouldNotBeAssignableTo<Birko.SuperFaktura.Exceptions.Exception>();
        }

        [Fact]
        public async Task CancelledRequestRaisesRequestCompleted()
        {
            using var server = new SilentServer();
            var client = new LocalSuperFaktura(server.Port);
            var events = new List<Birko.SuperFaktura.RequestCompletedEventArgs>();
            client.RequestCompleted += (sender, e) => events.Add(e);
            using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(200));

            await Should.ThrowAsync<OperationCanceledException>(() => client.Invoices.SendPeppol(1, cancellationToken: cts.Token).WaitAsync(FailFast));

            events.Count.ShouldBe(1);
            events[0].Method.ShouldBe("POST");
            events[0].Uri.ShouldBe("invoices/sendPeppol");
            events[0].StatusCode.ShouldBeNull();
            events[0].Exception.ShouldBeAssignableTo<OperationCanceledException>();
        }

        [Fact]
        public async Task AlreadyCancelledTokenSendsNothing()
        {
            using var server = new SilentServer();
            var client = new LocalSuperFaktura(server.Port);
            var events = 0;
            client.RequestCompleted += (sender, e) => events++;

            await Should.ThrowAsync<OperationCanceledException>(() => client.Tags.List(new CancellationToken(true)));

            events.ShouldBe(0);
            await Task.Delay(200);
            server.Accepted.IsCompleted.ShouldBeFalse();
        }

        [Fact]
        public async Task CancellationCutsTheRequestSpacingShort()
        {
            using var server = new SilentServer();
            var client = new LocalSuperFaktura(server.Port);
            // Records this profile's last request time, so the next call has to wait out the 1-second spacing.
            await Should.ThrowAsync<OperationCanceledException>(() => client.Tags.List(new CancellationToken(true)));
            using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(100));
            var stopwatch = Stopwatch.StartNew();

            await Should.ThrowAsync<OperationCanceledException>(() => client.Tags.List(cts.Token).WaitAsync(FailFast));

            stopwatch.Elapsed.ShouldBeLessThan(TimeSpan.FromMilliseconds(800));
            server.Accepted.IsCompleted.ShouldBeFalse();
        }

        [Fact]
        public async Task TimeoutIsStillAnApiException()
        {
            using var server = new SilentServer();
            var client = new LocalSuperFaktura(server.Port) { TimeoutSeconds = 1 };

            var ex = await Should.ThrowAsync<Birko.SuperFaktura.Exceptions.Exception>(() => client.Tags.List());

            ex.InnerException.ShouldBeAssignableTo<OperationCanceledException>();
        }
    }
}
