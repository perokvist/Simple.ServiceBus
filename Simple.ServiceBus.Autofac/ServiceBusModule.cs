using Autofac;
using Microsoft.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Simple.ServiceBus.Infrastructure;

namespace Simple.ServiceBus.Autofac
{
    public class ServiceBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetAssembly(typeof (IServiceBus)))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.Register(x => NamespaceManager.Create())
                .AsSelf()
                .SingleInstance();
            builder.Register(x => MessagingFactory.Create())
                .AsSelf()
                .SingleInstance();
            
        }
    }
}
