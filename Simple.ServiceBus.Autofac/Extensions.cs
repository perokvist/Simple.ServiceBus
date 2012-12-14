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

        public static IHandlersRegistered RegisterHandlers(this BuilderInitialized builder, Assembly assembly)
        {
            builder.Builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.GetInterfaces().Contains(typeof(IHandle)))
                .AsImplementedInterfaces();

            return new HandlersRegistered(builder.Builder);
        }

        public static ContainerBuilder ListenFor<T>(this IHandlersRegistered builder)
        {
            var b = builder.Builder;
            b.RegisterType<AutofacHandler<T>>()
             .As<IAutofacHandler<T>>();
            b.RegisterCallback(x => x
                .RegistrationsFor(
                    new TypedService(typeof(IServiceBus))).First()
                        .Activating += (s,e) => ((IServiceBus)e.Instance).Subscribe(e.Context.Resolve<IAutofacHandler<T>>())
                );
            
            return b;
        }

        public static ContainerBuilder Subscribe<T>(this ContainerBuilder builder, Action<T> handler)
        {
            var b = builder;
            b.RegisterCallback(x => x
                .RegistrationsFor(
                    new TypedService(typeof(IServiceBus))).First()
                        .Activating += (s, e) => ((IServiceBus)e.Instance).Subscribe(new Handler<T>(handler))
                );

            return builder;
        }
    }

    public interface IHandlersRegistered
    {
        ContainerBuilder Builder { get; }
    }

    public class HandlersRegistered : IHandlersRegistered
    {

        public HandlersRegistered(ContainerBuilder builder)
        {
            Builder = builder;
        }

        public ContainerBuilder Builder { get; private set; }

    }

    public class BuilderInitialized : IBuilderInitialized
    {
        public BuilderInitialized(ContainerBuilder builder)
        {
            Builder = builder;
        }

        internal ContainerBuilder Builder { get; private set; }
    }

    public interface IBuilderInitialized
    {
    }
}
