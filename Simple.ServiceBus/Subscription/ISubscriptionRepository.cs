using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Subscription
{
    public interface ISubscriptionRepository
    {
        SubscriptionDescription Get<T>();
    }
}