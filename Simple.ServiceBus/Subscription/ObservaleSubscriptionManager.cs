using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Simple.ServiceBus.Subscription
{
    public class ObservaleSubscriptionManager<T> : INamedObservable<T>
    {
        private readonly IMessageReceiver _messageReceiver;
        private readonly IDictionary<string, IObserver<T>> _observers;
        private readonly ISubscriptionConfiguration<T> _config;

        public ObservaleSubscriptionManager(
            IMessageReceiver messageReceiver,
            ISubscriptionConfigurationRepository subscriptionConfigurationRepository)
        {
            _messageReceiver = messageReceiver;
            _observers = new ConcurrentDictionary<string, IObserver<T>>();
            _config = subscriptionConfigurationRepository.Get<T>();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return Subscribe(observer, Guid.NewGuid().ToString());
        }
        public IDisposable Subscribe(IObserver<T> observer, string subscriptionKey)
        {
            //TODO: needs to be unique for multiple application use
            if (_observers.ContainsKey(subscriptionKey))
                throw new DuplicateNameException(subscriptionKey);

            _observers[subscriptionKey] = observer;

            var stop = _messageReceiver.Receive(_config, observer);


            return new DisposableAction(() => Unhsubscribe(subscriptionKey, stop)) { Id = subscriptionKey };
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
        IDisposable Subscribe(IObserver<T> observer, string name);
    }
}