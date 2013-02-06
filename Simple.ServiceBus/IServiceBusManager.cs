using System.Threading.Tasks;

namespace Simple.ServiceBus
{
    public interface IServiceBusManager
    {
        Task DeleteTopicAsync<T>();
        Task DeleteSubscriptionAsync<T>(string subscriptionName);
    }
}