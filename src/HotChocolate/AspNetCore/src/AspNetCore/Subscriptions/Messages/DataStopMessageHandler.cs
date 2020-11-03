using System.Threading;
using System.Threading.Tasks;

namespace HotChocolate.AspNetCore.Subscriptions.Messages
{
    public sealed class DataStopMessageHandler
        : MessageHandler<DataStopMessage>
    {
        protected override async Task HandleAsync(
            ISocketConnection connection,
            DataStopMessage message,
            CancellationToken cancellationToken)
        {
            await connection.Subscriptions.StopSubscriptionAsync(message.Id);
        }
    }
}
