using System;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Simple.ServiceBus.Extensions;
using Simple.ServiceBus.Publishing;

namespace Simple.ServiceBus.Subscription
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

        public async Task<SubscriptionDescription> Get<T>(string subscriptionName)
        {
            var topic = await _topicRepository.Get<T>();

            var exsitsTask = _namespaceManager.SubscriptionExistsAsync(topic.Path, subscriptionName, null);
            var createTask = _namespaceManager.CreateSubscriptionAsync(topic.Path, subscriptionName, null);
            var getTask = _namespaceManager.GetSubscriptionAsync(topic.Path, subscriptionName, null);

            if(await exsitsTask)
            {
                return await createTask;
            }

            return await getTask;
        }
    }
}
