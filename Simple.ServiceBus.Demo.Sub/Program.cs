using System;
using Autofac;
using Messages;
using Simple.ServiceBus.Autofac;
using Simple.ServiceBus.Bootstrapping;
using Simple.ServiceBus.Subscription;
using Topshelf;
using Topshelf.ServiceConfigurators;

namespace Simple.ServiceBus.Demo.Sub
{
    class Program
    {
        static void Main()
        {
            HostFactory.Run(
                hc => hc.Service<SubscriptionConfigurationService>(Setup));
        }

        private static void Setup(ServiceConfigurator<SubscriptionConfigurationService> serviceConfig)
        {
            var container = new ContainerBuilder()
            .RegisterServiceBus()
            .RegisterObservers(typeof(Program).Assembly)
            .Build();

            var resolver = new Resolver(container.ResolveNamed, container.Resolve);

            serviceConfig.ConstructUsing(
                s =>
                new SubscriptionConfigurationService(resolver,
                 map => map.ListenTo<SimpleMessage>().Using<SimpleHandler>().WithConfiguration(new SubscriptionConfiguration("Test_1"))
                    ));
            serviceConfig.WhenStarted(sv => sv.Start());
            serviceConfig.WhenStopped(sv => sv.Stop());
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
