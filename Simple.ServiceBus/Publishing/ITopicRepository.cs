using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Publishing
{
    public interface ITopicRepository
    {
        TopicDescription Get<T>();
    }
}