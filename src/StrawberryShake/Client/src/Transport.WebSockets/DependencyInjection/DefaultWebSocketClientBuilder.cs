using Microsoft.Extensions.DependencyInjection;

#if STITCHING
namespace HotChocolate.Stitching.Transport
#else
namespace StrawberryShake.Transport.DependencyInjection
#endif
{
    internal class DefaultWebSocketClientBuilder
        : IWebSocketClientBuilder
    {
        public DefaultWebSocketClientBuilder(IServiceCollection services, string name)
        {
            Services = services;
            Name = name;
        }

        public string Name { get; }

        public IServiceCollection Services { get; }
    }
}
