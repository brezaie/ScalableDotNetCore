using System;
using Domain;
using MassTransit;
using MassTransit.Util;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Set RabbitMQ properties
                RabbitMqConfiguration.RabbitMqUri = "rabbitmq://localhost";
                RabbitMqConfiguration.VirtualHost = "uservh";
                RabbitMqConfiguration.UserName = "user";
                RabbitMqConfiguration.Password = "123456";
                RabbitMqConfiguration.OAuthServiceQueue = "oauth.service";
                RabbitMqConfiguration.Timeout = TimeSpan.FromSeconds(10);


                var bus = BusConfigurator.ConfigureBus((cfg, host) =>
                {
                    cfg.ReceiveEndpoint(host, RabbitMqConfiguration.OAuthServiceQueue, e =>
                    {
                        //This consumer is fired when a request is sent from CreateUser method in UserController
                        e.Consumer<CreateUserCommandConsumer>();
                    });
                });

                TaskUtil.Await(() => bus.StartAsync());

                Console.WriteLine("Receiver started ...");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
