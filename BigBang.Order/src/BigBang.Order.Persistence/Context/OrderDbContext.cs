using BigBang.Order.Domain.Aggregates.OrderAggregate;
using BigBang.Order.Domain.Aggregates.OrderAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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

            builder.Entity<Order.Domain.Aggregates.OrderAggregate.Order>()
             .HasMany(order => order.Items);

            builder.Entity<Order.Domain.Aggregates.OrderAggregate.Order>()
            .Property(c => c.Status)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasMaxLength(255)
            .IsRequired(true)
            .HasConversion(
                v => v.Name,
                v => OrderStatus.Get(v));


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
