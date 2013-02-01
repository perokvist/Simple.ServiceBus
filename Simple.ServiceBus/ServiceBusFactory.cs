using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.ServiceBus.Infrastructure;
using Simple.ServiceBus.Publishing;
using Simple.ServiceBus.Subscription;
using MessageReceiver = Simple.ServiceBus.Subscription.MessageReceiver;

namespace Simple.ServiceBus
{
    public class ServiceBusFactory
    {
        private static readonly MessagingFactory MessagingFactory = MessagingFactory.Create();
        private static readonly NamespaceManager NamespaceManager = NamespaceManager.Create();
        private static readonly TopicRepository TopicRepository = new TopicRepository(NamespaceManager);
        private static readonly SubscriptionConfigurationRepository SubscriptionConfigurationRepository =  new SubscriptionConfigurationRepository();

        internal static readonly ObservableSubscriptionManagerFactory ObservableSubscriptionManagerFactory = new ObservableSubscriptionManagerFactory(
                    new MessageReceiver(new SubscriptionClientFactory(MessagingFactory, new SubscriptionRepository(NamespaceManager, TopicRepository)), SubscriptionConfigurationRepository),
                    new SubscriptionConfigurationRepository()
                );

        private static readonly IServiceBus ServiceBus = new ServiceBus(
            new SubscriptionManager(ObservableSubscriptionManagerFactory),
            new MessageDispatcher(new TopicClientFactory(MessagingFactory, TopicRepository)));

        public IServiceBus Create()
        {
            return ServiceBus;
        }
    }

    public static class ObservableFactory
    {

        public static IObservable<T> Create<T>()
        {
            return
               ServiceBusFactory.ObservableSubscriptionManagerFactory.Create<T>();
        }
    }
}
