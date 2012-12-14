using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;

namespace Simple.ServiceBus.Infrastructure
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly NamespaceManager _namespaceManager;
        private readonly ITopicRepository _topicRepository;

        public SubscriptionRepository(NamespaceManager namespaceManager, ITopicRepository topicRepository)
        {
            _namespaceManager = namespaceManager;
            _topicRepository = topicRepository;
        }

        public SubscriptionDescription Get<T>()
        {
            var subscriptionName = string.Format("Subscription_{0}_{1}", typeof(T).Name, Guid.NewGuid().ToString().Substring(0,10));
            var topic = _topicRepository.Get<T>();

            SubscriptionDescription subscription;

            if (!_namespaceManager.SubscriptionExists(topic.Path, subscriptionName))
                subscription = _namespaceManager.CreateSubscription(topic.Path, subscriptionName);
            else
                subscription = _namespaceManager.GetSubscription(topic.Path, subscriptionName);

            return subscription;
        }
    }
}
