using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Messages;
using Simple.ServiceBus;
using Simple.ServiceBus.Autofac;
using Simple.ServiceBus.Infrastructure;

namespace BusDemoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new ContainerBuilder()
                .RegisterServiceBus()
                .RegisterHandlers(System.Reflection.Assembly.GetExecutingAssembly())
                //.ListenFor<SimpleMessage>()
                //.Subscribe<SimpleMessage>(x => Console.WriteLine(string.Format("Received (delegate) '{0}' with message id of {1}", x.Title, x.Id)))
                .Builder.Build();

            var serviceBus = container.Resolve<IServiceBus>();
            serviceBus.Subscribe(new Handler<SimpleMessage>(x => Console.WriteLine(string.Format("Received (delegate) '{0}' with message id of {1}", x.Title, x.Id))));
            var message = Console.ReadLine();
        }
    }
}
