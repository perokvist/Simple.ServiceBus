using System;
using System.Collections.Generic;
using System.Linq;
using Simple.ServiceBus.Extensions;

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

        public IDisposable Subscribe<T>(IObserver<T> handler,SubscriptionConfiguration configuration)
        {
            var observable = GetObservable<T>();
            return observable.Subscribe(handler,configuration);
        }

        private INamedObservable<T> GetObservable<T>()
        {
            return _observables.PutIf(typeof(T), () => _observableSubscriptionManagerFactory.Create<T>());
        }
    }
}
