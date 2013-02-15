using System;

namespace Simple.ServiceBus.Subscription
{
    internal class DisposableAction : IConfigurated
    {
        private readonly Action _action;

        public DisposableAction(Action action)
        {
            _action = action;
        }

        public void Dispose()
        {
            _action();
        }

        public SubscriptionConfiguration Config { get; set; }
    }
}