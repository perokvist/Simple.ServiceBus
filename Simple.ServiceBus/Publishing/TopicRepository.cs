using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Simple.ServiceBus.Infrastructure;

namespace Simple.ServiceBus.Publishing
{
    public class TopicRepository : ITopicRepository
    {
        private readonly NamespaceManager _namespaceManager;

        public TopicRepository(NamespaceManager namespaceManager)
        {
            _namespaceManager = namespaceManager;
        }

        public async Task<TopicDescription> Get<T>()
        {
            var topicName = string.Format("Topic_{0}", typeof(T).Name);

            var existsTask = Task.Factory.FromAsync<string, bool>(
                _namespaceManager.BeginTopicExists, _namespaceManager.EndTopicExists, topicName, null);

            var createTask = Task.Factory.FromAsync<string, TopicDescription>
                (_namespaceManager.BeginCreateTopic, _namespaceManager.EndCreateTopic,
                                                    topicName, null);

            var getTask = Task.Factory.FromAsync<string, TopicDescription>(
                _namespaceManager.BeginGetTopic, _namespaceManager.EndGetTopic, topicName, null);

            if (await existsTask)
            {
                return await createTask;
            }
            return await getTask;
        }

    }
}
