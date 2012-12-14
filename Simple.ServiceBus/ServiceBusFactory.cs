using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.ServiceBus.Infrastructure;

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
                        new TopicRepository(namespaceManager))) //TODO fix ugly dependencies 
                    , new Infrastructure.MessageReceiver()),
                new MessageDispatcher(new TopicClientFactory(messagingFactory)));
        }
    }
}
