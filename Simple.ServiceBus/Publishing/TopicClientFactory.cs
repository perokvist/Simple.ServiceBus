using Microsoft.ServiceBus.Messaging;
using Simple.ServiceBus.Infrastructure;

namespace Simple.ServiceBus.Publishing
{
    public class TopicClientFactory : ITopicClientFactory
    {
        private readonly MessagingFactory _messagingFactory;
        private readonly ITopicRepository _topicRepository;

        public TopicClientFactory(MessagingFactory messagingFactory, ITopicRepository topicRepository)
        {
            _messagingFactory = messagingFactory;
            _topicRepository = topicRepository;
        }

        public TopicClient CreateFor<T>()
        {
            var topic = _topicRepository.Get<T>();
            return _messagingFactory.CreateTopicClient(topic.Path);
        }

    }
}
