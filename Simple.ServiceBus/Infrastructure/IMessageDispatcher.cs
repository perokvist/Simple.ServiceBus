namespace Simple.ServiceBus.Infrastructure
{
    public interface IMessageDispatcher
    {
        void Publish<T>(T message);
    }
}