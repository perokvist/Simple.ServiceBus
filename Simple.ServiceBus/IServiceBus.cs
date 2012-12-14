using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ServiceBus
{
    public interface IServiceBus
    {
        void Subscribe<T>(IHandle<T> handler);
        void Publish<T>(T message);
    }
}
