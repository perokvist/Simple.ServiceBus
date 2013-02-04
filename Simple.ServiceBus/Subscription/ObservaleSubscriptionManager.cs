using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace Simple.ServiceBus.Subscription
{
    public class ObservaleSubscriptionManager<T> : INamedObservable<T>
    {
        private readonly IMessageReceiver _messageReceiver;
        private readonly IDictionary<string, IObserver<T>> _observers;
        
        public ObservaleSubscriptionManager(
            IMessageReceiver messageReceiver)
        {
            _messageReceiver = messageReceiver;
            _observers = new ConcurrentDictionary<string, IObserver<T>>();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return Subscribe(observer, new SubscriptionConfiguration { SubscriptionName = Guid.NewGuid().ToString() });
        }

        public IConfigurated Subscribe(IObserver<T> observer, SubscriptionConfiguration config)
        {
            //TODO: needs to be unique for multiple application use
            if (_observers.ContainsKey(config.SubscriptionName))
                throw new DuplicateNameException(config.SubscriptionName);
            _observers[config.SubscriptionName] = observer;
            
            var stop = _messageReceiver.Receive(config, observer);

            return new DisposableAction(() => Unhsubscribe(config.SubscriptionName, stop)) { Config=config };
        }

        private void Unhsubscribe(string subscriptionKey, IDisposable stoppable)
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

    public interface INamedObservable<out T> : IObservable<T>
    {
        IConfigurated Subscribe(IObserver<T> observer, SubscriptionConfiguration config);
    }
}