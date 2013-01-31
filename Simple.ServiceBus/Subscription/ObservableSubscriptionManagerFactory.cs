using System;

namespace Simple.ServiceBus.Subscription
{
    public class ObservableSubscriptionManagerFactory : IObservableSubscriptionManagerFactory
    {
        private readonly IMessageReceiver _messageReceiver;
        private readonly ISubscriptionConfigurationRepository _configurationRepository;

        public ObservableSubscriptionManagerFactory(
            IMessageReceiver messageReceiver,
            ISubscriptionConfigurationRepository configurationRepository
            )
        {
            _messageReceiver = messageReceiver;
            _configurationRepository = configurationRepository;
        }

        public IObservable<T> Create<T>()
        {
            return new ObservaleSubscriptionManager<T>(_messageReceiver, _configurationRepository);
        }
    }
}