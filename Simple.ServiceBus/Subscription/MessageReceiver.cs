using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Simple.ServiceBus.Extensions;

namespace Simple.ServiceBus.Subscription
{
    public class MessageReceiver : IMessageReceiver
    {
        private readonly ISubscriptionClientFactory _subscriptionClientFactory;
        private readonly ISubscriptionConfigurationRepository _configurationRepository;

        public MessageReceiver(ISubscriptionClientFactory subscriptionClientFactory, ISubscriptionConfigurationRepository configurationRepository)
        {
            _subscriptionClientFactory = subscriptionClientFactory;
            _configurationRepository = configurationRepository;
        }

        public IDisposable Receive<T>(ISubscriptionConfiguration<T> config, IObserver<T> observer)
        {
            //NOTE config is not used yet
            var client = _subscriptionClientFactory.CreateFor(_configurationRepository.Get<T>());
            Receive<T>(client, observer.OnNext, observer.OnError);

            return new DisposableAction(() => Stop<T>(client));
        }

        private async static void Receive<T>(SubscriptionClient client, Action<T> action, Action<Exception> fail)
        {
            var task = Task.Factory.FromAsync<BrokeredMessage>(client.BeginReceive, client.EndReceive, action);
            var bm = await task;
            try
            {
                HandleMessage(bm, client.Mode, action);
            }
            catch (Exception e)
            {
                fail(e);
            }
            finally
            {
                if (!client.IsClosed)
                    Receive(client, action, fail);
            }
        }

        private static Task Stop<T>(SubscriptionClient client)
        {
            return Task.Factory.FromAsync(client.BeginClose, client.EndClose, null);
        }
        
        private static void HandleMessage<T>(BrokeredMessage message, ReceiveMode mode, Action<T> action)
        {
            if (message == null) return;
            var messageData = message.GetBody<T>();
            action(messageData);
            if (mode == ReceiveMode.PeekLock)
                message.Complete();
        }

    }
}
