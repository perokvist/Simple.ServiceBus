using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.ServiceBus.Infrastructure
{
    public class MessageReceiver : IMessageReceiver
    {
        public void Receive<T>(SubscriptionClient subscriptionClient, Action<T> action)
        {
            subscriptionClient.BeginReceive(
                (cb) =>
                {
                    var brokeredMessage = subscriptionClient.EndReceive(cb);
                    if (brokeredMessage != null)
                    {
                        var messageData = brokeredMessage.GetBody<T>();
                        action(messageData);
                        Receive(subscriptionClient, action);
                    }
                },
                null);
        }

    }
}
