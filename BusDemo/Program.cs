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
        static ManualResetEvent resetEvent = new ManualResetEvent(false);

        static void Main()
        {
            Run();
            resetEvent.WaitOne(); 
        }

        private static async void Run()
        {
            var container = new ContainerBuilder()
                .RegisterServiceBus()
                .RegisterHandlers(System.Reflection.Assembly.GetExecutingAssembly())
                .Build();

            var serviceBus = container.Resolve<IServiceBus>();
            var manager = container.Resolve<IServiceBusManager>();

            Console.WriteLine("Resetting bus...");
            //await manager.DeleteTopicAsync<SimpleMessage>();
            
            Console.Write("");
            Console.Write("Message: ");
            var message = Console.ReadLine();
            Send(serviceBus, message);
        }

        private static void Send(IServiceBus serviceBus, string message)
        {
            var tasks = Enumerable.Range(0, 1).Select(i => serviceBus.Publish(new SimpleMessage
                                                                                  {
                                                                                      Title = message + i,
                                                                                      Id = Guid.NewGuid(),
                                                                                      DateTime = DateTime.Now
                                                                                  })).ToArray();


            Task.WaitAll(tasks);
            Console.WriteLine("Done. Press any key to exit.");
            Console.ReadLine();
            resetEvent.Set();
        }
    }
}
