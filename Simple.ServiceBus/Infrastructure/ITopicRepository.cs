using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Infrastructure
{
    public interface ITopicRepository
    {
        TopicDescription Get<T>();
    }
}