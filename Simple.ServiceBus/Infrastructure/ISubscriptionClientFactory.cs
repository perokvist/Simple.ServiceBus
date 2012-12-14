using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Infrastructure
{
    public interface ISubscriptionClientFactory
    {
        SubscriptionClient CreateFor<T>();
    }
}