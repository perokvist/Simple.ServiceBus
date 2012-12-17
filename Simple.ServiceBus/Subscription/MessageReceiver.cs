using System;
using Microsoft.ServiceBus.Messaging;

namespace Simple.ServiceBus.Subscription
{
    public class MessageReceiver : IMessageReceiver
    {
        public void Receive<T>(SubscriptionClient subscriptionClient, ISubscriptionReceiveConfiguration<T> config, Action<T> action)
        {
            //var handler = ModeHandler(action);
            subscriptionClient.BeginReceive(
                (cb) =>
                {
                    var brokeredMessage = subscriptionClient.EndReceive(cb);
                    if (brokeredMessage != null)
                    {
                        var messageData = brokeredMessage.GetBody<T>();
                        action(messageData);
                        if(subscriptionClient.Mode == ReceiveMode.PeekLock)
                            brokeredMessage.Complete();

                        //handler(subscriptionClient.Mode, brokeredMessage);
                        Receive(subscriptionClient, config, action);
                    }
                },
                null);
        }

        private Action<ReceiveMode, BrokeredMessage> ModeHandler<T>(Action<T> action)
        {
            return (m, bm) =>
                       {
                        var messageData = bm.GetBody<T>();
                        action(messageData);
                           if(m == ReceiveMode.PeekLock)
                           {
                               bm.Complete();
                           }
                       };
        }
    }
}
