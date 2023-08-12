using BigBang.Order.Consumer.Consumers;
using BigBang.Order.Infrastructure.Events.Constants;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace BigBang.Order.Consumer
{
    internal static class ServiceRegistration
    {
        public static void RegisterHost(this IServiceCollection services, IConfiguration configuration)
        {
            AddConvey(services);
            RegisterMassTransit(services, configuration);
        }
        static void AddConvey(IServiceCollection services)
        {
            services.AddConvey()
                .AddCommandHandlers()
                .AddEventHandlers()
                .AddQueryHandlers()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryQueryDispatcher()
                .AddInMemoryEventDispatcher();

        }
        internal static void ConfigureApp(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

        }

        internal static void RegisterMassTransit(
            IServiceCollection services,
            IConfiguration configuration,
            int prefetchCount = 8,
            double millisecond = 300)
        {
            var settings = configuration.GetSection("MessageBrokerConfiguration");

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(settings["HostName"], t =>
                    {
                        t.Username(settings["UserName"]);
                        t.Password(settings["Password"]);
                    });

                    cfg.AutoDelete = false;
                    cfg.Durable = true;

                    cfg.ReceiveEndpoint(QueueName.PaymentCreatedQueueName, e =>
                    {
                        e.Consumer(() => new PaymentCreatedConsumer(context));

                        e.PrefetchCount = prefetchCount;
                        e.UseRetry(r => r.Interval(3, TimeSpan.FromMilliseconds(millisecond)));
                    });

                    cfg.ConfigureEndpoints(context, KebabCaseEndpointNameFormatter.Instance);
                });
            });
        }
    }
}
