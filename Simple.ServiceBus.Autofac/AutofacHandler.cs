using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.ServiceBus.Autofac
{
    public interface  IAutofacHandler<in T> : IObserver<T>
    {
         
    }

    public class AutofacHandler<T> : IAutofacHandler<T>
    {
        private readonly IComponentContext _context;

        public AutofacHandler(IComponentContext context)
        {
            _context = context;
        }

        public void OnCompleted()
        {
            Observer.OnCompleted();
        }

        public void OnError(Exception error)
        {
            Observer.OnError(error);
        }

        public void OnNext(T value)
        {
            Observer.OnNext(value);
        }

        private IObserver<T> Observer
        {
            get { return _context.Resolve<IObserver<T>>(); }
        } 

    }
}
