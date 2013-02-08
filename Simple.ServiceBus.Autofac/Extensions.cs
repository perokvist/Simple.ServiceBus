using Autofac;
using System;
using System.Linq;
using System.Reflection;

namespace Simple.ServiceBus.Autofac
{
    public static class Extensions
    {
        public static BuilderInitialized RegisterServiceBus(this ContainerBuilder builder)
        {
            builder.RegisterModule(new ServiceBusModule());
            return new BuilderInitialized(builder);
        }

        public static ContainerBuilder RegisterObservers(this BuilderInitialized builder, Assembly assembly)
        {
            builder.Builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.GetInterfaces().Where(i => i.IsGenericType)
                .Any(contract => contract.GetGenericTypeDefinition() == typeof(IObserver<>)))
                .AsSelf()
                .AsImplementedInterfaces();
            return builder.Builder;
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

}
