using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

#if STITCHING
namespace HotChocolate.Stitching.Transport
#else
namespace StrawberryShake.Transport.DependencyInjection
#endif
{
    public static class WebSocketConnectionPoolServiceCollectionExtensions
    {
        public static IServiceCollection AddWebSocketConnectionPool(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddSingleton<ISocketConnectionPool, WebSocketConnectionPool>();
            return services;
        }
    }
}
