using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Publishing
{
    public interface ITopicClientFactory
    {
        TopicClient CreateFor<T>();
    }
}