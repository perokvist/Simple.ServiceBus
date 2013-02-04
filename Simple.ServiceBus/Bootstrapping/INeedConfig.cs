using Simple.ServiceBus.Subscription;

namespace Simple.ServiceBus.Bootstrapping
{
    public interface INeedConfig
    {
        IConfigurated WithConfiguration(SubscriptionConfiguration configuration);
    }
}