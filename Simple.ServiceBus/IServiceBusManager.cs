namespace Simple.ServiceBus
{
    public interface IServiceBusManager
    {
        void DeleteTopic<T>();
        void DeleteSubscription<T>(string subscriptionName);
    }
}