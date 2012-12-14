using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Infrastructure
{
    public interface ISubscriptionRepository
    {
        SubscriptionDescription Get<T>();
    }
}