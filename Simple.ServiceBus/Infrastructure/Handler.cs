using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.ServiceBus.Infrastructure
{
    public class Handler<T> : IObserver<T>
    {
        private readonly Action<T> _action;

        public Handler(Action<T> action)
        {
            _action = action;
        }
        
        public void OnCompleted()
        {

        }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnNext(T value)
        {
            _action(value);
        }
    }
}
