using System;
using System.Collections.Generic;
using System.Linq;

namespace Simple.ServiceBus.Subscription
{
    public class SubscriptionManager : ISubscriptionManager
    {
        private readonly ISubscriptionClientFactory _subscriptionClientFactory;
        private readonly IMessageReceiver _messageReceiver;
        private readonly ISubscriptionConfigurationRepository _subscriptionConfigurationRepository;
        private readonly IList<Tuple<Type, IHandle>> _handlers;

        public SubscriptionManager(
            ISubscriptionClientFactory subscriptionClientFactory, 
            IMessageReceiver messageReceiver,
            ISubscriptionConfigurationRepository subscriptionConfigurationRepository)
        {
            _subscriptionClientFactory = subscriptionClientFactory;
            _messageReceiver = messageReceiver;
            _subscriptionConfigurationRepository = subscriptionConfigurationRepository;
            _handlers = new List<Tuple<Type, IHandle>>();
        }

        public void Subscribe<T>(IHandle<T> handler)
        {
            if (!SubscriptionSetupFor<T>())
            {
                var config = _subscriptionConfigurationRepository.Get<T>();
                SetupSubscription(config);
            }
            _handlers.Add(new Tuple<Type, IHandle>(typeof(T), handler));
        }

        private void SetupSubscription<T>(ISubscriptionConfiguration<T> config)
        {
            _messageReceiver.Receive(_subscriptionClientFactory.CreateFor(config), config, x => _handlers
                                                                                                    .Where(
                                                                                                        h =>
                                                                                                        h.Item1 == typeof (T))
                                                                                                    .Select(h => h.Item2)
                                                                                                    .ForEach(h => h.Handle(x)));
        }

        private bool SubscriptionSetupFor<T>()
        {
            return _handlers.Any(x => x.Item1 == typeof (T));
        }
        
    }
}
