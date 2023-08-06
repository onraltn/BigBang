using BigBang.Order.Api.Middlewares;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
namespace BigBang.Order.Api
{
    public static class ServiceRegistration
    {
        public static void RegisterHost(this IServiceCollection services)
        {
            AddConvey(services);
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
        public static void ConfigureApp(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Instruction.WebApi v1"));
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection()
               .UseStaticFiles(
                new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory + "/wwwroot")
                })

               .UseRouting()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
               {
                   endpoints.MapControllers();
               });
        }

    }
}
