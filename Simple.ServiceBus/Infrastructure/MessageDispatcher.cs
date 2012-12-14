using System;
using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Infrastructure
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly ITopicClientFactory _topicClientFactory;

        public MessageDispatcher(ITopicClientFactory topicClientFactory)
        {
            _topicClientFactory = topicClientFactory;
        }

        public void Publish<T>(T message)
        {
            var topicClient = _topicClientFactory.CreateFor<T>();
            try
            {
                topicClient.Send(new BrokeredMessage(message));
            }
            catch (Exception x)
            {
                throw x;
            }
            finally
            {
                topicClient.Close();
            }

        }

    }
}
