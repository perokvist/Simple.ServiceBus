using System;
using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Subscription
{
    public interface IMessageReceiver
    {
        void Receive<T>(SubscriptionClient subscriptionClient, ISubscriptionReceiveConfiguration<T> config, Action<T> action);
    }
}