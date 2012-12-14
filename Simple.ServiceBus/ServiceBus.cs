using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.ServiceBus.Infrastructure;

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

        void IServiceBus.Subscribe<T>(IHandle<T> handler)
        {
            //TODO clear topics, unsubscribe
            _subscriptionManager.Subscribe(handler);
        }

        void IServiceBus.Publish<T>(T message)
        {
            _messageDispatcher.Publish(message);
        }
    }
}
