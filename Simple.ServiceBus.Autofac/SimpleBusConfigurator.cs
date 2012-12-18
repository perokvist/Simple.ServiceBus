using System;
using System.Linq;
using Autofac;
using Autofac.Core;
using Simple.ServiceBus.Infrastructure;

namespace Simple.ServiceBus.Autofac
{
    public class SimpleBusConfigurator : ISimpleBusConfigurator
    {
        public SimpleBusConfigurator(ContainerBuilder containerBuilder)
        {
            Builder = containerBuilder;
        }

        public ISubscriptionConfigurator<T> ListenFor<T>()
        {
            Builder.RegisterType<AutofacHandler<T>>()
                .As<IAutofacHandler<T>>();
            Builder.RegisterCallback(x => x
                                        .RegistrationsFor(
                                            new TypedService(typeof(IServiceBus))).First()
                                        .Activating += (s, e) => ((IServiceBus)e.Instance).Subscribe(e.Context.Resolve<IAutofacHandler<T>>())
                );

            return new SubscriptionConfigurator<T>(this);
        }

        public ISimpleBusConfigurator Subscribe<T>(Action<T> handler)
        {
            var b = Builder;
            b.RegisterCallback(x => x
                                        .RegistrationsFor(
                                            new TypedService(typeof(IServiceBus))).First()
                                        .Activating += (s, e) => ((IServiceBus)e.Instance).Subscribe(new Handler<T>(handler))
                );

            return this;
        }

        public ContainerBuilder Builder { get; private set; }
    }
}