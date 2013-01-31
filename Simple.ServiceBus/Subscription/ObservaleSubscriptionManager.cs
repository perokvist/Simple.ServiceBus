using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Simple.ServiceBus.Subscription
{
    public class ObservaleSubscriptionManager<T> : IObservable<T>
    {
        private readonly IMessageReceiver _messageReceiver;
        private readonly ISubscriptionConfigurationRepository _subscriptionConfigurationRepository;
        private readonly IDictionary<Guid, IObserver<T>> _observers;

        public ObservaleSubscriptionManager(
            IMessageReceiver messageReceiver,
            ISubscriptionConfigurationRepository subscriptionConfigurationRepository)
        {
            _messageReceiver = messageReceiver;
            _subscriptionConfigurationRepository = subscriptionConfigurationRepository;
            _observers = new ConcurrentDictionary<Guid, IObserver<T>>();
        }
        
        public IDisposable Subscribe(IObserver<T> observer)
        {
            SetupSubscription();
            
            var subscriptionKey = Guid.NewGuid();
            _observers[subscriptionKey] = observer;

            return new DisposableAction(() => Unhsubscribe(subscriptionKey));
        }

        private void SetupSubscription()
        {
            if (_observers.Any()) return;
            var config = _subscriptionConfigurationRepository.Get<T>();
            _messageReceiver.Receive(config, message => _observers.Values.ForEach(o => o.OnNext(message)), ex => _observers.Values.ForEach(o => o.OnError(ex)));
        }

        private void Unhsubscribe(Guid subscriptionKey)
        {
            var o = _observers[subscriptionKey];
            _observers.Remove(subscriptionKey);
            o.OnCompleted();
            if (!_observers.Any())
            {
                _messageReceiver.Stop<T>();
            }
        }

        internal class DisposableAction : IDisposable
        {
            private readonly Action _action;

            public DisposableAction(Action action)
            {
                _action = action;
            }

            public void Dispose()
            {
                _action();
            }
        }
        
    }


}