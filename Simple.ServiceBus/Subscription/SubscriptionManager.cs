using System;
using System.Collections.Generic;
using System.Linq;

namespace Simple.ServiceBus.Subscription
{
    public class SubscriptionManager : ISubscriptionManager
    {
        private readonly IObservableSubscriptionManagerFactory _observableSubscriptionManagerFactory;
        private readonly IDictionary<Type, dynamic> _observables;

        public SubscriptionManager(IObservableSubscriptionManagerFactory observableSubscriptionManagerFactory)
        {
            _observableSubscriptionManagerFactory = observableSubscriptionManagerFactory;
            _observables = new Dictionary<Type, dynamic>();
        }

        public IDisposable Subscribe<T>(IObserver<T> handler)
        {
            var o = GetObservable<T>();
            return o.Subscribe(handler);
        }

        private IObservable<T> GetObservable<T>()
        {
            if(!_observables.ContainsKey(typeof(T)))
            {
                var o = _observableSubscriptionManagerFactory.Create<T>();
                _observables[typeof(T)] = o;
                return o;
            }
            return _observables[typeof (T)];
        }
    }
}
