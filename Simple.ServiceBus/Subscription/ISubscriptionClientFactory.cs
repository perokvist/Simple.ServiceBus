using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Subscription
{
    public interface ISubscriptionClientFactory
    {
        SubscriptionClient CreateFor<T>(ISubscriptionConfiguration<T> config);
    }
}