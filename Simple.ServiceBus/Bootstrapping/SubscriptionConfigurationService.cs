using System;
using System.Collections.Generic;
using System.Linq;
using Simple.ServiceBus.Extensions;

namespace Simple.ServiceBus.Bootstrapping
{
    public class SubscriptionConfigurationService
    {
        private readonly Func<Map, IDisposable>[] _mapActions;
        private IEnumerable<IDisposable> _stoppers;
        private readonly Map _map;

        public SubscriptionConfigurationService(IResolver resolver, params Func<Map, IDisposable>[] mapActions)
        {
            _map = new Map(resolver);
            _mapActions = mapActions;
        }

        public void Start()
        {
            _stoppers = StartHandlers();
        }

        private IEnumerable<IDisposable> StartHandlers()
        {
            return _mapActions.Select(a => a(_map)).ToList();
        }

        public bool Stop()
        {
            _stoppers.ForEach(disposable => disposable.Dispose());
            return true;
        }
    }
}