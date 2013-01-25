using System;
using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Subscription
{
    public class MessageReceiver : IMessageReceiver
    {
        public void Receive<T>(SubscriptionClient subscriptionClient, ISubscriptionReceiveConfiguration<T> config, Action<T> action)
        {
            //NOTE cofig unused for now
            subscriptionClient.BeginReceive(
                (cb) =>
                {
                    var brokeredMessage = subscriptionClient.EndReceive(cb);
                    HandleMessage(brokeredMessage, subscriptionClient.Mode, action); 
                    Receive(subscriptionClient, config, action);
                },
                null);
        }

        private static void HandleMessage<T>(BrokeredMessage message, ReceiveMode mode, Action<T> action)
        {
            if (message == null) return;
            var messageData = message.GetBody<T>();
            action(messageData);
            if(mode == ReceiveMode.PeekLock)
                message.Complete();
        }
    }
}
