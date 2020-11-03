using System;
using System.Threading.Tasks;

namespace HotChocolate.AspNetCore.Subscriptions
{
    public class SubscriptionMock : ISubscription
    {
        public string Id { get; set; } = "abc";

        public bool IsDisposed { get; private set; }

        public event EventHandler Completed;

        public void Start()
        {
        }

        public void Complete()
        {
            Completed?.Invoke(this, EventArgs.Empty);
        }

        public async Task StopAsync() => await DisposeAsync();

        public ValueTask DisposeAsync()
        {
            IsDisposed = true;
            return default;
        }
    }
}
