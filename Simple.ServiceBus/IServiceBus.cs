using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ServiceBus.Subscription;

namespace Simple.ServiceBus
{
    public interface IServiceBus
    {
        void Publish<T>(T message);
        IDisposable Subscribe<T>(IObserver<T> handler,SubscriptionConfiguration configuration=null);
    }
}
