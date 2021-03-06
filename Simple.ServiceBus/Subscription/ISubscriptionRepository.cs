using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Subscription
{
    public interface ISubscriptionRepository
    {
        Task<SubscriptionDescription> Get<T>(string subscriptionName);
    }
}