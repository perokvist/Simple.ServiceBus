using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Publishing
{
    public interface ITopicRepository
    {
        Task<TopicDescription> Get<T>();
    }
}