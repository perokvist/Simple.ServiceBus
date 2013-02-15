using System;
using Simple.ServiceBus.Subscription;

namespace Simple.ServiceBus.Bootstrapping
{
    internal class NeedConfig<TMessage, THandler> : INeedConfig where THandler : IObserver<TMessage>
    {
        private readonly IResolver _resolver;
        private readonly string _name;


        public NeedConfig(IResolver context, string name = null)
        {
            _resolver = context;
            _name = name;
        }

        public IConfigurated WithConfiguration(SubscriptionConfiguration configuration)
        {
            var serviceBus = _resolver.Resolve<IServiceBus>();
            if (_name == null)
                return (IConfigurated)serviceBus.Subscribe(_resolver.Resolve<THandler>(), configuration);
            return (IConfigurated)serviceBus.Subscribe(_resolver.ResolveNamed<THandler>(_name));
        }
    }
}