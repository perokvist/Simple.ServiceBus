using System;

namespace Simple.ServiceBus.Subscription
{
    public interface IConfigurated:IDisposable
    {
        SubscriptionConfiguration Config { get; set; }
    }
}