using System;

namespace Simple.ServiceBus.Bootstrapping
{
    public class Map
    {
        private readonly IResolver _resolver;

        public Map(IResolver context)
        {
            _resolver = context;
        }

        public INeedHandler<T> ListenTo<T>()
        {
            return new NeedHandler<T>(_resolver);
        }

        private class NeedHandler<T> : INeedHandler<T>
        {
            private readonly IResolver _resolver;

            public NeedHandler(IResolver context)
            {
                _resolver = context;
            }
            public INeedConfig Using<THandler>() where THandler : IObserver<T>
            {
                return new NeedConfig<T, THandler>(_resolver);
            }

            public INeedConfig UsingNamed(string name)
            {
                return new NeedConfig<T, IObserver<T>>(_resolver, name);
            }
        }
    }
}