using BigBang.Order.Api.Consumers;
using BigBang.Order.Api.Middlewares;
using BigBang.Order.Application.ApplicationCommands.CreateOrder;
using BigBang.Order.Application.ApplicationQueries.GetOrderByTrackingNumber;
using BigBang.Order.Infrastructure.Events.Constants;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
namespace BigBang.Order.Api
{
    public static class ServiceRegistration
    {
        public static void RegisterHost(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterMassTransit(services, configuration);
            AddConvey(services);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order.WebApi", Version = "v1" });
            });
            services.AddScoped<ICommandHandler<CreateOrderCommand>, CreateOrderCommandHandler>();
            
        }
        static void AddConvey(IServiceCollection services)
        {
            services.AddConvey()
                .AddCommandHandlers()
                .AddInMemoryCommandDispatcher()
                .AddEventHandlers()
                .AddInMemoryEventDispatcher()
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher();

        }
        public static void ConfigureApp(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order.WebApi v1"));
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
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

                    cfg.UseRawJsonSerializer();
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
