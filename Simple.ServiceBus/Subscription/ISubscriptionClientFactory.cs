using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Subscription
{
    public interface ISubscriptionClientFactory
    {
        Task<SubscriptionClient> CreateFor<T>(SubscriptionConfiguration config);
    }
}