using System;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace Domain
{
    public class BusConfigurator
    {
        public static IBusControl ConfigureBus(
            Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null)
        {
            try
            {
                return Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri(RabbitMqConfiguration.FullAddress), hst =>
                    {
                        hst.Username(RabbitMqConfiguration.UserName);
                        hst.Password(RabbitMqConfiguration.Password);
                    });

                    registrationAction?.Invoke(cfg, host);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IRequestClient<ITCommand, ITEvent> CreateRequestClient<ITCommand, ITEvent>(IBus bus)
            where ITCommand : class
            where ITEvent : class

        {
            try
            {
                var serviceAddress = new Uri(RabbitMqConfiguration.FullAddress + RabbitMqConfiguration.OAuthServiceQueue);
                var client = bus.CreateRequestClient<ITCommand, ITEvent>(serviceAddress, RabbitMqConfiguration.Timeout,
                    RabbitMqConfiguration.Timeout);

                return client;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}