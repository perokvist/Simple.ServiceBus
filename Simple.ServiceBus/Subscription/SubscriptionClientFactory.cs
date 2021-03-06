using System.Threading.Tasks;
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

        public async Task<SubscriptionClient> CreateFor<T>(SubscriptionConfiguration config)
        {
            var subscription = await _subscriptionRepository.Get<T>(config.SubscriptionName);
            var topicPath = subscription.TopicPath;
            //TODO async
            return _messagingFactory.CreateSubscriptionClient(topicPath, subscription.Name, config.ReceiveMode);
        }
    }
}
