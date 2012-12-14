namespace Simple.ServiceBus.Infrastructure
{
    public interface ISubscriptionManager
    {
        void Subscribe<T>(IHandle<T> handler);
    }
}