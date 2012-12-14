using Autofac;
using Messages;
using Simple.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.ServiceBus.Autofac;

namespace BusDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new ContainerBuilder()
                .RegisterServiceBus()
                .RegisterHandlers(System.Reflection.Assembly.GetExecutingAssembly())
                .ListenFor<SimpleMessage>()
                .Subscribe<SimpleMessage>(x => Console.WriteLine(string.Format("Received (delegate) '{0}' with message id of {1}", x.Title, x.Id)))
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
