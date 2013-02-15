#Simple.ServiceBus

Library for simple pub/sub for the Windows Service Bus. 

##Example

```
var container = new ContainerBuilder()
          .RegisterServiceBus()
          .Build();
 
  var serviceBus = container.Resolve<IServiceBus>();
  var manager = container.Resolve<IServiceBusManager>();

 ```
 
``` 
 var container = new ContainerBuilder()
      .RegisterServiceBus()
      .RegisterObservers(typeof(Program).Assembly)
      .Build();
 
var resolver = new Resolver(container.ResolveNamed, container.Resolve);
 
serviceConfig.ConstructUsing(
    s =>
    new SubscriptionConfigurationService(resolver,
     map => map.ListenTo<SimpleMessage>().Using<SimpleHandler>()
            .WithConfiguration(new SubscriptionConfiguration("Test_1"))
        ));
serviceConfig.WhenStarted(sv => sv.Start());
serviceConfig.WhenStopped(sv => sv.Stop());
 
```


### Downloads

Download from NuGet 'Simple.ServiceBus' [Search NuGet for Simple.ServiceBus](http://nuget.org/packages?q=simple.servicebus&prerelease=true&sortOrder=relevance)

