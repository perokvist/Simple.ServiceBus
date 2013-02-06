using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Simple.ServiceBus.Extensions;
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

            var existsTask = _namespaceManager.TopicExistsAsync(topicName, null);
            var createTask = _namespaceManager.CreateTopicAsync(topicName, null);
            var getTask = _namespaceManager.GetTopicAsync(topicName, null);
            
            if (await existsTask)
            {
                return await getTask;
            }
            return await createTask;
        }

    }
}
