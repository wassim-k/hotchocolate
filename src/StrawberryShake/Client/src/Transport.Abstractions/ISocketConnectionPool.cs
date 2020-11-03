using System;
using System.Threading;
using System.Threading.Tasks;

#if STITCHING
namespace HotChocolate.Stitching.Transport
#else
namespace StrawberryShake.Transport
#endif
{
    public interface ISocketConnectionPool
        : IDisposable
    {
        Task<ISocketConnection> RentAsync(
            string name,
            CancellationToken cancellationToken = default);

        Task ReturnAsync(
            ISocketConnection connection,
            CancellationToken cancellationToken = default);
    }
}
