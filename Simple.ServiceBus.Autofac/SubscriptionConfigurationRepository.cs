using Autofac;
using Simple.ServiceBus.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.ServiceBus.Autofac
{
    public class SubscriptionConfigurationRepository : ISubscriptionConfigurationRepository
    {
        private readonly IComponentContext _context;

        public SubscriptionConfigurationRepository(IComponentContext context)
        {
            _context = context;
        }

        public ISubscriptionConfiguration<T> Get<T>()
        {
            if (!_context.IsRegistered<ISubscriptionConfiguration<T>>())
                return new SubscriptionConfiguration<T>();

            return _context.Resolve<ISubscriptionConfiguration<T>>();
        }
    }
}
