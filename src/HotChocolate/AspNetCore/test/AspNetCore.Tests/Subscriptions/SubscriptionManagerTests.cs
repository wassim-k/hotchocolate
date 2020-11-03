using System;
using System.Threading.Tasks;
using Xunit;

namespace HotChocolate.AspNetCore.Subscriptions
{
    public class SubscriptionManagerTests
    {
        [Fact]
        public void Create_Instance_Connection_Is_Null()
        {
            // arrange
            // act
            void Action() => new SubscriptionManager(null!);

            // assert
            Assert.Throws<ArgumentNullException>(Action);
        }

        [Fact]
        public void Register_Subscription()
        {
            // arrange
            var connection = new SocketConnectionMock();
            var subscriptions = new SubscriptionManager(connection);
            var subscription = new SubscriptionMock();

            // act
            subscriptions.Register(subscription);

            // assert
            Assert.Collection(subscriptions,
                t => Assert.Equal(subscription, t));
        }

        [Fact]
        public void Register_Subscription_SubscriptionIsNull()
        {
            // arrange
            var connection = new SocketConnectionMock();
            var subscriptions = new SubscriptionManager(connection);
            var subscription = new SubscriptionMock();

            // act
            Action action = () => subscriptions.Register(null);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public async Task Register_Subscription_ManagerAlreadyDisposed()
        {
            // arrange
            var connection = new SocketConnectionMock();
            var subscriptions = new SubscriptionManager(connection);
            var subscription = new SubscriptionMock { Id = "abc" };

            subscriptions.Register(subscription);
            await subscriptions.DisposeAsync();

            // act
            Action action = () =>
                subscriptions.Register(new SubscriptionMock { Id = "def" });

            // assert
            Assert.Throws<ObjectDisposedException>(action);
        }

        [Fact]
        public async Task Unregister_Subscription()
        {
            // arrange
            var connection = new SocketConnectionMock();
            var subscriptions = new SubscriptionManager(connection);
            var subscription = new SubscriptionMock();
            subscriptions.Register(subscription);
            Assert.Collection(subscriptions,
                t => Assert.Equal(subscription, t));

            // act
            await subscriptions.StopSubscriptionAsync(subscription.Id);

            // assert
            Assert.Empty(subscriptions);
        }

        [Fact]
        public async Task Unregister_Subscription_SubscriptionIdIsNull()
        {
            // arrange
            var connection = new SocketConnectionMock();
            var subscriptions = new SubscriptionManager(connection);
            var subscription = new SubscriptionMock();
            subscriptions.Register(subscription);
            Assert.Collection(subscriptions,
                t => Assert.Equal(subscription, t));

            // act
            Func<Task> action = () => subscriptions.StopSubscriptionAsync(null);

            // assert
            await Assert.ThrowsAsync<ArgumentException>(action);
        }

        [Fact]
        public async Task Unregister_Subscription_ManagerAlreadyDisposed()
        {
            // arrange
            var connection = new SocketConnectionMock();
            var subscriptions = new SubscriptionManager(connection);
            var subscription = new SubscriptionMock { Id = "abc" };

            subscriptions.Register(subscription);
            await subscriptions.DisposeAsync();

            // act
            Func<Task> action = () => subscriptions.StopSubscriptionAsync("abc");

            // assert
            await Assert.ThrowsAsync<ObjectDisposedException>(action);
        }

        [Fact]
        public async Task Dispose_Subscriptions()
        {
            // arrange
            var connection = new SocketConnectionMock();
            var subscriptions = new SubscriptionManager(connection);
            var subscription_a = new SubscriptionMock();
            var subscription_b = new SubscriptionMock();

            subscriptions.Register(subscription_a);
            subscriptions.Register(subscription_b);

            // act
            await subscriptions.DisposeAsync();

            // assert
            Assert.Empty(subscriptions);
            Assert.True(subscription_a.IsDisposed);
            Assert.True(subscription_a.IsDisposed);
        }

        [Fact]
        public void Complete_Subscription()
        {
            // arrange
            var connection = new SocketConnectionMock();
            var subscriptions = new SubscriptionManager(connection);
            var subscription = new SubscriptionMock();
            subscriptions.Register(subscription);
            Assert.Collection(subscriptions,
                t => Assert.Equal(subscription, t));

            // act
            subscription.Complete();

            // assert
            Assert.Empty(subscriptions);
        }
    }
}
