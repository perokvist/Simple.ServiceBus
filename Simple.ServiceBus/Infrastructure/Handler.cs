using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.ServiceBus.Infrastructure
{
    public class Handler<T> : IHandle<T>
    {
        private readonly Action<T> _action;

        public Handler(Action<T> action)
        {
            _action = action;
        }

        void IHandle<T>.Handle(T message)
        {
            Handle(message);
        }

        public void Handle(object message)
        {
            _action((T)message);
        }
    }
}
