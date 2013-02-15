using System;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Simple.ServiceBus.Infrastructure;

namespace Simple.ServiceBus.Publishing
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly ITopicClientFactory _topicClientFactory;

        public MessageDispatcher(ITopicClientFactory topicClientFactory)
        {
            _topicClientFactory = topicClientFactory;
        }

        public async Task Publish<T>(T message)
        {
            var topicClient = await _topicClientFactory.CreateFor<T>();
            try
            {
                var task = Task.Factory.FromAsync(topicClient.BeginSend, topicClient.EndSend, new BrokeredMessage(message), null);
                await task;
            }
            finally
            {
                topicClient.Close();
            }

        }

    }
}
