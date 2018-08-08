# ScalableDotNetCore
The .Net Core demo project scaled up with [RabbitMQ](https://www.rabbitmq.com) and [MassTransit](http://masstransit-project.com/)

## Getting Started
A quick tutorial on the project files and projects

### Requester
In the `Startup.cs`, firstly, the bus is initialized, registered and started using the properties fetched from `appsettings.json`:

```
var bus = BusConfigurator.ConfigureBus((cfg, host) =>
{
    //This receiver is fired when a response is created from CreateUserCommandConsumer
    cfg.ReceiveEndpoint(host, RabbitMqConfiguration.OAuthServiceQueue, e =>
    {
    });
});

//Registration of the bus
services.AddSingleton<IPublishEndpoint>(bus);
services.AddSingleton<ISendEndpointProvider>(bus);
services.AddSingleton<IBus>(bus);

bus.StartAsync();
```

Next, a UserController is set to request a CreateUserCommand to the receiver. Befre making the request, the RequestClient is created using the `BusConfigurator.CreateRequestClient`.

### Receiver
When a request is created, it is time for the receiver to process the request and respond. In the `Program.cs`, the bus is configured with the same propoerties as in `Startup.cs`.
Next, a consumer is designed named `CreateUserCommandConsumer` responsible for processing the `ICreateUserCommand`.
After processing the command, an event is raised named `ICreateUserEvent` to the requester.

## Deployment
Before running the solution, make sure to have [RabbitMQ](https://www.rabbitmq.com/download.html) installed. After running its [Management plugin](https://www.rabbitmq.com/management.html), it will be launched on port 15672 by default.
Next, after running the solution, both projects (`Requester` and `Receiver` are run together) start their work. Each project make an Exchange and a Queue in the RabbitMQ. Inaddition, two other Exchanges are created, one for the `ICreateUserCommand` and the other one for the `oauth.service` along with its Queue.
When making an API call to the `CreateUser` action with some parameter like 
```
{
	"email": "behzadrezaie11@gmail.com"
}
```
the message will go through the exchanges and queues to reach the `CreateUserCommandConsumer`. Next, the result will be led to the requester.
