using System;
using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Subscription
{
    public class SubscriptionConfiguration
    {
        public SubscriptionConfiguration(string name)
        {
            SubscriptionName = name;
            ConfigAction = c => { };
        }

        public ReceiveMode ReceiveMode { get; set; }

        public TimeSpan TimeSpan { get; set; }

        public string SubscriptionName { get; private set; }

        /// <summary>
        /// Configure the client
        /// </summary>
        /// <remarks>Temporary full access to the client</remarks>
        public Action<SubscriptionClient> ConfigAction { get; set; }
    }
}
