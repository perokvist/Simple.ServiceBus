using Simple.ServiceBus.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.ServiceBus
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
    }
}
