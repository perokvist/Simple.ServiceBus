using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.ServiceBus.Extensions
{
    public static class NamespaceManagerExtensions
    {
        public static Task<bool> TopicExistsAsync(this NamespaceManager nm, string topicPath, object state)
        {
            return Task.Factory.FromAsync<string, bool>(
                nm.BeginTopicExists, nm.EndTopicExists, topicPath, state);
        }

        public static Task<TopicDescription> CreateTopicAsync(this NamespaceManager nm, string topicPath, object state)
        {
            return Task.Factory.FromAsync<string, TopicDescription>
                (nm.BeginCreateTopic, nm.EndCreateTopic,
                                                    topicPath, state);
        }

        public static Task<TopicDescription> GetTopicAsync(this NamespaceManager nm, string topicPath, object state)
        {
            return Task.Factory.FromAsync<string, TopicDescription>(
                nm.BeginGetTopic, nm.EndGetTopic, topicPath, state);
        }

        public static Task<bool> SubscriptionExistsAsync(this NamespaceManager nm, string topicPath, string subscriptionName, object state)
        {
            return Task.Factory.FromAsync<string, string, bool>(
                    nm.BeginSubscriptionExists, nm.EndSubscriptionExists, topicPath, subscriptionName, state);
        }


        public static Task<SubscriptionDescription> CreateSubscriptionAsync(this NamespaceManager nm, string topicPath, string subscriptionName, object state)
        {
            return Task.Factory.FromAsync<string, string, SubscriptionDescription>(
                    nm.BeginCreateSubscription, nm.EndCreateSubscription, topicPath, subscriptionName, state);

        }

        public static Task<SubscriptionDescription> GetSubscriptionAsync(this NamespaceManager nm, string topicPath, string subscriptionName, object state)
        {
            return Task.Factory.FromAsync<string, string, SubscriptionDescription>(
                    nm.BeginGetSubscription, nm.EndGetSubscription, topicPath, subscriptionName, state);
        }

        public static Task DeleteSubscriptionAsync(this NamespaceManager nm, string topicPath, string subscriptionName, object state)
        {
            return Task.Factory.FromAsync<string, string>(
                    nm.BeginDeleteSubscription, nm.EndDeleteSubscription, topicPath, subscriptionName, state);
        }

        public static Task DeleteTopicAsync(this NamespaceManager nm, string topicPath, object state)
        {
            return Task.Factory.FromAsync<string>(
                    nm.BeginDeleteTopic, nm.EndDeleteTopic, topicPath, state);
        }

    }
}
