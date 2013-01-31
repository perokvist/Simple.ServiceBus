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
        public IServiceBus Create() {
            var messagingFactory = MessagingFactory.Create(); 
            var namespaceManager = NamespaceManager.Create();
            var topicRepository = new TopicRepository(namespaceManager);
            var observablefactory = new ObservableSubscriptionManagerFactory(
                    new MessageReceiver(new SubscriptionClientFactory(messagingFactory, new SubscriptionRepository(namespaceManager, topicRepository))),
                    new SubscriptionConfigurationRepository()
                );
            return new ServiceBus(new SubscriptionManager(observablefactory), new MessageDispatcher(new TopicClientFactory(messagingFactory, topicRepository)));
        }
    }
}
