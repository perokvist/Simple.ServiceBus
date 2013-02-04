using System;

namespace Simple.ServiceBus.Subscription
{
    public class ObservableSubscriptionManagerFactory : IObservableSubscriptionManagerFactory
    {
        private readonly IMessageReceiver _messageReceiver;
        

        public ObservableSubscriptionManagerFactory(IMessageReceiver messageReceiver)
        {
            _messageReceiver = messageReceiver;
            
        }

        public INamedObservable<T> Create<T>()
    {
        return new ObservaleSubscriptionManager<T>(_messageReceiver);
    }
    }
}