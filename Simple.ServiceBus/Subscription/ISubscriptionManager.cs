using System;

namespace Simple.ServiceBus.Subscription
{
    public interface ISubscriptionManager
    {
        IDisposable Subscribe<T>(IObserver<T> handler);
    }
}