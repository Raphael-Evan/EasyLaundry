using Easy_Laundry.Models;
using Microsoft.EntityFrameworkCore;

namespace Easy_Laundry.Data
{
    public class Easy_LaundryContext : DbContext
    {
        public Easy_LaundryContext(DbContextOptions<Easy_LaundryContext> options)
            : base(options)
        {
        }

        public DbSet<OrderModel> OrderModel { get; set; } = default!;
        public DbSet<Loginmodel> Loginmodel { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderModel>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");
        }
    }
}
