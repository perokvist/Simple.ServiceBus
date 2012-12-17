namespace Simple.ServiceBus.Publishing
{
    public interface IMessageDispatcher
    {
        void Publish<T>(T message);
    }
}