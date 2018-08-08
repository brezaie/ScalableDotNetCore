using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Command;
using Domain.Event;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace RabbitMqMassTransit
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


                #region MassTransit

                //Set RabbitMQ properties
                RabbitMqConfiguration.RabbitMqUri = Configuration["RabbitMq:Url"];
                RabbitMqConfiguration.VirtualHost = Configuration["RabbitMq:VirtualHost"];
                RabbitMqConfiguration.UserName = Configuration["RabbitMq:UserName"];
                RabbitMqConfiguration.Password = Configuration["RabbitMq:Password"];
                RabbitMqConfiguration.OAuthServiceQueue = Configuration["RabbitMq:OAuth2ServiceQueue"];
                RabbitMqConfiguration.Timeout = TimeSpan.FromSeconds(Convert.ToInt32(Configuration["RabbitMq:Timeout"]));


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

                #endregion
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
