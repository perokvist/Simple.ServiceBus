using System;
using Messages;

namespace Simple.ServiceBus.Demo.Pub
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
