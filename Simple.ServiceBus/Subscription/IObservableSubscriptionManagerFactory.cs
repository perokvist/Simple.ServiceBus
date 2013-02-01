using System;

namespace Simple.ServiceBus.Subscription
{
    public interface IObservableSubscriptionManagerFactory
    {
        INamedObservable<T> Create<T>();
    }
}