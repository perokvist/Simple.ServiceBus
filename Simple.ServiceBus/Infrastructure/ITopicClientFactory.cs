using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Infrastructure
{
    public interface ITopicClientFactory
    {
        TopicClient CreateFor<T>();
    }
}