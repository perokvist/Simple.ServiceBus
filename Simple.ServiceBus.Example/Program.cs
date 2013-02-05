using System;
using Autofac;
using Messages;
using Simple.ServiceBus.Bootstrapping;
using Simple.ServiceBus.Subscription;
using Topshelf;
using Topshelf.Runtime;
using Topshelf.ServiceConfigurators;
using Simple.ServiceBus.Autofac;

namespace Simple.ServiceBus.Example
{
    class Program
    {
        static void Main()
        {
            HostFactory.Run(
                hc => hc.Service<SubscriptionConfigurationService>(Setup));
        }

        private static void Setup(ServiceConfigurator<SubscriptionConfigurationService> obj)
        {
            var cb = new ContainerBuilder()
            .RegisterServiceBus()
            .RegisterHandlers(typeof(Program).Assembly);
            var container = cb.Build();
            var resolver = new Resolver(container.ResolveNamed, container.Resolve);

            obj.ConstructUsing(
                s =>
                new SubscriptionConfigurationService(resolver,
                 map => map.ListenTo<SimpleMessage>().Using<SimpleHandler>().WithConfiguration(new SubscriptionConfiguration("Test_1"))
                    ));
            obj.WhenStarted(sv => sv.Start());
            obj.WhenStopped(sv => sv.Stop());
        }
    }

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
            Console.WriteLine("Title: {0}, Throughput:{1}", value.Title, DateTime.Now - value.DateTime);
        }
    }
}
