using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Subscription
{
    public class MessageReceiver : IMessageReceiver
    {
        private readonly ISubscriptionClientFactory _subscriptionClientFactory;
        private readonly IDictionary<Type, SubscriptionClient> _clients;

        public MessageReceiver(ISubscriptionClientFactory subscriptionClientFactory)
        {
            _subscriptionClientFactory = subscriptionClientFactory;
            _clients = new ConcurrentDictionary<Type, SubscriptionClient>();
        }

        public async void Receive<T>(ISubscriptionConfiguration<T> config, Action<T> action, Action<Exception> fail)
        {
            var subscriptionClient = GetClient(config);
            var task = Task.Factory.FromAsync<BrokeredMessage>(subscriptionClient.BeginReceive, subscriptionClient.EndReceive, action);
            var bm = await task;
            try
            {
                HandleMessage(bm, subscriptionClient.Mode, action);
            }
            catch (Exception e)
            {
                fail(e);
            }
            finally
            {
                if (!subscriptionClient.IsClosed)
                    Receive(config, action, fail);
            }
        }

        public void Stop<T>()
        {
            _clients[typeof(T)].Close();
        }

        private SubscriptionClient GetClient<T>(ISubscriptionConfiguration<T> config)
        {
            if (!_clients.ContainsKey(typeof(T)))
            {
                _clients[typeof(T)] = _subscriptionClientFactory.CreateFor<T>(config);
            }
            return _clients[typeof(T)];

        }

        private Action<ReceiveMode, BrokeredMessage> ModeHandler<T>(Action<T> action)
        {
            if (message == null) return;
            var messageData = message.GetBody<T>();
            action(messageData);
            if (mode == ReceiveMode.PeekLock)
                message.Complete();
        }
    }
}
