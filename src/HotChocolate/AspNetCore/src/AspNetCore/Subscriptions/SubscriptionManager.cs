using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolate.AspNetCore.Subscriptions
{
    public sealed class SubscriptionManager : ISubscriptionManager
    {
        private readonly ConcurrentDictionary<string, ISubscription> _subs =
            new ConcurrentDictionary<string, ISubscription>();
        private readonly ISocketConnection _connection;
        private bool _disposed;

        public SubscriptionManager(ISocketConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public void Register(ISubscription subscription)
        {
            if (subscription == null)
            {
                throw new ArgumentNullException(nameof(subscription));
            }

            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SubscriptionManager));
            }

            if (_subs.TryAdd(subscription.Id, subscription))
            {
                subscription.Start();
                subscription.Completed += (sender, eventArgs) =>
                    _subs.TryRemove(subscription.Id, out _);
            }
        }

        public async Task StopSubscriptionAsync(string subscriptionId)
        {
            if (string.IsNullOrEmpty(subscriptionId))
            {
                throw new ArgumentException(
                    $"'{nameof(subscriptionId)}' cannot be null or empty.",
                    nameof(subscriptionId));
            }

            if (_subs.TryRemove(subscriptionId, out ISubscription? subscription))
            {
                await subscription.StopAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                if (_subs.Count > 0)
                {
                    ISubscription?[] subs = _subs.Values.ToArray();
                    _subs.Clear();

                    for (int i = 0; i < subs.Length; i++)
                    {
                        if (subs[i] is { } s)
                        {
                            await s.StopAsync();
                        }
                        subs[i] = null;
                    }
                }
                _disposed = true;
            }
        }

        public IEnumerator<ISubscription> GetEnumerator()
        {
            return _subs.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
