using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Simple.ServiceBus.Subscription
{
    public class ObservaleSubscriptionManager<T> : IObservable<T>
    {
        private readonly IMessageReceiver _messageReceiver;
        private readonly IDictionary<Guid, IObserver<T>> _observers;
        private readonly ISubscriptionConfiguration<T> _config;

        public ObservaleSubscriptionManager(
            IMessageReceiver messageReceiver,
            ISubscriptionConfigurationRepository subscriptionConfigurationRepository)
        {
            _messageReceiver = messageReceiver;
            _observers = new ConcurrentDictionary<Guid, IObserver<T>>();
            _config = subscriptionConfigurationRepository.Get<T>();
        }
        
        public IDisposable Subscribe(IObserver<T> observer)
        {
            var subscriptionKey = Guid.NewGuid();
            _observers[subscriptionKey] = observer;

            var stop=_messageReceiver.Receive(_config, observer);


            return new DisposableAction(() => Unhsubscribe(subscriptionKey,stop));
        }


        private void Unhsubscribe(Guid subscriptionKey,IDisposable stoppable)
        {
            var o = _observers[subscriptionKey];
            _observers.Remove(subscriptionKey);
            o.OnCompleted();
            if (!_observers.Any())
            {
                stoppable.Dispose();
            }
        }

        
    }
}