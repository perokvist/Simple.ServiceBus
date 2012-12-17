using System;
using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Subscription
{
    public interface ISubscriptionConfiguration<T> : ISubscriptionReceiveConfiguration<T>
    {

    }

    public interface ISubscriptionReceiveConfiguration<T>
    {
        TimeSpan TimeSpan { get; set; }
        ReceiveMode ReceiveMode { get; set; }
    }

    public class SubscriptionConfiguration<T> : ISubscriptionConfiguration<T>
    {
        public ReceiveMode ReceiveMode { get; set; }

        public TimeSpan TimeSpan { get; set; }
    }
}
