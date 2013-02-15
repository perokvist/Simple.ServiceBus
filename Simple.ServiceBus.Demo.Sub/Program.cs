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
            serviceConfig.WhenStarted(service => service.Start());
            serviceConfig.WhenStopped(service => service.Stop());
        }
    }
}
