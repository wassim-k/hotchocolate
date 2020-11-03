using System;
using System.Threading;
using System.Threading.Tasks;

namespace HotChocolate.AspNetCore.Subscriptions
{
    public interface ISocketSession : IAsyncDisposable
    {
        Task HandleAsync(CancellationToken cancellationToken);
    }
}
