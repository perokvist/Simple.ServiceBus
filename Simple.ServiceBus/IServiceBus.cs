using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ServiceBus.Subscription;
using System.Threading.Tasks;

namespace Simple.ServiceBus
{
    public interface IServiceBus
    {
        Task PublishAsync<T>(T message);
        IDisposable Subscribe<T>(IObserver<T> handler,SubscriptionConfiguration configuration=null);
    }
}
