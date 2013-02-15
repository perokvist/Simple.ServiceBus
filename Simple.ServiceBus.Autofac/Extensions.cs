using Autofac;
using System;
using System.Linq;
using System.Reflection;

namespace Simple.ServiceBus.Autofac
{
    public static class Extensions
    {
        public static ContainerBuilder RegisterServiceBus(this ContainerBuilder builder)
        {
            builder.RegisterModule(new ServiceBusModule());
            return builder;
        }

        public static ContainerBuilder RegisterObservers(this ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.GetInterfaces().Where(i => i.IsGenericType)
                .Any(contract => contract.GetGenericTypeDefinition() == typeof(IObserver<>)))
                .AsSelf()
                .AsImplementedInterfaces();
            return builder;
        }
    }
    
}
