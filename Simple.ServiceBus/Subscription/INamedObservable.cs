using System;
using System.Threading.Tasks;

namespace Simple.ServiceBus.Subscription
{
    public interface INamedObservable<out T> : IObservable<T>
    {
        IConfigurated Subscribe(IObserver<T> observer, SubscriptionConfiguration config);
    }
}