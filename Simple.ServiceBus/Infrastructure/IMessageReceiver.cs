using System;
using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Infrastructure
{
    public interface IMessageReceiver
    {
        void Receive<T>(SubscriptionClient subscriptionClient, Action<T> action);
    }
}