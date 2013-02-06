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

        public async Task<TopicClient> CreateFor<T>()
        {
            var topic = await _topicRepository.Get<T>();
                return _messagingFactory.CreateTopicClient(topic.Path);

                //.ContinueWith(x => _messagingFactory.CreateTopicClient(x.Result.Path));
        }

    }
}
