using System;

namespace Simple.ServiceBus.Subscription
{
    public interface IObservableSubscriptionManagerFactory
    {
        IObservable<T> Create<T>();
    }
}