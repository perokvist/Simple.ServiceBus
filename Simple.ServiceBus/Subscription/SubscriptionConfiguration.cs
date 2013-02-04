using System;
using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Subscription
{
    public class SubscriptionConfiguration
    {
        public SubscriptionConfiguration(string name)
        {
            SubscriptionName = name;
        }

        public ReceiveMode ReceiveMode { get; set; }

        public TimeSpan TimeSpan { get; set; }

        public string SubscriptionName { get; private set; }
    }
}
