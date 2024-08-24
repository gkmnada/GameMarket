using Microsoft.EntityFrameworkCore;

namespace Order.Persistence.Context
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Domain.Entities.Order> Orders { get; set; }
    }
}
