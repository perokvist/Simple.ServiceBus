using Microsoft.ServiceBus;
using Simple.ServiceBus.Publishing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.ServiceBus
{
    public class ServiceBusManager : IServiceBusManager
    {
        private readonly NamespaceManager _namespaceManager;
        private readonly ITopicRepository _topicRepository;

        public ServiceBusManager(NamespaceManager namespaceManager, ITopicRepository topicRepository)
        {
            _namespaceManager = namespaceManager;
            _topicRepository = topicRepository;
        }

        public void DeleteTopic<T>()
        {
            var topic = _topicRepository.Get<T>();
            _namespaceManager.DeleteTopic(topic.Path);
        }

        public void DeleteSubscription<T>(string subscriptionName)
        {
            var topic = _topicRepository.Get<T>();
            _namespaceManager.DeleteSubscription(topic.Path, subscriptionName);
        }
    }
}
