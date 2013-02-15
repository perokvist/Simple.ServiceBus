using System.Threading.Tasks;

namespace Simple.ServiceBus.Publishing
{
    public interface IMessageDispatcher
    {
        Task Publish<T>(T message);
    }
}