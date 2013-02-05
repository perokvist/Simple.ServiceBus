using System.Threading.Tasks;
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

        public Task<TopicClient> CreateFor<T>()
        {
            return _topicRepository.Get<T>()
                .ContinueWith(x => _messagingFactory.CreateTopicClient(x.Result.Path));
        }

    }
}
