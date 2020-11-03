using System.Threading.Tasks;

#if STITCHING
namespace HotChocolate.Stitching.Transport
#else
namespace StrawberryShake.Transport
#endif
{
    public interface ISocketConnectionInterceptor
    {
        Task OnConnectAsync(ISocketConnection connection);

        Task OnDisconnectAsync(ISocketConnection connection);
    }
}
