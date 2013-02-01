using System;
using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Subscription
{
    public interface IMessageReceiver
    {
        IDisposable Receive<T>(ISubscriptionConfiguration<T> config, IObserver<T> observer);
    }
}