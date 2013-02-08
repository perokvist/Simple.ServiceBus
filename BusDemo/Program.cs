using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Messages;
using Microsoft.ServiceBus.Messaging;
using Simple.ServiceBus;
using System;
using Simple.ServiceBus.Autofac;

namespace BusDemo
{
    class Program
    {
        static readonly ManualResetEvent ResetEvent = new ManualResetEvent(false);

        static void Main()
        {
            Run();
            ResetEvent.WaitOne();
        }

        private static async void Run()
        {
            var container = new ContainerBuilder()
                .RegisterServiceBus()
                .RegisterObservers(System.Reflection.Assembly.GetExecutingAssembly())
                .Build();

            var serviceBus = container.Resolve<IServiceBus>();
            var manager = container.Resolve<IServiceBusManager>();

            Console.WriteLine("Resetting demo topic...");
            await manager.DeleteTopicAsync<SimpleMessage>();
            Console.Write("Message: ");
            var message = Console.ReadLine();
            Console.Write("Sending messages...");
            await Send(serviceBus, message);
            Console.WriteLine("Done. Press any key to exit.");
            Console.ReadLine();
            ResetEvent.Set();
        }

        private static Task Send(IServiceBus serviceBus, string message)
        {
            var tasks = Enumerable.Range(0, 25)
                .Select(i => serviceBus.Publish(
                    new SimpleMessage
                    {
                        Title = message + i,
                        Id = Guid.NewGuid(),
                        DateTime = DateTime.Now
                    }))
                 .ToArray();

            return Task.WhenAll(tasks);
           
        }
    }
}
