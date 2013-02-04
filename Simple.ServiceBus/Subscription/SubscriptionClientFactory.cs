using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Subscription
{
    public class SubscriptionClientFactory : ISubscriptionClientFactory
    {
        private readonly MessagingFactory _messagingFactory;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionClientFactory(MessagingFactory messagingFactory, 
            ISubscriptionRepository subscriptionRepository)
        {
            _messagingFactory = messagingFactory;
            _subscriptionRepository = subscriptionRepository;
        }

        public SubscriptionClient CreateFor<T>(ISubscriptionConfiguration<T> config)
        {
            var subscription = _subscriptionRepository.Get<T>(config.SubscriptionName);
            var topicPath = subscription.TopicPath;
            return _messagingFactory.CreateSubscriptionClient(topicPath, subscription.Name, config.ReceiveMode);
        }
    }
}
