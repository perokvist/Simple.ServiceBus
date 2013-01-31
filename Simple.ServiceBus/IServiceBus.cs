using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ServiceBus
{
    public interface IServiceBus
    {
        void Publish<T>(T message);
        void Subscribe<T>(IObserver<T> handler);
    }
}
