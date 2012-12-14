using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Infrastructure
{
    public class TopicClientFactory : ITopicClientFactory
    {
        private readonly MessagingFactory _messagingFactory;

        public TopicClientFactory(MessagingFactory messagingFactory)
        {
            _messagingFactory = messagingFactory;
        }

        public TopicClient CreateFor<T>()
        {
            var topicName = string.Format("Topic_{0}", typeof(T).Name);
            return _messagingFactory.CreateTopicClient(topicName);
        }

    }
}
