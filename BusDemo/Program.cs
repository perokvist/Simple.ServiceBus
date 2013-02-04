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

            while (!string.IsNullOrEmpty(message))
            {
                serviceBus.Publish(new SimpleMessage
                {
                    Title = message,
                    Id = Guid.NewGuid()
                });

                Console.Write("Message:");
                message = Console.ReadLine();
            }
        }
    }
}
