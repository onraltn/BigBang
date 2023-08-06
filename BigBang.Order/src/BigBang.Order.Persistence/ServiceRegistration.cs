using BigBang.Order.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
namespace BigBang.Order.Persistence
{
    public static class ServiceRegistration
    {
        public static void RegisterPersistence(this IServiceCollection services)
        {
            services.AddDbContext<OrderDbContext>(opt => opt.UseInMemoryDatabase("OrderDb"));
        }
    }
}
