using System;
using System.Collections.Generic;
using Simple.ServiceBus.Infrastructure;

namespace Simple.ServiceBus.Extensions
{
    public static class Extentions
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }
        }

        public static IServiceBus Subscribe<T>(this IServiceBus bus, Action<T> handler)
        {

            bus.Subscribe(new Handler<T>(handler));
            return bus;
        }

        public static T PutIf<T>(this IDictionary<Type,T> dict, Func<T> factory)
        {
            return dict.PutIf(typeof (T), factory);
        }

        public static T PutIf<TKey,T>(this IDictionary<TKey, T> dict,TKey key, Func<T> factory)
        {
            if (!dict.ContainsKey(key))
            {
                var value = factory();
                dict[key] = value;
                return value;
            }
            return dict[key];
        }
    }
}
