
using System;
using Messages;

namespace Simple.ServiceBus.Demo.Sub
{
    public class SimpleHandler : IObserver<SimpleMessage>
    {
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("Error: {0}, something went pear shaped", error.Message);
        }

        public void OnNext(SimpleMessage value)
        {
            Console.WriteLine("Title: {0}, Throughput:{1}", value.Title, DateTime.Now - value.DateTime);
        }
    }
}