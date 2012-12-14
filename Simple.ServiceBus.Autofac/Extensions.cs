using Autofac;
using Autofac.Core;
using Simple.ServiceBus.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
                .Where(x => x.GetInterfaces().Contains(typeof(IHandle)))
                .AsImplementedInterfaces();

            return new SimpleBusConfigurator(builder.Builder);
        }

        public static ISimpleBusConfigurator ListenFor<T>(this ISimpleBusConfigurator builder)
        {
            var b = builder.Builder;
            b.RegisterType<AutofacHandler<T>>()
             .As<IAutofacHandler<T>>();
            b.RegisterCallback(x => x
                .RegistrationsFor(
                    new TypedService(typeof(IServiceBus))).First()
                        .Activating += (s,e) => ((IServiceBus)e.Instance).Subscribe(e.Context.Resolve<IAutofacHandler<T>>())
                );
            
            return builder;
        }

        public static ISimpleBusConfigurator Subscribe<T>(this ISimpleBusConfigurator builder, Action<T> handler)
        {
            var b = builder.Builder;
            b.RegisterCallback(x => x
                .RegistrationsFor(
                    new TypedService(typeof(IServiceBus))).First()
                        .Activating += (s, e) => ((IServiceBus)e.Instance).Subscribe(new Handler<T>(handler))
                );

            return builder;
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
    
    public interface ISimpleBusConfigurator
    {
        ContainerBuilder Builder { get; } 
    }

    public class SimpleBusConfigurator : ISimpleBusConfigurator
    {
        public SimpleBusConfigurator(ContainerBuilder containerBuilder)
        {
            Builder = containerBuilder;
        }

        public ContainerBuilder Builder { get; private set; }
    }
}
