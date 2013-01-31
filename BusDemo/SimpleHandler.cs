using Messages;
using Simple.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusDemo
{
    public class SimpleHandler : IObserver<SimpleMessage>
    {
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(SimpleMessage value)
        {
            Console.WriteLine(string.Format("Received (autofac) '{0}' with message id of {1}", value.Title, value.Id));
        }
    }
}
