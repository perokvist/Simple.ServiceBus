using System;
using System.Collections.Generic;
using System.Linq;

namespace Simple.ServiceBus.Subscription
{
    public class SubscriptionConfigurationRepository : ISubscriptionConfigurationRepository
    {
        private ISet<Delegate> _configs;
        
        public void Add<T>(Action<ISubscriptionConfiguration<T>> action)
        {
            if(_configs == null)
                _configs = new HashSet<Delegate>();

            Func<ISubscriptionConfiguration<T>> accessor = () =>
                                                        {
                                                            var config = new SubscriptionConfiguration<T>();
                                                            action(config);
                                                            return config;
                                                        };
            if (!_configs.Add(accessor))
                throw new InvalidOperationException(string.Format("Configuration for {0} already in repository", typeof(T).ToString()));
        }

        public ISubscriptionConfiguration<T> Get<T>()
        {
            var configAccessor = GetConfig<ISubscriptionConfiguration<T>>();
            if (configAccessor != null)
                return configAccessor();

            return new SubscriptionConfiguration<T>();
        } 

        private Func<TConfig> GetConfig<TConfig>() where TConfig : class
        {
            return _configs.FirstOrDefault(x => x is Func<TConfig>) as Func<TConfig>;
        }
    }
}
