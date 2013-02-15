using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.ServiceBus.Infrastructure;
using Simple.ServiceBus.Publishing;
using Simple.ServiceBus.Subscription;

namespace Simple.ServiceBus
{
    class ServiceBus : IServiceBus
    {
        private readonly ISubscriptionManager _subscriptionManager;
        private readonly IMessageDispatcher _messageDispatcher;

        public ServiceBus(ISubscriptionManager subscriptionManager, IMessageDispatcher messageDispatcher)
        {
            _subscriptionManager = subscriptionManager;
            _messageDispatcher = messageDispatcher;
        }

        IDisposable IServiceBus.Subscribe<T>(IObserver<T> handler, SubscriptionConfiguration configuration)
        {
            configuration = configuration ?? new SubscriptionConfiguration(Guid.NewGuid().ToString());
            return _subscriptionManager.Subscribe(handler, configuration);
        }

        Task IServiceBus.PublishAsync<T>(T message)
        {
            return _messageDispatcher.Publish(message);
        }
    }
}
