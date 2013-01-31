#Simple.ServiceBus

Library for simple pub/sub for the Windows Service Bus. 

##Example

```
var container = new ContainerBuilder()
                .RegisterServiceBus()
                .RegisterHandlers(System.Reflection.Assembly.GetExecutingAssembly())
                .ListenFor<SimpleMessage>().Configure(c => c.ReceiveMode = ReceiveMode.ReceiveAndDelete)
                .Subscribe<SimpleMessage>(x => Console.WriteLine(string.Format("Received (delegate) '{0}' with message id of {1}", x.Title, x.Id)))
                .Build();

            var serviceBus = container.Resolve<IServiceBus>();
 ```
 
``` 
 public class SimpleHandler : IHandle<SimpleMessage>
    {
        public void Handle(SimpleMessage message)
        {
            Console.WriteLine(string.Format("Received (autofac) '{0}' with message id of {1}", message.Title, message.Id));
        }

        public void Handle(object message)
        {
            this.Handle((SimpleMessage) message);
        }
    }
```

#Next

* Add Configuration support for SubscriptionClient sufix
* Commadning scenario, Maybe a version of listen for named process, that only allows one recipient
* Command scenario, proccessmanager that start/stop subscription of registered handlers
* Retrieve and process messages async/parallel, config parallel
* Deadletter/fault handling
* Transient support

### Downloads

Download from NuGet 'Simple.ServiceBus' [Search NuGet for Simple.ServiceBus](http://nuget.org/packages?q=simple.servicebus&prerelease=true&sortOrder=relevance)

