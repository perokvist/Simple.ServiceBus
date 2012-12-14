using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Infrastructure
{
    public class SubscriptionManager : ISubscriptionManager
    {
        private readonly ISubscriptionClientFactory _subscriptionClientFactory;
        private readonly IMessageReceiver _messageReceiver;
        private readonly IList<Tuple<Type, IHandle>> _handlers;

        public SubscriptionManager(
            ISubscriptionClientFactory subscriptionClientFactory, 
            IMessageReceiver messageReceiver)
        {
            _subscriptionClientFactory = subscriptionClientFactory;
            _messageReceiver = messageReceiver;
            _handlers = new List<Tuple<Type, IHandle>>();
        }

        public void Subscribe<T>(IHandle<T> handler)
        {
            _handlers.Add(new Tuple<Type, IHandle>(typeof(T), handler));
            _messageReceiver.Receive<T>(_subscriptionClientFactory.CreateFor<T>(), x => _handlers
                                                                           .Where(h => h.Item1 == typeof(T))
                                                                           .Select(h => h.Item2)
                                                                           .ForEach(h => h.Handle(x)));
        }

        
    }
}
