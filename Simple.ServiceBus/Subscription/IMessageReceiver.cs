using System;
using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Subscription
{
    public interface IMessageReceiver
    {
        void Receive<T>(ISubscriptionConfiguration<T> config, Action<T> success, Action<Exception> fail);
        void Stop<T>();
    }
}