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

        IDisposable IServiceBus.Subscribe<T>(IObserver<T> handler)
        {
            //TODO clear topics
            return _subscriptionManager.Subscribe(handler);
        }

        void IServiceBus.Publish<T>(T message)
        {
            _messageDispatcher.Publish(message);
        }
    }
}
