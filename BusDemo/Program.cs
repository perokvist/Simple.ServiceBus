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

            Console.Write("Message: ");
            var message = Console.ReadLine();


            Enumerable.Range(0, 10000).AsParallel().ForAll(
                i => serviceBus.Publish(new SimpleMessage
                                            {
                                                Title = message + i,
                                                Id = Guid.NewGuid(),
                                                DateTime = DateTime.Now
                                            }));

        }
    }
}
