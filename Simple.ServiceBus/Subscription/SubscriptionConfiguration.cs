using System;
using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Subscription
{
    public class SubscriptionConfiguration
    {
        public ReceiveMode ReceiveMode { get; set; }

        public TimeSpan TimeSpan { get; set; }

        public string SubscriptionName { get; set; }
    }
}
