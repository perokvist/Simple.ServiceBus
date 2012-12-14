using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ServiceBus
{
    public interface IHandle<in T> : IHandle
    {
        void Handle(T message);
    }

    public interface IHandle
    {
        void Handle(object message);
    }
}
