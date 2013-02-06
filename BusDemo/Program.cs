using System.Linq;
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
        static void Main()
        {
            var container = new ContainerBuilder()
                .RegisterServiceBus()
                .RegisterHandlers(System.Reflection.Assembly.GetExecutingAssembly())
                .Build();

            var serviceBus = container.Resolve<IServiceBus>();
            var manager = container.Resolve<IServiceBusManager>();

            Console.Write("Resetting bus...");

            manager.DeleteTopic<SimpleMessage>();
            
            //manager.DeleteSubscription<SimpleMessage>("Test_1");

            Console.Write("Message: ");
            var message = Console.ReadLine();

            var tasks = Enumerable.Range(0, 1).Select(i => serviceBus.Publish(new SimpleMessage
                                            {
                                                Title = message + i,
                                                Id = Guid.NewGuid(),
                                                DateTime = DateTime.Now
                                            })).ToArray();

            Task.WaitAll(tasks);
            Console.Write("Done. Press any key to exit.");
            Console.ReadLine();

        }
    }
}
