using Autofac;
using System;
using Simple.ServiceBus.Infrastructure;
using Simple.ServiceBus.Subscription;

namespace Simple.ServiceBus.Autofac
{
    public class SubscriptionConfigurator<T> : ISubscriptionConfigurator<T>
    {
        private readonly ISimpleBusConfigurator _configurator;

        public SubscriptionConfigurator(ISimpleBusConfigurator configurator)
        {
            _configurator = configurator;
        }

        public ISimpleBusConfigurator Configure(Action<SubscriptionConfiguration> action)
        {
            var config = new SubscriptionConfiguration();
            action(config);
            Builder.RegisterInstance(config)
                .AsImplementedInterfaces()
                .SingleInstance();
            
            return _configurator;
        }

        public ISimpleBusConfigurator WithContainerConfig()
        {
            return _configurator;
        }

        public ContainerBuilder Builder
        {
            get { return _configurator.Builder; }
        }
    }
}