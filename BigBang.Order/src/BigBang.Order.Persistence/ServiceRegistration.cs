using BigBang.Order.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BigBang.Order.Domain.Repositories;
using BigBang.Order.Persistence.Repositories;

namespace BigBang.Order.Persistence
{
    public static class ServiceRegistration
    {
        public static void RegisterPersistence(this IServiceCollection services)
        {
            services.AddDbContext<OrderDbContext>(opt => opt.UseInMemoryDatabase("OrderDb"));

            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}
