using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.ServiceBus.Autofac
{
    public interface  IAutofacHandler<in T> : IHandle<T>
    {
         
    }

    public class AutofacHandler<T> : IAutofacHandler<T>
    {
        private readonly IComponentContext _context;

        public AutofacHandler(IComponentContext context)
        {
            _context = context;
        }

        public void Handle(T message)
        {
            var handler = _context.Resolve<IHandle<T>>();
            handler.Handle(message);
        }

        public void Handle(object message)
        {
            this.Handle((dynamic)message);
        }
    }
}
