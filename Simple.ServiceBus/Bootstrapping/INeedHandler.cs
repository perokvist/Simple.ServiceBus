using System;

namespace Simple.ServiceBus.Bootstrapping
{
    public interface INeedHandler<out TMessage>
    {
        INeedConfig Using<THandler>() where THandler : IObserver<TMessage>;
        INeedConfig UsingNamed(string name);

    }
}