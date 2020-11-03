using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

#if STITCHING
namespace HotChocolate.Stitching.Transport
#else
namespace StrawberryShake.Transport
#endif
{
    public interface IWebSocketClient
        : IDisposable
    {
        Uri? Uri { get; }

        ClientWebSocket Socket { get; }

        Task ConnectAsync(Uri? uri = default , CancellationToken cancellationToken = default);
    }
}
