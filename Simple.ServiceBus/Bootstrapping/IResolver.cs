using System;

namespace Simple.ServiceBus.Bootstrapping
{
    public interface IResolver
    {
        T Resolve<T>();
        T ResolveNamed<T>(string name);
    }

    public class Resolver : IResolver
    {
        private readonly Func<string, Type, object> _namedResolver;
        private readonly Func<Type, object> _resolver;

        public Resolver(Func<string,Type, object> namedResolver, Func<Type, object> resolver)
        {
            _namedResolver = namedResolver;
            _resolver = resolver;
        }

        public T Resolve<T>()
        {
            return (T)_resolver(typeof (T));
        }

        public T ResolveNamed<T>(string name)
        {
            return (T)_namedResolver(name,typeof(T));
        }
    }
}