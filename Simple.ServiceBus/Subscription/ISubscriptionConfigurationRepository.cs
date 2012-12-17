namespace Simple.ServiceBus.Subscription
{
    public interface ISubscriptionConfigurationRepository
    {
        ISubscriptionConfiguration<T> Get<T>();
    }
}