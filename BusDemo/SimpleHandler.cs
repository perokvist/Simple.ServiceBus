using Messages;
using Simple.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusDemo
{
    public class SimpleHandler : IHandle<SimpleMessage>
    {
        public void Handle(SimpleMessage message)
        {
            Console.WriteLine(string.Format("Received (autofac) '{0}' with message id of {1}", message.Title, message.Id));
        }

        public void Handle(object message)
        {
            this.Handle((SimpleMessage) message);
        }
    }
}
