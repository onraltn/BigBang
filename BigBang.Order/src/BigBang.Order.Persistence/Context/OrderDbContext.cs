using BigBang.Order.Domain.Aggregates.OrderAggregate;
using BigBang.Order.Domain.Aggregates.OrderAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace BigBang.Order.Persistence.Context
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order.Domain.Aggregates.OrderAggregate.Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
