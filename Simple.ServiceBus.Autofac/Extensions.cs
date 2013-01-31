using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
using Simple.ServiceBus.Infrastructure;
using Simple.ServiceBus.Subscription;

namespace Simple.ServiceBus.Autofac
{
    public static class Extensions
    {
        public static BuilderInitialized RegisterServiceBus(this ContainerBuilder builder)
        {
            builder.RegisterModule(new ServiceBusModule());
            return new BuilderInitialized(builder);
        }

        public static ISimpleBusConfigurator RegisterHandlers(this BuilderInitialized builder, Assembly assembly)
        {
            builder.Builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.GetInterfaces().Where(i => i.IsGenericType)
                .Any(contract => contract.GetGenericTypeDefinition() == typeof(IObserver<>)))
                .AsImplementedInterfaces();

            return new SimpleBusConfigurator(builder.Builder);
        }

        public static ISimpleBusConfigurator RegisterConfiguration(this ISimpleBusConfigurator builder, Assembly assembly)
        {
            builder.Builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.GetInterfaces().Contains(typeof(ISubscriptionConfiguration<>)))
                .AsImplementedInterfaces();

            return new SimpleBusConfigurator(builder.Builder);
        }


        public static IContainer Build(this ISimpleBusConfigurator builder)
        {
            return builder.Builder.Build();
        }
    }

    public class BuilderInitialized 
    {
        public BuilderInitialized(ContainerBuilder builder)
        {
            Builder = builder;
        }
        
        internal ContainerBuilder Builder { get; private set; }
    }

    public interface ISubscriptionConfigurator<T> : IBuilderAccessor
    {
        ISimpleBusConfigurator Configure(Action<ISubscriptionConfiguration<T>> action);
        ISimpleBusConfigurator WithContainerConfig();

    }

    public interface ISimpleBusConfigurator : IBuilderAccessor
    {
        ISimpleBusConfigurator Subscribe<T>(Action<T> handler);
        ISubscriptionConfigurator<T> ListenFor<T>();
    }

    public interface IBuilderAccessor
    {
        ContainerBuilder Builder { get; }
    }
}
