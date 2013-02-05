using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Publishing
{
    public interface ITopicClientFactory
    {
        Task<TopicClient> CreateFor<T>();
    }
}