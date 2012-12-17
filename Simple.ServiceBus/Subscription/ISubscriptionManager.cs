namespace Simple.ServiceBus.Subscription
{
    public interface ISubscriptionManager
    {
        void Subscribe<T>(IHandle<T> handler);
    }
}