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
            return new ServiceBus(new SubscriptionManager(
                new SubscriptionClientFactory(messagingFactory,  
                        new SubscriptionRepository(namespaceManager,
                        new TopicRepository(namespaceManager))), //TODO fix ugly dependencies 
                    new MessageReceiver(), 
                    new SubscriptionConfigurationRepository()),
                new MessageDispatcher(new TopicClientFactory(messagingFactory)));
        }
    }
}
