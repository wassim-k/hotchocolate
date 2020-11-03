using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate.AspNetCore.Subscriptions.Messages;
using HotChocolate.Execution;

namespace HotChocolate.AspNetCore.Subscriptions
{
    internal sealed class Subscription : ISubscription
    {
        internal const byte Delimiter = 0x07;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly ISocketConnection _connection;
        private readonly IResponseStream _responseStream;
        private Task? _task;
        private bool _disposed;

        public event EventHandler? Completed;

        public Subscription(
            ISocketConnection connection,
            IResponseStream responseStream,
            string id)
        {
            _connection = connection ??
                throw new ArgumentNullException(nameof(connection));
            _responseStream = responseStream ??
                throw new ArgumentNullException(nameof(responseStream));
            Id = id ??
                throw new ArgumentNullException(nameof(id));
        }

        public string Id { get; }

        public void Start()
        {
            if (!_disposed && _task is null)
            {
                _task = SendResultsAsync();
            }
        }

        public async Task StopAsync()
        {
            await DisposeAsync();
        }

        private async Task SendResultsAsync()
        {
            try
            {
                await foreach (IQueryResult result in
                    _responseStream.ReadResultsAsync().WithCancellation(_cts.Token))
                {
                    using (result)
                    {
                        await _connection.SendAsync(new DataResultMessage(Id, result), _cts.Token);
                    }
                }

                if (!_cts.IsCancellationRequested)
                {
                    await _connection.SendAsync(new DataCompleteMessage(Id), _cts.Token);
                    Completed?.Invoke(this, EventArgs.Empty);
                }
            }
            catch (OperationCanceledException)
            {
                // the subscription was canceled.
            }
            finally
            {
                _task = null;
                await StopAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                _disposed = true;
                
                if (!_cts.IsCancellationRequested)
                {
                    _cts.Cancel();
                }

                if (_task is not null)
                {
                    await _task;
                }

                _cts.Dispose();
            }
        }
    }
}
