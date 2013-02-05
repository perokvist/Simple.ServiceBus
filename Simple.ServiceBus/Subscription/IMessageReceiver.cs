using System;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Subscription
{
    public interface IMessageReceiver
    {
        Task<IDisposable> Receive<T>(SubscriptionConfiguration config, IObserver<T> observer);
    }
}