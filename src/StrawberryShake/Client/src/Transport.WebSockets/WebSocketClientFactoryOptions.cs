using System;
using System.Collections.Generic;
using System.Net.WebSockets;

#if STITCHING
namespace HotChocolate.Stitching.Transport
#else
namespace StrawberryShake.Transport
#endif
{
    public class WebSocketClientFactoryOptions
    {
        /// <summary>
        /// Gets a list of operations used to configure an <see cref="ClientWebSocket"/>.
        /// </summary>
        public IList<Action<WebSocketClient>> WebSocketClientActions { get; } =
            new List<Action<WebSocketClient>>();
    }
}
