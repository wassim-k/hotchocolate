using System;
using System.Threading.Tasks;

namespace HotChocolate.AspNetCore.Subscriptions
{
    public interface ISubscription : IAsyncDisposable
    {
        event EventHandler? Completed;

        string Id { get; }

        void Start();

        Task StopAsync();
    }
}
