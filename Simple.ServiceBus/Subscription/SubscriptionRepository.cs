using System;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
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

            SubscriptionDescription subscription;

            var exsitsTask = Task.Factory.FromAsync<string, string, bool>(
                    _namespaceManager.BeginSubscriptionExists, _namespaceManager.EndSubscriptionExists, topic.Path, subscriptionName, null);

            var createTask = Task.Factory.FromAsync<string, string, SubscriptionDescription>(
                    _namespaceManager.BeginCreateSubscription, _namespaceManager.EndCreateSubscription, topic.Path, subscriptionName, null);

            var getTask = Task.Factory.FromAsync<string, string, SubscriptionDescription>(
                    _namespaceManager.BeginGetSubscription, _namespaceManager.EndGetSubscription, topic.Path, subscriptionName, null);

            if(await exsitsTask)
            {
                return await createTask;
            }else
            {
                return await getTask;
            }
        }
    }
}
