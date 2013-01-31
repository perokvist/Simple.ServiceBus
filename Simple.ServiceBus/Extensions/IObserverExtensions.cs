using System;
using Simple.ServiceBus.Infrastructure;

namespace Simple.ServiceBus.Extensions
{
    public static class ObservableExtensions
    {
         public static IDisposable Subscribe<T>(this IObservable<T> self, Action<T> action)
         {
             return self.Subscribe(new Handler<T>(action));
         }
    }
}