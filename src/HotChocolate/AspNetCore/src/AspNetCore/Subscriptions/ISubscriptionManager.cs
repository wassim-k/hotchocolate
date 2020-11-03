using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotChocolate.AspNetCore.Subscriptions
{
    public interface ISubscriptionManager
        : IEnumerable<ISubscription>
        , IAsyncDisposable
    {
        void Register(ISubscription subscription);

        Task StopSubscriptionAsync(string subscriptionId);
    }
}
